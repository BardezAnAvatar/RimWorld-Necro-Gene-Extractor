using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public class NecroGeneExtractorCorpseSettings(bool? accept, float multiplierResource, float multiplierTime)
{
    private bool _accept = accept ?? true;

    public bool Accept => _accept;

    public float CostMultiplierResource => multiplierResource;

    public float CostMultiplierTime => multiplierTime;

    public void ExposeData(string prefix)
    {
        if (accept.HasValue)
        {
            Scribe_Values.Look(ref _accept, $"{prefix}.{nameof(Accept)}", accept ?? true);
        }

        Scribe_Values.Look(ref multiplierResource, $"{prefix}.{nameof(CostMultiplierResource)}", multiplierResource);
        Scribe_Values.Look(ref multiplierTime, $"{prefix}.{nameof(CostMultiplierTime)}", multiplierTime);
    }
}
