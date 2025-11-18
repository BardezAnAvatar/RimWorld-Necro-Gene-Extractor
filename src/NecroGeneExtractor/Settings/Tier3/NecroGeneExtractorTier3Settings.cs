using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier3;

public class NecroGeneExtractorTier3Settings: NecroGeneExtractorTierSettings
{
    protected NecroGeneExtractorTier3CorpseSettingsFresh fresh = new();
    protected NecroGeneExtractorTier3CorpseSettingsRotting rotting = new();
    protected NecroGeneExtractorTier3CorpseSettingsDessicated dessicated = new();

    public override NecroGeneExtractorCorpseSettingsFresh Fresh => fresh;

    public override NecroGeneExtractorCorpseSettingsNonFresh Rotting => rotting;

    public override NecroGeneExtractorCorpseSettingsNonFresh Dessicated => dessicated;

    protected override string ClassName => nameof(NecroGeneExtractorTier3Settings);

    public override void ExposeData()
    {
        Scribe_Deep.Look(ref fresh, nameof(Fresh));
        Scribe_Deep.Look(ref rotting, nameof(Rotting));
        Scribe_Deep.Look(ref dessicated, nameof(Dessicated));
    }
}
