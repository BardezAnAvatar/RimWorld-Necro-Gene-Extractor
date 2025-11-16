using UnityEngine;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

internal static class WindowDrawing
{
    private const float SCROLL_BAR_WIDTH_MARGIN = 20f;
    private const float SCROLLABLE_AREA_HEIGHT = 9999f;

    private const float SECTION_HEIGHT_FRESH = 200f;
    private const float SECTION_HEIGHT_NONFRESH = 300f;
    private const float SECTION_HEIGHT_TIER = 600f;

    private const float TIER_AREA_LINES = 8f;
    private const float HOURS_MIN = 1f;
    private const float HOURS_MAX = 24f;
    private const float NEUTROAMINE_MIN = 2f;
    private const float NEUTROAMINE_MAX = 50f;

    private const float LINE_HEIGHT_LABEL = 1.3f;
    private const float LINE_HEIGHT_CHECKBOX = 1.4f;
    private const float LINE_HEIGHT_SLIDER = 1.3f;

    private static Vector2 _scrollPosition = new(0f, 0f);
    private static float _totalContentHeight = 800f;

    public static void DrawSettings_Variables(Rect settingsArea, NecroGeneExtractorSettings settings)
    {
        bool scrollBarVisible = _totalContentHeight > settingsArea.height;

        Rect scrollViewTotal = new Rect(0f, 0f, settingsArea.width - (scrollBarVisible ? SCROLL_BAR_WIDTH_MARGIN : 0f), _totalContentHeight);
        Widgets.BeginScrollView(settingsArea, ref _scrollPosition, scrollViewTotal);

        Rect viewRect = new Rect(0f, 0f, scrollViewTotal.width, SCROLLABLE_AREA_HEIGHT);

        // Create the generic listing, which we'll fill with our settings.
        Listing_Standard listing = new Listing_Standard();
        listing.Begin(viewRect);

        DrawSettingsTier(listing, viewRect.width, "NGET_ExtractorTier2", settings.SettingsTier2);
        DrawGapBetweenSections(listing);
        DrawSettingsTier(listing, viewRect.width, "NGET_ExtractorTier3", settings.SettingsTier3);
        DrawGapBetweenSections(listing);
        DrawSettingsTier(listing, viewRect.width, "NGET_ExtractorTier4", settings.SettingsTier4);

        _totalContentHeight = listing.CurHeight;
        listing.End();

        Widgets.EndScrollView();
    }

    private static void DrawSettingsTier(Listing_Standard parent, float width, string tierName, NecroGeneExtractorTierSettings tierSettings)
    {
        Listing_Standard subSection = BeginSubSection(parent, SECTION_HEIGHT_TIER, width: width);

        subSection.Label(tierName.Translate());
        DrawSettingsFresh(subSection, width, ref tierSettings.Fresh.CostMultiplierTime, ref tierSettings.Fresh.CostMultiplierResource);
        DrawGapBetweenSections(subSection);
        DrawSettingsNonFresh(subSection, width, "RotStateRotting", ref tierSettings.Rotting.Accept, ref tierSettings.Rotting.CostMultiplierTime, ref tierSettings.Rotting.CostMultiplierResource);
        DrawGapBetweenSections(subSection);
        DrawSettingsNonFresh(subSection, width, "RotStateDessicated", ref tierSettings.Dessicated.Accept, ref tierSettings.Dessicated.CostMultiplierTime, ref tierSettings.Dessicated.CostMultiplierResource);

        parent.EndSection(subSection);
    }

    private static void DrawSettingsFresh(Listing_Standard parent, float width, ref float hours, ref float neutroamine)
    {
        Listing_Standard subSection = BeginSubSection(parent, SECTION_HEIGHT_FRESH, width);

        subSection.Label("RotStateFresh".Translate()); //base game string
        hours = subSection.SliderLabeled("NGET_WorkHours".Translate(), hours, HOURS_MIN, HOURS_MAX, tooltip: "NGET_WorkHoursTooltip".Translate());
        neutroamine = subSection.SliderLabeled("NGET_CostNeutroamine".Translate(), hours, NEUTROAMINE_MIN, NEUTROAMINE_MAX, tooltip: "NGET_CostNeutroamineTooltip".Translate());

        parent.EndSection(subSection);
    }

    private static void DrawSettingsNonFresh(Listing_Standard parent, float width, string corpseType, ref bool enabled, ref float hours, ref float neutroamine)
    {
        Listing_Standard subSection = BeginSubSection(parent, SECTION_HEIGHT_NONFRESH, width);

        subSection.Label(corpseType.Translate()); //base game string
        subSection.CheckboxLabeled("NGET_CorpseTypeEnabled".Translate(), ref enabled, tooltip: "NGET_CorpseTypeEnabledTooltip"); //base game string

        if (enabled)
        {
            hours = subSection.SliderLabeled("NGET_WorkHours".Translate(), hours, HOURS_MIN, HOURS_MAX, tooltip: "NGET_WorkHoursTooltip".Translate());
            neutroamine = subSection.SliderLabeled("NGET_CostNeutroamine".Translate(), hours, NEUTROAMINE_MIN, NEUTROAMINE_MAX, tooltip: "NGET_CostNeutroamineTooltip".Translate());
        }

        parent.EndSection(subSection);
    }

    public static Listing_Standard BeginSubSection(Listing_Standard parent, float height, float width, float sectionBorder = 4f, float bottomBorder = 4f)
    {
        Rect rect = parent.GetRect(height + sectionBorder + bottomBorder);
        rect.width = width - sectionBorder;
        Widgets.DrawMenuSection(rect);
        Listing_Standard listing_Standard = new Listing_Standard();
        Rect rect2 = new Rect(rect.x + sectionBorder, rect.y + sectionBorder, rect.width - sectionBorder * 2f, rect.height - (sectionBorder + bottomBorder));
        listing_Standard.Begin(rect2);

        return listing_Standard;
    }

    public static void DrawGapBetweenSections(Listing_Standard listing)
    {
        listing.Gap(20f);
    }
}
