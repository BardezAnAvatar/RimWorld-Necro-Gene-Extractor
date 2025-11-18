using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier3;

public class Tier3Settings: TierSettings
{
    protected Tier3CorpseSettingsFresh fresh;
    protected Tier3CorpseSettingsRotting rotting;
    protected Tier3CorpseSettingsDessicated dessicated;

    public override CorpseSettingsFresh Fresh => fresh;

    public override CorpseSettingsNonFresh Rotting => rotting;

    public override CorpseSettingsNonFresh Dessicated => dessicated;

    public Tier3Settings() => Initialize();

    protected override void Initialize()
    {
        fresh ??= new();
        rotting ??= new();
        dessicated ??= new();
    }

    public override void ExposeData()
    {
        Initialize();
        Scribe_Deep.Look(ref fresh, nameof(Fresh));
        Scribe_Deep.Look(ref rotting, nameof(Rotting));
        Scribe_Deep.Look(ref dessicated, nameof(Dessicated));
    }
}
