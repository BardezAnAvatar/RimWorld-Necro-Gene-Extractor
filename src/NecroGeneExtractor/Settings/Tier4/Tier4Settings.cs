using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier4;

public class Tier4Settings : TierSettings
{
    protected Tier4CorpseSettingsFresh fresh = new();
    protected Tier4CorpseSettingsRotting rotting = new();
    protected Tier4CorpseSettingsDessicated dessicated = new();

    public override CorpseSettingsFresh Fresh => fresh;

    public override CorpseSettingsNonFresh Rotting => rotting;

    public override CorpseSettingsNonFresh Dessicated => dessicated;

    protected override string ClassName => nameof(Tier4Settings);

    public override void ExposeData()
    {
        Scribe_Deep.Look(ref fresh, nameof(Fresh));
        Scribe_Deep.Look(ref rotting, nameof(Rotting));
        Scribe_Deep.Look(ref dessicated, nameof(Dessicated));
    }
}
