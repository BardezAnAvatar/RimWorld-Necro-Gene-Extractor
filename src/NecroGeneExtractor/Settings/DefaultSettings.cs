namespace Bardez.Biotech.NecroGeneExtractor.Settings;

internal static class DefaultSettings
{
    /// <summary>Amount of Neutroamine consumed per hour</summary>
    public const float FRESH_RESOURCE = 5f;

    /// <summary>Processing time of a single cycle of the corpse gene extraction, in hours</summary>
    public const float FRESH_TIME = 24f;

    public const float MULTIPLIER_RESOURCE_ROTTING = 3f;
    public const float MULTIPLIER_RESOURCE_DESSICATED = 5f;
    public const float MULTIPLIER_TIME_ROTTING = 6f;
    public const float MULTIPLIER_TIME_DESSICATED = 10f;

    public static class Tier2
    {
        public const bool ACCEPT_ROTTING = false;
        public const bool ACCEPT_DESSICATED = false;
        public const float MULTIPLIER_TIME = 1f; //base speed
        public const float MULTIPLIER_RESOURCE = 1f; //base consumption
        public const float MULTIPLIER_OVERDRIVE_TIME = 3f; //base overdrive speed-up
        public const float MULTIPLIER_OVERDRIVE_RESOURCE = 2f; //base overdrive consumption
    }

    public static class Tier3
    {
        public const bool ACCEPT_ROTTING = true;
        public const bool ACCEPT_DESSICATED = false;
        public const float MULTIPLIER_TIME = 0.8f; //125% faster
        public const float MULTIPLIER_RESOURCE = 2f; //200% consumption
        public const float MULTIPLIER_OVERDRIVE_TIME = 3f; //overdrive speed-up
        public const float MULTIPLIER_OVERDRIVE_RESOURCE = 2f; //overdrive consumption
    }

    public static class Tier4
    {
        public const bool ACCEPT_ROTTING = true;
        public const bool ACCEPT_DESSICATED = true;
        public const float MULTIPLIER_TIME = 0.4f; //250% faster
        public const float MULTIPLIER_RESOURCE = 4f; //400% consumption
        public const float MULTIPLIER_OVERDRIVE_TIME = 3f; //base overdrive speed-up
        public const float MULTIPLIER_OVERDRIVE_RESOURCE = 2f; //base overdrive consumption
    }
}
