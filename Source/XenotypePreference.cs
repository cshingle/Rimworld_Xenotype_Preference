using HarmonyLib;
using Verse;

namespace Zig158.XenotypePreference
{
    [StaticConstructorOnStartup]
    public static class XenotypePreference
    {
        static XenotypePreference()
        {
            harmonyInstance = new Harmony("Zig158.XenotypePreference");
            harmonyInstance.PatchAll();
        }

        static Harmony harmonyInstance;
    }
}