using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Zig158.XenotypePreference
{
    [HarmonyPatch(typeof(Xenogerm), nameof(Xenogerm.PawnIdeoDisallowsImplanting))]
    public static class XpPawnIdeoDisallowsImplanting
    {
        public static void Postfix(ref bool __result, Xenogerm __instance, Pawn selPawn)
        {
            __result = !CanPawnAcceptXenogerm(selPawn, __instance);
        }

        private static bool CanPawnAcceptXenogerm(Pawn pawn, Xenogerm xenogerm)
        {
            var ideo = pawn.Ideo;
            if (!ideo.PreferredXenotypes.Any() && !ideo.PreferredCustomXenotypes.Any() || pawn.genes == null)
                return true;

            var xenogermGenes = xenogerm.GeneSet.GenesListForReading.Where(g => g.passOnDirectly).ToHashSet();
            var resultingGenes = xenogermGenes
                .Concat(pawn.genes.Endogenes.Select(g => g.def).Where(gd => gd.passOnDirectly))
                .ToHashSet();
            
            // if pawn has at least the genes in the preferred xenotypes then return true
            return ideo.PreferredXenotypes.Any(xt =>
                                              xt.genes.TrueForAll(g => !g.passOnDirectly || resultingGenes.Contains(g))) ||
                                          ideo.PreferredCustomXenotypes.Any(cx =>
                                              cx.genes.TrueForAll(g => !g.passOnDirectly || resultingGenes.Contains(g)));
        }
    }
}