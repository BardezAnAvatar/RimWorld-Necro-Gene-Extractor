using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public abstract class NecroGeneExtractorTierSettings(NecroGeneExtractorCorpseSettingsFresh fresh,
    NecroGeneExtractorCorpseSettingsNonFresh rotting,
    NecroGeneExtractorCorpseSettingsNonFresh dessicated) : IExposable, ILoadReferenceable
{
    public NecroGeneExtractorCorpseSettingsFresh Fresh => fresh;

    public NecroGeneExtractorCorpseSettingsNonFresh Rotting => rotting;

    public NecroGeneExtractorCorpseSettingsNonFresh Dessicated => dessicated;

    protected abstract string ClassName { get; }

    public void ExposeData()
    {
        Scribe_References.Look(ref fresh, nameof(Fresh));
        Scribe_References.Look(ref rotting, nameof(Rotting));
        Scribe_References.Look(ref dessicated, nameof(Dessicated));
    }

    public string GetUniqueLoadID() => ClassName + "_" + GetHashCode();
}
