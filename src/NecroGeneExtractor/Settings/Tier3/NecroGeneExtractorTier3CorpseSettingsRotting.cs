namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier3;

public class NecroGeneExtractorTier3CorpseSettingsRotting
    : NecroGeneExtractorCorpseSettingsNonFresh
{
    protected override bool DefaultAccept => DefaultSettings.TIER3_ROTTING_ACCEPT;
}
