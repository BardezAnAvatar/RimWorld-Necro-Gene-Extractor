using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public class NecroGeneExtractorTierSettings(NecroGeneExtractorCorpseSettingsFresh fresh, NecroGeneExtractorCorpseSettingsNonFresh rotting, NecroGeneExtractorCorpseSettingsNonFresh dessicated)
{
    public NecroGeneExtractorCorpseSettingsFresh Fresh => fresh;
    public NecroGeneExtractorCorpseSettingsNonFresh Rotting => rotting;
    public NecroGeneExtractorCorpseSettingsNonFresh Dessicated => dessicated;

    public void ExposeData(string prefix)
    {
        Fresh.ExposeData($"{prefix}.{nameof(Fresh)}");
        Rotting.ExposeData($"{prefix}.{nameof(Rotting)}");
        Dessicated.ExposeData($"{prefix}.{nameof(Dessicated)}");
    }
}
