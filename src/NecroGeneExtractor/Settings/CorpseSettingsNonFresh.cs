using System.Text;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public abstract class CorpseSettingsNonFresh : IExposable
{
    public bool Accept;

    public float CostMultiplierResource;

    public float CostMultiplierTime;

    protected abstract bool DefaultAccept { get; }

    protected virtual float DefaultResource => DefaultSettings.ROTTING_RESOURCE;

    protected virtual float DefaultTime => DefaultSettings.ROTTING_TIME;

    public virtual void SetDefaults()
    {
        Accept = DefaultAccept;
        CostMultiplierResource = DefaultResource;
        CostMultiplierTime = DefaultTime;
    }

    public virtual void ExposeData()
    {
        Scribe_Values.Look(ref Accept, nameof(Accept), DefaultAccept);
        Scribe_Values.Look(ref CostMultiplierResource, nameof(CostMultiplierResource), DefaultResource);
        Scribe_Values.Look(ref CostMultiplierTime, nameof(CostMultiplierTime), DefaultTime);
    }

    public override string ToString()
    {
        StringBuilder builder = new();
        builder.Append("{");
        builder.Append(nameof(Accept));
        builder.Append(": ");
        builder.Append(Accept);
        builder.Append(", ");
        builder.Append(nameof(CostMultiplierResource));
        builder.Append(": ");
        builder.Append(CostMultiplierResource);
        builder.Append(", ");
        builder.Append(nameof(CostMultiplierTime));
        builder.Append(": ");
        builder.Append(CostMultiplierTime);
        builder.Append(", ");
        builder.Append('(');
        builder.Append(nameof(DefaultAccept));
        builder.Append(": ");
        builder.Append(DefaultAccept);
        builder.Append(", ");
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
