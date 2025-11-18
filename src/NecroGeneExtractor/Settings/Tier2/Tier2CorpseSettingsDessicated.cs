namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier2;

public class Tier2CorpseSettingsDessicated
    : CorpseSettingsNonFresh
{
    protected override bool DefaultAccept => DefaultSettings.TIER2_DESSICATED_ACCEPT;
}
