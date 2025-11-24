using Bardez.Biotech.NecroGeneExtractor.Settings.Tiers;
using UnityEngine;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Buildings;

[StaticConstructorOnStartup]
public class NecroGeneExtractor_Tier4 : NecroGeneExtractor_Base
{
    protected override TierSettings TierSettings => NecroSettings.SettingsTier4;

    public override bool CanExtractArchite => true;

    public override bool CanTargetExtraction => true;

    protected override Graphic TopGraphic
    {
        get
        {
            if (cachedTopGraphic == null)
            {
                cachedTopGraphic = GraphicDatabase.Get<Graphic_Multi>("NextroGeneExtractorTiers/NecroGeneExtractor_Top_Tier4", ShaderDatabase.Transparent, def.graphicData.drawSize, Color.white);
            }
            return cachedTopGraphic;
        }
    }
}
