namespace Bardez.Biotech.NecroGeneExtractor.Settings.Tiers;

public class Tier3Settings : TierSettings
{
    protected override bool DefaultAcceptRotten => DefaultSettings.Tier3.ACCEPT_ROTTING;

    protected override bool DefaultAcceptDesiccated => DefaultSettings.Tier3.ACCEPT_DESSICATED;

    protected override float DefaultMultiplierResource => DefaultSettings.Tier3.MULTIPLIER_RESOURCE;

    protected override float DefaultMultiplierTime => DefaultSettings.Tier3.MULTIPLIER_TIME;

    protected override float DefaultMultiplierOverdriveResource => DefaultSettings.Tier3.MULTIPLIER_OVERDRIVE_RESOURCE;

    protected override float DefaultMultiplierOverdriveTime => DefaultSettings.Tier3.MULTIPLIER_OVERDRIVE_TIME;

    public Tier3Settings() => SetDefaults();
}
