using System.Text;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings.Corpses;

public class CorpseSettingsFresh : IExposable
{
    public float CostResource;

    public float CostTime;

    protected float DefaultResource => DefaultSettings.FRESH_RESOURCE;

    protected float DefaultTime => DefaultSettings.FRESH_TIME;

    public CorpseSettingsFresh() => SetDefaults();

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
