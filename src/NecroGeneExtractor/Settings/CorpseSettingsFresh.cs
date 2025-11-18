using System.Text;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public abstract class CorpseSettingsFresh : IExposable
{
    public float CostResource;

    public float CostTime;

    protected abstract float DefaultResource { get; }

    protected abstract float DefaultTime { get; }

    public virtual void SetDefaults()
    {
        CostResource = DefaultResource;
        CostTime = DefaultTime;
    }

    public void ExposeData()
    {
        Scribe_Values.Look(ref CostResource, nameof(CostResource), DefaultResource);
        Scribe_Values.Look(ref CostTime, nameof(CostTime), DefaultTime);
    }

    public override string ToString()
    {
        StringBuilder builder = new();
        builder.Append("{");
        builder.Append(nameof(CostResource));
        builder.Append(": ");
        builder.Append(CostResource);
        builder.Append(", ");
        builder.Append(nameof(CostTime));
        builder.Append(": ");
        builder.Append(CostTime);
        builder.Append(", ");
        builder.Append('(');
        builder.Append(nameof(CostResource));
        builder.Append(": ");
        builder.Append(CostResource);
        builder.Append(", ");
        builder.Append(nameof(CostTime));
        builder.Append(": ");
        builder.Append(CostTime);
        builder.Append(')');
        builder.Append("}");
        return builder.ToString();
    }
}
