using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier3;

public class Tier3Settings: TierSettings
{
    protected Tier3CorpseSettingsFresh fresh = new();
    protected Tier3CorpseSettingsRotting rotting = new();
    protected Tier3CorpseSettingsDessicated dessicated = new();

    public override CorpseSettingsFresh Fresh => fresh;

    public override CorpseSettingsNonFresh Rotting => rotting;

    public override CorpseSettingsNonFresh Dessicated => dessicated;

    protected override string ClassName => nameof(Tier3Settings);

    public override void ExposeData()
    {
        Scribe_Deep.Look(ref fresh, nameof(Fresh));
        Scribe_Deep.Look(ref rotting, nameof(Rotting));
        Scribe_Deep.Look(ref dessicated, nameof(Dessicated));
    }
}
