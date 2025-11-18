using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public abstract class NecroGeneExtractorCorpseSettingsNonFresh
    : IExposable, ILoadReferenceable
{
    public bool Accept;

    public float CostMultiplierResource;

    public float CostMultiplierTime;

    protected abstract bool DefaultAccept { get; }

    protected virtual float DefaultResource => DefaultSettings.ROTTING_RESOURCE;

    protected virtual float DefaultTime => DefaultSettings.ROTTING_TIME;

    public virtual void ExposeData()
    {
        Scribe_Values.Look(ref Accept, nameof(Accept), DefaultAccept);
        Scribe_Values.Look(ref CostMultiplierResource, nameof(CostMultiplierResource), DefaultResource);
        Scribe_Values.Look(ref CostMultiplierTime, nameof(CostMultiplierTime), DefaultTime);
    }

    public string GetUniqueLoadID() => nameof(NecroGeneExtractorCorpseSettingsNonFresh) + "_" + GetHashCode();
}
