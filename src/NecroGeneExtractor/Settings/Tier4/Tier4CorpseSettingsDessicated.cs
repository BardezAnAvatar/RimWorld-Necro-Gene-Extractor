namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier4;

public class Tier4CorpseSettingsDessicated
    : CorpseSettingsNonFresh
{
    protected override bool DefaultAccept => DefaultSettings.TIER4_DESSICATED_ACCEPT;

    public Tier4CorpseSettingsDessicated() => SetDefaults();
}
