using System.Text;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings.Corpses;

public abstract class CorpseSettingsNonFresh : IExposable
{
    public float CostMultiplierResource;

    public float CostMultiplierTime;

    protected abstract float DefaultResource { get; }

    protected abstract float DefaultTime { get; }

    public virtual void SetDefaults()
    {
        CostMultiplierResource = DefaultResource;
        CostMultiplierTime = DefaultTime;
    }

    public virtual void ExposeData()
    {
        Scribe_Values.Look(ref CostMultiplierResource, nameof(CostMultiplierResource), DefaultResource);
        Scribe_Values.Look(ref CostMultiplierTime, nameof(CostMultiplierTime), DefaultTime);
    }

    public override string ToString()
    {
        StringBuilder builder = new();
        builder.Append("{");
        builder.Append(nameof(CostMultiplierResource));
        builder.Append(": ");
        builder.Append(CostMultiplierResource);
        builder.Append(", ");
        builder.Append(nameof(CostMultiplierTime));
        builder.Append(": ");
        builder.Append(CostMultiplierTime);
        builder.Append(", ");
        builder.Append('(');
        builder.Append(nameof(DefaultResource));
        builder.Append(": ");
        builder.Append(DefaultResource);
        builder.Append(", ");
        builder.Append(nameof(DefaultTime));
        builder.Append(": ");
        builder.Append(DefaultTime);
        builder.Append(')');
        builder.Append("}");
        return builder.ToString();
    }
}
