using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public class NecroGeneExtractorCorpseSettingsFresh(float multiplierResource, float multiplierTime)
{
    public float CostMultiplierResource = multiplierResource;

    public float CostMultiplierTime = multiplierTime;

    public virtual void ExposeData(string prefix)
    {
        Scribe_Values.Look(ref CostMultiplierResource, $"{prefix}.{nameof(CostMultiplierResource)}", CostMultiplierResource);
        Scribe_Values.Look(ref CostMultiplierTime, $"{prefix}.{nameof(CostMultiplierTime)}", CostMultiplierTime);
    }
}
