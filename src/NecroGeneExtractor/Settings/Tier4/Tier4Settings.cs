using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier4;

public class Tier4Settings : TierSettings
{
    protected Tier4CorpseSettingsFresh fresh;
    protected Tier4CorpseSettingsRotting rotting;
    protected Tier4CorpseSettingsDessicated dessicated;

    public override CorpseSettingsFresh Fresh => fresh;

    public override CorpseSettingsNonFresh Rotting => rotting;

    public override CorpseSettingsNonFresh Dessicated => dessicated;

    public Tier4Settings() => Initialize();

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
