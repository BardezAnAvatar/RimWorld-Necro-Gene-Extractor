namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier4;

public class Tier4CorpseSettingsRotting
    : CorpseSettingsNonFresh
{
    protected override bool DefaultAccept => DefaultSettings.TIER4_ROTTING_ACCEPT;
}
