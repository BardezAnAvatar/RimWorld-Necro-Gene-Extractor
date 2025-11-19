using Bardez.Biotech.NecroGeneExtractor.Settings.Tiers;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Buildings;

[StaticConstructorOnStartup]
public class NecroGeneExtractorTier2 : NecroGeneExtractorBase
{
    protected override TierSettings TierSettings => NecroSettings.SettingsTier2;
}
