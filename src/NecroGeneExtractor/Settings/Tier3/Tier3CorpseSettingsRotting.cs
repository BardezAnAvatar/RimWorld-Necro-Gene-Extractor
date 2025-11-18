namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier3;

public class Tier3CorpseSettingsRotting : CorpseSettingsNonFresh
{
    protected override bool DefaultAccept => DefaultSettings.TIER3_ROTTING_ACCEPT;

    protected override float DefaultResource => DefaultSettings.ROTTING_RESOURCE;

    protected override float DefaultTime => DefaultSettings.ROTTING_TIME;

    public Tier3CorpseSettingsRotting() => SetDefaults();
}
