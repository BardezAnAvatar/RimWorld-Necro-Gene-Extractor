namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier4;

public class NecroGeneExtractorTier4CorpseSettingsRotting
    : NecroGeneExtractorCorpseSettingsNonFresh
{
    protected override bool DefaultAccept => DefaultSettings.TIER4_ROTTING_ACCEPT;
}
