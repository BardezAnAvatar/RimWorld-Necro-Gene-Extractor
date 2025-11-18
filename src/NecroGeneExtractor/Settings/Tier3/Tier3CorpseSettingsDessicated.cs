namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier3;

public class Tier3CorpseSettingsDessicated : CorpseSettingsNonFresh
{
    protected override bool DefaultAccept => DefaultSettings.TIER3_DESSICATED_ACCEPT;

    protected override float DefaultResource => DefaultSettings.DESSICATED_RESOURCE;

    protected override float DefaultTime => DefaultSettings.DESSICATED_TIME;

    public Tier3CorpseSettingsDessicated() => SetDefaults();
}
