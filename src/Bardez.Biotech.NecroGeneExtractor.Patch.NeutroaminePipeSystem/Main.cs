using HarmonyLib;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Patch.NeutroaminePipeSystem;

[StaticConstructorOnStartup]
public class Main
{
    static Main()
    {
        var harmony = new Harmony("NGET_VRE_ANDROIDS_COMPAT");
        harmony.PatchAll();

        Log.Message($"[Necro Gene Extraction Tiers] Compatibility for Vanilla Races Expanded: Androids loaded.");
    }
}
