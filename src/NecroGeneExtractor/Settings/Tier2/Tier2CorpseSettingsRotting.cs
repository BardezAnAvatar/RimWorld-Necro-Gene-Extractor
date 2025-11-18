namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier2;

public class Tier2CorpseSettingsRotting : CorpseSettingsNonFresh
{
    protected override bool DefaultAccept => DefaultSettings.TIER2_ROTTING_ACCEPT;

    protected override float DefaultResource => DefaultSettings.ROTTING_RESOURCE;

    protected override float DefaultTime => DefaultSettings.ROTTING_TIME;

    public Tier2CorpseSettingsRotting() => SetDefaults();
}
