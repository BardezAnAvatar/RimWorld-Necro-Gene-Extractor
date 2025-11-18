namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier4;

public class NecroGeneExtractorTier4Settings() 
    : NecroGeneExtractorTierSettings(
        new NecroGeneExtractorTier4CorpseSettingsFresh(),
        new NecroGeneExtractorTier4CorpseSettingsRotting(),
        new NecroGeneExtractorTier4CorpseSettingsDessicated())
{
    protected override string ClassName => nameof(NecroGeneExtractorTier4Settings);
}
