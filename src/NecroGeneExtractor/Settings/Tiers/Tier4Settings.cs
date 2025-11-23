namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tiers;

public class Tier4Settings : TierSettings
{
    protected override bool DefaultAcceptRotten => DefaultSettings.Tier4.ACCEPT_ROTTING;

    protected override bool DefaultAcceptDessicated => DefaultSettings.Tier4.ACCEPT_DESSICATED;

    protected override float DefaultMultiplierResource => DefaultSettings.Tier4.MULTIPLIER_RESOURCE;

    protected override float DefaultMultiplierTime => DefaultSettings.Tier4.MULTIPLIER_TIME;

    protected override float DefaultMultiplierOverdriveResource => DefaultSettings.Tier4.MULTIPLIER_OVERDRIVE_RESOURCE;

    protected override float DefaultMultiplierOverdriveTime => DefaultSettings.Tier4.MULTIPLIER_OVERDRIVE_TIME;

    public Tier4Settings() => SetDefaults();
}
