using System.Text;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public abstract class NecroGeneExtractorTierSettings : IExposable
{
    public abstract NecroGeneExtractorCorpseSettingsFresh Fresh { get; }

    public abstract NecroGeneExtractorCorpseSettingsNonFresh Rotting { get; }

    public abstract NecroGeneExtractorCorpseSettingsNonFresh Dessicated { get; }

    public abstract void ExposeData();

    protected abstract string ClassName { get; }

    public override string ToString()
    {
        StringBuilder builder = new();
        builder.Append(ClassName);
        builder.Append(" {");
        builder.Append(nameof(Fresh));
        builder.Append(": ");
        builder.Append(Fresh);
        builder.Append(", ");
        builder.Append(nameof(Rotting));
        builder.Append(": ");
        builder.Append(Rotting);
        builder.Append(", ");
        builder.Append(nameof(Dessicated));
        builder.Append(": ");
        builder.Append(Dessicated);
        builder.Append("}");
        return builder.ToString();
    }
}
