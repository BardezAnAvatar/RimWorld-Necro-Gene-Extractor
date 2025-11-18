using Bardez.Biotech.NecroGeneExtractor.Utilities;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public abstract class NecroGeneExtractorCorpseSettingsFresh
    : IExposable, ILoadReferenceable
{
    public float CostResource;

    public float CostTime;

    protected abstract float DefaultResource { get; }

    protected abstract float DefaultTime { get; }

    public void ExposeData()
    {
        Scribe_Values.Look(ref CostResource, nameof(CostResource), DefaultResource);
        Scribe_Values.Look(ref CostTime, nameof(CostTime), DefaultTime);
        DebugMessaging.DebugMessage($"{nameof(CostTime)} changed: {CostTime}");
    }

    public string GetUniqueLoadID() => nameof(NecroGeneExtractorCorpseSettingsFresh) + "_" + GetHashCode();
}
