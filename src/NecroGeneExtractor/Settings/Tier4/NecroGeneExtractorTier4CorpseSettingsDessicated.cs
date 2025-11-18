namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier4;

public class NecroGeneExtractorTier4CorpseSettingsDessicated
    : NecroGeneExtractorCorpseSettingsNonFresh
{
    protected override bool DefaultAccept => DefaultSettings.TIER4_DESSICATED_ACCEPT;
}
