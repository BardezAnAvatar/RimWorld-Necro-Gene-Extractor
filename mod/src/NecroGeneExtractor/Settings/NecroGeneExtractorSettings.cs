using Bardez.Biotech.NecroGeneExtractor.Settings.Corpses;
using Bardez.Biotech.NecroGeneExtractor.Settings.Tiers;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public class NecroGeneExtractorSettings : ModSettings
{
    public CorpseSettingsFresh CorpseFresh;

    public CorpseSettingsRotting CorpseRotting;

    public CorpseSettingsDessicated CorpseDessicated;

    public Tier2Settings SettingsTier2;

    public Tier3Settings SettingsTier3;

    public Tier4Settings SettingsTier4;

    public NecroGeneExtractorSettings() => Initialize();

    protected void Initialize()
    {
        CorpseFresh ??= new();
        CorpseRotting ??= new();
        CorpseDessicated ??= new();
        SettingsTier2 ??= new();
        SettingsTier3 ??= new();
        SettingsTier4 ??= new();
    }

    public void SetDefaults()
    {
        CorpseFresh.SetDefaults();
        CorpseRotting.SetDefaults();
        CorpseDessicated.SetDefaults();
        SettingsTier2.SetDefaults();
        SettingsTier3.SetDefaults();
        SettingsTier4.SetDefaults();
    }

    /// <summary>
    /// The part that writes our settings to a file.
    /// Note that saving is by ref. Labels cannot use spaces (xml tags)
    /// </summary>
    public override void ExposeData()
    {
        Initialize();
        base.ExposeData();
        Scribe_Deep.Look(ref CorpseFresh, nameof(CorpseFresh));
        Scribe_Deep.Look(ref CorpseRotting, nameof(CorpseRotting));
        Scribe_Deep.Look(ref CorpseDessicated, nameof(CorpseDessicated));
        Scribe_Deep.Look(ref SettingsTier2, nameof(SettingsTier2));
        Scribe_Deep.Look(ref SettingsTier3, nameof(SettingsTier3));
        Scribe_Deep.Look(ref SettingsTier4, nameof(SettingsTier4));
    }
}
