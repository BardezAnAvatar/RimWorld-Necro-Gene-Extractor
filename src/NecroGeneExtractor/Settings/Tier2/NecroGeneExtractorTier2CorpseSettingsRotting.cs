namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier2;

public class NecroGeneExtractorTier2CorpseSettingsRotting
    : NecroGeneExtractorCorpseSettingsNonFresh
{
    protected override bool DefaultAccept => DefaultSettings.TIER2_ROTTING_ACCEPT;
}
