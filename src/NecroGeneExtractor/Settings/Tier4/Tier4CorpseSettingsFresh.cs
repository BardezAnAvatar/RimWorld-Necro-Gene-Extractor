namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tier4;

public class Tier4CorpseSettingsFresh
    : CorpseSettingsFresh
{
    protected override float DefaultResource => DefaultSettings.TIER4_FRESH_RESOURCE;

    protected override float DefaultTime => DefaultSettings.TIER4_FRESH_TIME;

    public Tier4CorpseSettingsFresh()
    {
        CostResource = DefaultResource;
        CostTime = DefaultTime;
    }
}
