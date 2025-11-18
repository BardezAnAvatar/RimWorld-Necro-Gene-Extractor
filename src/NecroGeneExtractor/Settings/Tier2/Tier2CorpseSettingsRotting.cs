namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier2;

public class Tier2CorpseSettingsRotting
    : CorpseSettingsNonFresh
{
    protected override bool DefaultAccept => DefaultSettings.TIER2_ROTTING_ACCEPT;

    public Tier2CorpseSettingsRotting() => SetDefaults();
}
