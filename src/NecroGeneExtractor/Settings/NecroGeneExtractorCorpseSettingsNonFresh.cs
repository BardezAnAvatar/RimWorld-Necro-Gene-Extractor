using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

public class NecroGeneExtractorCorpseSettingsNonFresh(bool accept, float multiplierResource, float multiplierTime)
    : NecroGeneExtractorCorpseSettingsFresh(multiplierResource, multiplierTime)
{
    public bool Accept = accept;

    public override void ExposeData(string prefix)
    {
        Scribe_Values.Look(ref Accept, $"{prefix}.{nameof(Accept)}", Accept);
        base.ExposeData(prefix);
    }
}
