using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public class NecroGeneExtractorCorpseSettingsNonFresh(bool accept, float multiplierResource, float multiplierTime) : IExposable
{
    public bool Accept = accept;

    public float CostMultiplierResource = multiplierResource;

    public float CostMultiplierTime = multiplierTime;

    public void ExposeData()
    {
        Scribe_Values.Look(ref Accept, nameof(Accept), Accept);
        Scribe_Values.Look(ref CostMultiplierResource, nameof(CostMultiplierResource), CostMultiplierResource);
        Scribe_Values.Look(ref CostMultiplierTime, nameof(CostMultiplierTime), CostMultiplierTime);
    }
}
