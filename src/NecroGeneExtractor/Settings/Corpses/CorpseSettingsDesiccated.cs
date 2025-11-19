namespace Bardez.Biotech.NecroGeneExtractor.Settings.Corpses;

public class CorpseSettingsDesiccated : CorpseSettingsNonFresh
{
    protected override float DefaultResource => DefaultSettings.MULTIPLIER_RESOURCE_DESSICATED;

    protected override float DefaultTime => DefaultSettings.MULTIPLIER_TIME_DESSICATED;

    public CorpseSettingsDesiccated() => SetDefaults();
}
