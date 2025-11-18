namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier2;

public class NecroGeneExtractorTier2CorpseSettingsDessicated
    : NecroGeneExtractorCorpseSettingsNonFresh
{
    protected override bool DefaultAccept => DefaultSettings.TIER2_DESSICATED_ACCEPT;
}
