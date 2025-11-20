using Bardez.Biotech.NecroGeneExtractor.Settings.Tiers;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Buildings;

[StaticConstructorOnStartup]
public class NecroGeneExtractor_Tier2 : NecroGeneExtractor_Base
{
    protected override TierSettings TierSettings => NecroSettings.SettingsTier2;
}
