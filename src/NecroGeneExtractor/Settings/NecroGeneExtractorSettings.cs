using Bardez.Biotech.NecroGeneExtractor.Settings.Tier2;
using Bardez.Biotech.NecroGeneExtractor.Settings.Tier3;
using Bardez.Biotech.NecroGeneExtractor.Settings.Tier4;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public class NecroGeneExtractorSettings : ModSettings
{
    public NecroGeneExtractorTier2Settings SettingsTier2 = new();

    public NecroGeneExtractorTier3Settings SettingsTier3 = new();

    public NecroGeneExtractorTier4Settings SettingsTier4 = new();

    /// <summary>
    /// The part that writes our settings to a file. Note that saving is by ref. Labels cannot use spaces (xml tags)
    /// </summary>, 
    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_References.Look(ref SettingsTier2, nameof(SettingsTier2));
        Scribe_References.Look(ref SettingsTier3, nameof(SettingsTier3));
        Scribe_References.Look(ref SettingsTier4, nameof(SettingsTier4));
    }
}
