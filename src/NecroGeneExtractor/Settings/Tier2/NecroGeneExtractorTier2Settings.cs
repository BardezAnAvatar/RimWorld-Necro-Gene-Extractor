namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier2;

public class NecroGeneExtractorTier2Settings() 
    : NecroGeneExtractorTierSettings(
        new NecroGeneExtractorTier2CorpseSettingsFresh(),
        new NecroGeneExtractorTier2CorpseSettingsRotting(),
        new NecroGeneExtractorTier2CorpseSettingsDessicated())
{
    protected override string ClassName => nameof(NecroGeneExtractorTier2Settings);
}
