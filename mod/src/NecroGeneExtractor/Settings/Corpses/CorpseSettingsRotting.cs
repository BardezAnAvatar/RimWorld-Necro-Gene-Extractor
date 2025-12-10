namespace Bardez.Biotech.NecroGeneExtractor.Settings.Corpses;

public class CorpseSettingsRotting : CorpseSettingsNonFresh
{
    protected override float DefaultResource => DefaultSettings.MULTIPLIER_RESOURCE_ROTTING;

    protected override float DefaultTime => DefaultSettings.MULTIPLIER_TIME_ROTTING;

    public CorpseSettingsRotting() => SetDefaults();
}
