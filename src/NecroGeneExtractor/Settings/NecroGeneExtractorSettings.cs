using Bardez.Biotech.NecroGeneExtractor.Settings.Tier2;
using Bardez.Biotech.NecroGeneExtractor.Settings.Tier3;
using Bardez.Biotech.NecroGeneExtractor.Settings.Tier4;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public class NecroGeneExtractorSettings : ModSettings
{
    public Tier2Settings SettingsTier2;

    public Tier3Settings SettingsTier3;

    public Tier4Settings SettingsTier4;

    public NecroGeneExtractorSettings()
    {
        Initialize();
    }

    protected void Initialize()
    {
        SettingsTier2 ??= new();
        SettingsTier3 ??= new();
        SettingsTier4 ??= new();
    }

    /// <summary>
    /// The part that writes our settings to a file.
    /// Note that saving is by ref. Labels cannot use spaces (xml tags)
    /// </summary>
    public override void ExposeData()
    {
        Initialize();
        base.ExposeData();
        Scribe_Deep.Look(ref SettingsTier2, nameof(SettingsTier2));
        Scribe_Deep.Look(ref SettingsTier3, nameof(SettingsTier3));
        Scribe_Deep.Look(ref SettingsTier4, nameof(SettingsTier4));
    }
}
