using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public class NecroGeneExtractorTierSettings(NecroGeneExtractorCorpseSettings fresh, NecroGeneExtractorCorpseSettings rotting, NecroGeneExtractorCorpseSettings dessicated)
{
    NecroGeneExtractorCorpseSettings Fresh => fresh;
    NecroGeneExtractorCorpseSettings Rotting => rotting;
    NecroGeneExtractorCorpseSettings Dessicated => dessicated;

    public void ExposeData(string prefix)
    {
        Fresh.ExposeData($"{prefix}.{nameof(Fresh)}");
        Rotting.ExposeData($"{prefix}.{nameof(Rotting)}");
        Dessicated.ExposeData($"{prefix}.{nameof(Dessicated)}");
    }
}
