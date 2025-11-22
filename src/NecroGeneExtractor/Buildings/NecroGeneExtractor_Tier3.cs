using Bardez.Biotech.NecroGeneExtractor.Settings.Tiers;
using UnityEngine;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Buildings;

[StaticConstructorOnStartup]
public class NecroGeneExtractor_Tier3 : NecroGeneExtractor_Base
{
    protected override TierSettings TierSettings => NecroSettings.SettingsTier3;

    public override bool CanExtractArchite => true;

    protected override Graphic TopGraphic
    {
        get
        {
            if (cachedTopGraphic == null)
            {
                cachedTopGraphic = GraphicDatabase.Get<Graphic_Multi>("NextroGeneExtractorTiers/NecroGeneExtractor_Top_Tier3", ShaderDatabase.Transparent, def.graphicData.drawSize, Color.white);
            }
            return cachedTopGraphic;
        }
    }
}
