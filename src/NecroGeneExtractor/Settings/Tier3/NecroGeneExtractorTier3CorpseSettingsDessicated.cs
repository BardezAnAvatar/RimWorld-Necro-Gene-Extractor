namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier3;

public class NecroGeneExtractorTier3CorpseSettingsDessicated
    : NecroGeneExtractorCorpseSettingsNonFresh
{
    protected override bool DefaultAccept => DefaultSettings.TIER3_DESSICATED_ACCEPT;
}
