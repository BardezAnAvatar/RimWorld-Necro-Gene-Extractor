namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier3;

public class Tier3CorpseSettingsDessicated
    : CorpseSettingsNonFresh
{
    protected override bool DefaultAccept => DefaultSettings.TIER3_DESSICATED_ACCEPT;
}
