using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier2;

public class Tier2Settings : TierSettings
{
    protected Tier2CorpseSettingsFresh fresh = new();
    protected Tier2CorpseSettingsRotting rotting = new();
    protected Tier2CorpseSettingsDessicated dessicated = new();

    public override CorpseSettingsFresh Fresh => fresh;

    public override CorpseSettingsNonFresh Rotting => rotting;

    public override CorpseSettingsNonFresh Dessicated => dessicated;

    protected override string ClassName => nameof(Tier2Settings);

    public override void ExposeData()
    {
        Scribe_Deep.Look(ref fresh, nameof(Fresh));
        Scribe_Deep.Look(ref rotting, nameof(Rotting));
        Scribe_Deep.Look(ref dessicated, nameof(Dessicated));
    }
}
