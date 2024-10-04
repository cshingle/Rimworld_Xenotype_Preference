using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;


namespace Zig158.XenotypePreference
{
    [HarmonyPatch(typeof(Ideo), nameof(Ideo.IsPreferredXenotype))]
    public static class XpIsPreferredXenotype
    {
        static bool Postfix(bool result, Ideo __instance, Pawn pawn)
        {
            return result || PreferredXenotypeCheck(__instance, pawn);
        }

        private static bool PreferredXenotypeCheck(Ideo ideo, Pawn pawn)
        {
            if (!ideo.PreferredXenotypes.Any() && !ideo.PreferredCustomXenotypes.Any() || pawn.genes == null)
                return false;
            
            var pawnGeneDefs = GetGeneDefSet(pawn);
            return ideo.PreferredXenotypes.Any(xt =>
                       xt.genes.TrueForAll(g => !g.passOnDirectly || pawnGeneDefs.Contains(g))) ||
                   ideo.PreferredCustomXenotypes.Any(cx =>
                       cx.genes.TrueForAll(g => !g.passOnDirectly || pawnGeneDefs.Contains(g)));
        }

        private static HashSet<GeneDef> GetGeneDefSet(Pawn pawn, bool excludeNotPassedOnDirectly = true)
        {
            var geneDefs = pawn.genes.GenesListForReading.Select(g => g.def);
            return (excludeNotPassedOnDirectly ? geneDefs.Where(g => g.passOnDirectly) : geneDefs).ToHashSet();
        }
    }
}