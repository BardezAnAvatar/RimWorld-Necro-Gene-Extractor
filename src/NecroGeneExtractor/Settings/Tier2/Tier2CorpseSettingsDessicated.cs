namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier2;

public class Tier2CorpseSettingsDessicated : CorpseSettingsNonFresh
{
    protected override bool DefaultAccept => DefaultSettings.TIER2_DESSICATED_ACCEPT;

    protected override float DefaultResource => DefaultSettings.DESSICATED_RESOURCE;

    protected override float DefaultTime => DefaultSettings.DESSICATED_TIME;

    public Tier2CorpseSettingsDessicated() => SetDefaults();
}
