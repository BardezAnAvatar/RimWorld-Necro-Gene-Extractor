namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier4;

public class Tier4CorpseSettingsDessicated
    : CorpseSettingsNonFresh
{
    protected override bool DefaultAccept => DefaultSettings.TIER4_DESSICATED_ACCEPT;

    protected override float DefaultResource => DefaultSettings.DESSICATED_RESOURCE;

    protected override float DefaultTime => DefaultSettings.DESSICATED_TIME;

    public Tier4CorpseSettingsDessicated() => SetDefaults();
}
