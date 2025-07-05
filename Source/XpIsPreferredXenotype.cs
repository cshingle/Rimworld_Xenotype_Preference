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
            return PreferredXenotypeCheck(__instance, pawn);
        }

        private static bool PreferredXenotypeCheck(Ideo ideo, Pawn pawn)
        {
            if (HasPreferredXenotypes(ideo))
                return false;

            if (IsPreferredXenotype(pawn, ideo))
                return true;

            if (IsEvolvedXenotype(pawn, ideo))
                return true;

            return false;
        }
        
                /// <summary>
        /// Checks to see is ideology has preferred Xenotype
        /// </summary>
        public static bool HasPreferredXenotypes(Ideo ideo)
        {
            return !ideo.PreferredXenotypes.Any() && !ideo.PreferredCustomXenotypes.Any();
        }

        /// <summary>
        /// Checks to see if the pawn's xenotype matches one of the ideology xenotypes
        /// </summary>
        public static bool IsPreferredXenotype(Pawn pawn, Ideo ideo)
        {
            // Check standard Xenotypes
            if (pawn.genes.Xenotype != null && pawn.genes.CustomXenotype == null && !pawn.genes.UniqueXenotype)
            {
                 return ideo.PreferredXenotypes.Contains(pawn.genes.Xenotype);
            }

            if (pawn.genes.CustomXenotype != null)
            {
                return ideo.PreferredCustomXenotypes.Contains(pawn.genes.CustomXenotype);
            }
            
            return false;
        }

        /// <summary>
        /// Checks to see if the pawn has at least the base set of genes from one of the ideology xenotypes
        /// </summary>
        public static bool IsEvolvedXenotype(Pawn pawn, Ideo ideo)
        {
            var pawnGeneDefs = pawn.genes.GenesListForReading.Select(gene => gene.def);
            // check standard xenotypes
            foreach (XenotypeDef xenotype in ideo.PreferredXenotypes)
            {
                if (xenotype.genes.Count > 0 && xenotype.AllGenes.TrueForAll(gene => pawnGeneDefs.Contains(gene) || !gene.passOnDirectly))
                    return true;
            }
            
            // check custom xenotypes
            foreach (CustomXenotype xenotype in ideo.PreferredCustomXenotypes)
            {
                if (xenotype.genes.Count > 0 && xenotype.genes.TrueForAll(gene => pawnGeneDefs.Contains(gene) || !gene.passOnDirectly))
                    return true;
            }
            
            return false;
        }
    }
}