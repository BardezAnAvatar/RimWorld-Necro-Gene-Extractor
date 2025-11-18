using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier4;

public class NecroGeneExtractorTier4Settings : NecroGeneExtractorTierSettings
{
    protected NecroGeneExtractorTier4CorpseSettingsFresh fresh = new();
    protected NecroGeneExtractorTier4CorpseSettingsRotting rotting = new();
    protected NecroGeneExtractorTier4CorpseSettingsDessicated dessicated = new();

    public override NecroGeneExtractorCorpseSettingsFresh Fresh => fresh;

    public override NecroGeneExtractorCorpseSettingsNonFresh Rotting => rotting;

    public override NecroGeneExtractorCorpseSettingsNonFresh Dessicated => dessicated;

    protected override string ClassName => nameof(NecroGeneExtractorTier4Settings);

    public override void ExposeData()
    {
        Scribe_Deep.Look(ref fresh, nameof(Fresh));
        Scribe_Deep.Look(ref rotting, nameof(Rotting));
        Scribe_Deep.Look(ref dessicated, nameof(Dessicated));
    }
}
