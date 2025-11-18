using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier2;

public class NecroGeneExtractorTier2Settings : NecroGeneExtractorTierSettings
{
    protected NecroGeneExtractorTier2CorpseSettingsFresh fresh = new();
    protected NecroGeneExtractorTier2CorpseSettingsRotting rotting = new();
    protected NecroGeneExtractorTier2CorpseSettingsDessicated dessicated = new();

    public override NecroGeneExtractorCorpseSettingsFresh Fresh => fresh;

    public override NecroGeneExtractorCorpseSettingsNonFresh Rotting => rotting;

    public override NecroGeneExtractorCorpseSettingsNonFresh Dessicated => dessicated;

    protected override string ClassName => nameof(NecroGeneExtractorTier2Settings);

    public override void ExposeData()
    {
        Scribe_Deep.Look(ref fresh, nameof(Fresh));
        Scribe_Deep.Look(ref rotting, nameof(Rotting));
        Scribe_Deep.Look(ref dessicated, nameof(Dessicated));
    }
}
