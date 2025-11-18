using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier2;

public class Tier2Settings : TierSettings
{
    protected Tier2CorpseSettingsFresh fresh;
    protected Tier2CorpseSettingsRotting rotting;
    protected Tier2CorpseSettingsDessicated dessicated;

    public override CorpseSettingsFresh Fresh => fresh;

    public override CorpseSettingsNonFresh Rotting => rotting;

    public override CorpseSettingsNonFresh Dessicated => dessicated;

    public Tier2Settings()
    {
        Initialize();
    }

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
