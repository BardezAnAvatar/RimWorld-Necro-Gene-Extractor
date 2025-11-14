using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public class NecroGeneExtractorSettings : ModSettings
{
    private const float defaultCostTimeMultipierRotting = 6f;
    private const float defaultCostTimeMultipierDessicated = 10f;
    private const float defaultCostResourceMultiplierRotting = 3f;
    private const float defaultCostResourceMultiplierDessicated = 5f;

    public float CostResourceFresh = 5f;
    public float CostTimeFresh = 3f;

    private readonly NecroGeneExtractorTierSettings _settingsTier2 
        = new(new(5f, 6f), new(false, defaultCostResourceMultiplierRotting, defaultCostTimeMultipierRotting), new(false, defaultCostResourceMultiplierDessicated, defaultCostTimeMultipierDessicated));
    private readonly NecroGeneExtractorTierSettings _settingsTier3
        = new(new(4f, 4.5f), new(true, defaultCostResourceMultiplierRotting, defaultCostTimeMultipierRotting), new(false, defaultCostResourceMultiplierDessicated, defaultCostTimeMultipierDessicated));
    private readonly NecroGeneExtractorTierSettings _settingsTier4
        = new(new(3f, 2.5f), new(true, defaultCostResourceMultiplierRotting, defaultCostTimeMultipierRotting), new(true, defaultCostResourceMultiplierDessicated, defaultCostTimeMultipierDessicated));

    public NecroGeneExtractorTierSettings SettingsTier2 => _settingsTier2;
    public NecroGeneExtractorTierSettings SettingsTier3 => _settingsTier3;
    public NecroGeneExtractorTierSettings SettingsTier4 => _settingsTier4;

    /// <summary>
    /// The part that writes our settings to a file. Note that saving is by ref. Labels cannot use spaces (xml tags)
    /// </summary>
    public override void ExposeData()
    {
        _settingsTier2.ExposeData(nameof(SettingsTier2));
        _settingsTier2.ExposeData(nameof(SettingsTier3));
        _settingsTier2.ExposeData(nameof(SettingsTier4));
    }
}
