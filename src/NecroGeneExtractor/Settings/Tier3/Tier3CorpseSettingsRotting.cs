namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier3;

public class Tier3CorpseSettingsRotting
    : CorpseSettingsNonFresh
{
    protected override bool DefaultAccept => DefaultSettings.TIER3_ROTTING_ACCEPT;
}
