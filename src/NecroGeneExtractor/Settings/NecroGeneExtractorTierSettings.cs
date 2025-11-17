using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public class NecroGeneExtractorTierSettings(NecroGeneExtractorCorpseSettingsFresh fresh,
    NecroGeneExtractorCorpseSettingsNonFresh rotting,
    NecroGeneExtractorCorpseSettingsNonFresh dessicated) : IExposable
{
    public NecroGeneExtractorCorpseSettingsFresh Fresh => fresh;

    public NecroGeneExtractorCorpseSettingsNonFresh Rotting => rotting;

    public NecroGeneExtractorCorpseSettingsNonFresh Dessicated => dessicated;

    public void ExposeData()
    {
        Scribe_Values.Look(ref fresh, nameof(Fresh));
        Scribe_Values.Look(ref rotting, nameof(Rotting));
        Scribe_Values.Look(ref dessicated, nameof(Dessicated));
    }
}
