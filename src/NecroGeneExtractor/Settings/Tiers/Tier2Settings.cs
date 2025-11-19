namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tiers;

public class Tier2Settings : TierSettings
{
    protected override bool DefaultAcceptRotten => DefaultSettings.Tier2.ACCEPT_ROTTING;

    protected override bool DefaultAcceptDesiccated => DefaultSettings.Tier2.ACCEPT_DESSICATED;

    protected override float DefaultMultiplierResource => DefaultSettings.Tier2.MULTIPLIER_RESOURCE;

    protected override float DefaultMultiplierTime => DefaultSettings.Tier2.MULTIPLIER_TIME;

    protected override float DefaultMultiplierOverdriveResource => DefaultSettings.Tier2.MULTIPLIER_OVERDRIVE_RESOURCE;

    protected override float DefaultMultiplierOverdriveTime => DefaultSettings.Tier2.MULTIPLIER_OVERDRIVE_TIME;

    public Tier2Settings() => SetDefaults();
}
