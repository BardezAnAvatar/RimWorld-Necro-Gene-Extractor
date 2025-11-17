using Bardez.Biotech.NecroGeneExtractor.Utilities;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public class NecroGeneExtractorCorpseSettingsFresh(float costResource, float costTime) : IExposable
{
    public float CostResource = costResource;

    public float CostTime = costTime;

    public void ExposeData()
    {
        Scribe_Values.Look(ref CostResource, nameof(CostResource), CostResource);
        Scribe_Values.Look(ref CostTime, nameof(CostTime), CostTime);
        DebugMessaging.DebugMessage($"{nameof(CostTime)} changed: {CostTime}");
    }
}
