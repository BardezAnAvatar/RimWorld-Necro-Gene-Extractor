using System.Text;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public abstract class TierSettings : IExposable
{
    public abstract CorpseSettingsFresh Fresh { get; }

    public abstract CorpseSettingsNonFresh Rotting { get; }

    public abstract CorpseSettingsNonFresh Dessicated { get; }

    public abstract void ExposeData();

    public override string ToString()
    {
        StringBuilder builder = new();
        builder.Append("{");
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

    protected abstract void Initialize();
}
