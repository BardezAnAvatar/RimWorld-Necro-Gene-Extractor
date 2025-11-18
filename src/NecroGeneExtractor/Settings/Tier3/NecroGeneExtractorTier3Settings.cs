namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier3;

public class NecroGeneExtractorTier3Settings() 
    : NecroGeneExtractorTierSettings(
        new NecroGeneExtractorTier3CorpseSettingsFresh(),
        new NecroGeneExtractorTier3CorpseSettingsRotting(),
        new NecroGeneExtractorTier3CorpseSettingsDessicated())
{
    protected override string ClassName => nameof(NecroGeneExtractorTier3Settings);
}
