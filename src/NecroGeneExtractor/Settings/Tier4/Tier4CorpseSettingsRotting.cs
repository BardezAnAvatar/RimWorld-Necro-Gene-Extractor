namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier4;

public class Tier4CorpseSettingsRotting : CorpseSettingsNonFresh
{
    protected override bool DefaultAccept => DefaultSettings.TIER4_ROTTING_ACCEPT;

    protected override float DefaultResource => DefaultSettings.ROTTING_RESOURCE;

    protected override float DefaultTime => DefaultSettings.ROTTING_TIME;

    public Tier4CorpseSettingsRotting() => SetDefaults();
}
