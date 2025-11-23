using System.Text;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tiers;

public abstract class TierSettings : IExposable
{
    public bool AcceptRotten;

    public bool AcceptDessicated;

    public float CostMultiplierResource;

    public float CostMultiplierTime;

    public float CostMultiplierOverdriveResource;

    public float CostMultiplierOverdriveTime;

    protected abstract bool DefaultAcceptRotten { get; }

    protected abstract bool DefaultAcceptDessicated { get; }

    protected abstract float DefaultMultiplierResource { get; }

    protected abstract float DefaultMultiplierTime { get; }

    protected abstract float DefaultMultiplierOverdriveResource { get; }

    protected abstract float DefaultMultiplierOverdriveTime { get; }

    public virtual void ExposeData()
    {
        Scribe_Values.Look(ref AcceptRotten, nameof(AcceptRotten), DefaultAcceptRotten);
        Scribe_Values.Look(ref AcceptDessicated, nameof(AcceptDessicated), DefaultAcceptDessicated);
        Scribe_Values.Look(ref CostMultiplierResource, nameof(CostMultiplierResource), DefaultMultiplierResource);
        Scribe_Values.Look(ref CostMultiplierTime, nameof(CostMultiplierTime), DefaultMultiplierTime);
        Scribe_Values.Look(ref CostMultiplierOverdriveResource, nameof(CostMultiplierOverdriveResource), DefaultMultiplierOverdriveResource);
        Scribe_Values.Look(ref CostMultiplierOverdriveTime, nameof(CostMultiplierOverdriveTime), DefaultMultiplierOverdriveTime);
    }

    public override string ToString()
    {
        StringBuilder builder = new();
        builder.Append("{");
        builder.Append(nameof(AcceptRotten));
        builder.Append(": ");
        builder.Append(AcceptRotten);
        builder.Append(", ");
        builder.Append(nameof(AcceptDessicated));
        builder.Append(": ");
        builder.Append(AcceptDessicated);
        builder.Append(", ");
        builder.Append(nameof(CostMultiplierResource));
        builder.Append(": ");
        builder.Append(CostMultiplierResource);
        builder.Append(", ");
        builder.Append(nameof(CostMultiplierTime));
        builder.Append(": ");
        builder.Append(CostMultiplierTime);
        builder.Append(", ");
        builder.Append(nameof(CostMultiplierOverdriveResource));
        builder.Append(": ");
        builder.Append(CostMultiplierOverdriveResource);
        builder.Append(", ");
        builder.Append(nameof(CostMultiplierOverdriveTime));
        builder.Append(": ");
        builder.Append(CostMultiplierOverdriveTime);
        builder.Append(", (");
        builder.Append(nameof(DefaultAcceptRotten));
        builder.Append(": ");
        builder.Append(DefaultAcceptRotten);
        builder.Append(", ");
        builder.Append(nameof(DefaultAcceptDessicated));
        builder.Append(": ");
        builder.Append(DefaultAcceptDessicated);
        builder.Append(", ");
        builder.Append(nameof(DefaultMultiplierResource));
        builder.Append(": ");
        builder.Append(DefaultMultiplierResource);
        builder.Append(", ");
        builder.Append(nameof(DefaultMultiplierTime));
        builder.Append(": ");
        builder.Append(DefaultMultiplierTime);
        builder.Append(", ");
        builder.Append(nameof(DefaultMultiplierOverdriveResource));
        builder.Append(": ");
        builder.Append(DefaultMultiplierOverdriveResource);
        builder.Append(", ");
        builder.Append(nameof(DefaultMultiplierOverdriveTime));
        builder.Append(": ");
        builder.Append(DefaultMultiplierOverdriveTime);
        builder.Append(")}");
        return builder.ToString();
    }

    public virtual void SetDefaults()
    {
        AcceptRotten = DefaultAcceptRotten;
        AcceptDessicated = DefaultAcceptDessicated;
        CostMultiplierResource = DefaultMultiplierResource;
        CostMultiplierTime = DefaultMultiplierTime;
        CostMultiplierOverdriveResource = DefaultMultiplierOverdriveResource;
        CostMultiplierOverdriveTime = DefaultMultiplierOverdriveTime;
    }
}
