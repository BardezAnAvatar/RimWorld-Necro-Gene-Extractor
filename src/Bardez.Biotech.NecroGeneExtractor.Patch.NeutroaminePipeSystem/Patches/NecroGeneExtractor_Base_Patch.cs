using Bardez.Biotech.NecroGeneExtractor.Buildings;
using HarmonyLib;
using PipeSystem;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Patch.NeutroaminePipeSystem.Patches;

[HarmonyPatch]
public static class NecroGeneExtractor_Base_Patch
{
    /// <summary>Executes immediately after <see cref="NecroGeneExtractor_Base.Tick_ConsumeResources" />, appending to that method</summary>
    /// <param name="__instance">Instance of <see cref="NecroGeneExtractor_Base" /> being Ticked</param>
    [HarmonyPatch(typeof(NecroGeneExtractor_Base), "Tick_ConsumeResources")]
    public static void Postfix(Building __instance)
    {
        //Only run every 60 ticks, to reduce processing load
        if (!__instance.IsHashIntervalTick(60))
        {
            return;
        }

        if (!(__instance is NecroGeneExtractor_Base necroExtractor))
        {
            return;
        }

        var pipeNet = necroExtractor.GetComp<CompResource>()?.PipeNet;
        if (pipeNet != null)
        {
            var networkNeutroamine = pipeNet.Stored;
            var neutroamineNeeded = necroExtractor.NeutroamineNeeded;

            if (neutroamineNeeded >= 1f && networkNeutroamine > 1f)
            {
                pipeNet.DrawAmongStorage(1f, pipeNet.storages);
                necroExtractor.TryAddNeutroamine(1);
            }
        }
    }
}
