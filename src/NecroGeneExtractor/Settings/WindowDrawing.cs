using Bardez.Biotech.NecroGeneExtractor.Utilities;
using UnityEngine;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Settings;

internal static class WindowDrawing
{
    private const float SCROLL_BAR_WIDTH_MARGIN = 20f;
    private const float SCROLLABLE_AREA_HEIGHT = 9999f;

    private const float SECTION_HEIGHT_TIER = 1000f;

    private const float HOURS_MIN = 1f;
    private const float HOURS_MAX = 24f;
    private const float NEUTROAMINE_MIN = 1f;
    private const float NEUTROAMINE_MAX = 50f;
    private const float INCREMENT_TIME = 0.25f;
    private const float INCREMENT_RESOURCE = 0.25f;

    private const float LINE_HEIGHT_MULTIPIER = 1.35f;

    private const float SECTION_GAP = 20f;
    private const float LINE_MARGIN_VERTICAL = 12f;
    private const float SUBSECTION_PADDING = 2f;

    private static Vector2 _scrollPosition = new(0f, 0f);
    private static float _totalContentHeight = 800f;

    public static void DrawSettings_Variables(Rect settingsArea, NecroGeneExtractorSettings settings)
    {
        bool scrollBarVisible = _totalContentHeight > settingsArea.height;

        Rect scrollViewTotal = new Rect(0f, 0f, settingsArea.width - (scrollBarVisible ? SCROLL_BAR_WIDTH_MARGIN : 0f), _totalContentHeight);
        Widgets.BeginScrollView(settingsArea, ref _scrollPosition, scrollViewTotal);

        try
        {
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
        }
        finally
        {
            Widgets.EndScrollView();
        }
    }

    private static void DrawSettingsTier(Listing_Standard parent, float width, string tierName, TierSettings tierSettings)
    {
        TaggedString header = "<b><color=\"green\">" + tierName.Translate() + "</color></b>";
        float height = GetHeightTierSubsection(tierSettings);
        Listing_Standard subSection = BeginSubSection(parent, height, width: width);
        try
        {
            DrawTierHeader(subSection, header);

            DrawSettingsFresh(subSection, width, ref tierSettings.Fresh.CostTime, ref tierSettings.Fresh.CostResource);
            DrawGapBetweenSections(subSection);
            DrawSettingsNonFresh(subSection, width, "RotStateRotting", ref tierSettings.Rotting.Accept, ref tierSettings.Rotting.CostMultiplierTime, ref tierSettings.Rotting.CostMultiplierResource);
            DrawGapBetweenSections(subSection);
            DrawSettingsNonFresh(subSection, width, "RotStateDessicated", ref tierSettings.Dessicated.Accept, ref tierSettings.Dessicated.CostMultiplierTime, ref tierSettings.Dessicated.CostMultiplierResource);
        }
        finally
        {
            parent.EndSection(subSection);
        }
    }

    private static void DrawTierHeader(Listing_Standard subSection, TaggedString header)
    {
        // Make section text bigger
        var previousFont = Text.Font;
        Text.Font = GameFont.Medium;
        subSection.Label(header);
        Text.Font = previousFont;
    }

    private static void DrawSettingsFresh(Listing_Standard parent, float width, ref float hours, ref float neutroamine)
    {
        var height = GetHeightCorpseTypeFresh();
        Listing_Standard subSection = BeginSubSection(parent, height, width);
        try
        {
            DrawCorpseTypeHeader(subSection, "RotStateFresh");
            DrawHoursAndNeutroamine(subSection, ref hours, ref neutroamine,
                "NGET_WorkHours", "NGET_WorkHoursTooltip", "NGET_CostNeutroamine", "NGET_CostNeutroamineTooltip");
        }
        finally
        {
            parent.EndSection(subSection);
        }
    }

    private static void DrawSettingsNonFresh(Listing_Standard parent, float width, string corpseType, ref bool enabled, ref float hours, ref float neutroamine)
    {
        var height = GetHeightCorpseTypeNonFresh(enabled);
        Listing_Standard subSection = BeginSubSection(parent, height, width);

        try
        {
            DrawCorpseTypeHeader(subSection, corpseType);
            subSection.CheckboxLabeled("NGET_CorpseTypeEnabled".Translate(), ref enabled, tooltip: "NGET_CorpseTypeEnabledTooltip");

            if (enabled)
            {
                DrawHoursAndNeutroamine(subSection, ref hours, ref neutroamine,
                    "NGET_WorkHoursMultiplier", "NGET_WorkHoursMultiplierTooltip",
                    "NGET_CostNeutroamineMultiplier", "NGET_CostNeutroamineMultiplierTooltip");
            }
        }
        finally
        {
            parent.EndSection(subSection);
        }
    }

    private static void DrawCorpseTypeHeader(Listing_Standard subSection, string corpseTypeKey)
    {
        var stateString = corpseTypeKey.Translate(); //base game string
        var corpseString = "NGET_CorpseType".Translate().Formatted(stateString);
        var labelString = "<b>" + corpseString + "</b>";
        subSection.Label(labelString);
    }

    private static void DrawHoursAndNeutroamine(Listing_Standard subSection, ref float hours, ref float neutroamine,
        string hoursLabelKey, string hoursTooltipKey, string neutroLabelKey, string neutroTooltipKey)
    {
        var hoursLabel = hoursLabelKey.Translate().Formatted(hours);
        var hoursTooltip = hoursTooltipKey.Translate();
        hours = subSection.SliderLabeled(hoursLabel, hours, HOURS_MIN, HOURS_MAX, INCREMENT_TIME, tooltip: hoursTooltip);

        var neutroamineLabel = neutroLabelKey.Translate().Formatted(neutroamine);
        var neutroTooltip = neutroTooltipKey.Translate();
        neutroamine = subSection.SliderLabeled(neutroamineLabel, neutroamine, NEUTROAMINE_MIN, NEUTROAMINE_MAX, INCREMENT_RESOURCE, tooltip: neutroTooltip);
    }

    private static Listing_Standard BeginSubSection(Listing_Standard parent, float height, float width, float sectionBorder = 6f, float bottomBorder = 4f)
    {
        Rect rect = parent.GetRect(height + sectionBorder + bottomBorder);
        Widgets.DrawMenuSection(rect);
        Listing_Standard listing_Standard = new Listing_Standard();
        Rect rect2 = new Rect(rect.x + sectionBorder, rect.y + sectionBorder, rect.width - (sectionBorder * 2f), rect.height - (sectionBorder + bottomBorder));
        listing_Standard.Begin(rect2);

        return listing_Standard;
    }

    private static void DrawGapBetweenSections(Listing_Standard listing)
    {
        listing.Gap(SECTION_GAP);
    }

    private static float GetHeightTierSubsection(TierSettings tierSettings)
    {
        var previousFont = Text.Font;

        Text.Font = GameFont.Medium;
        var headerHeight = Text.LineHeight + LINE_MARGIN_VERTICAL;
        Text.Font = previousFont;

        var height = headerHeight
            + SUBSECTION_PADDING + GetHeightCorpseTypeFresh() + SUBSECTION_PADDING
            + SECTION_GAP
            + SUBSECTION_PADDING + GetHeightCorpseTypeNonFresh(tierSettings.Rotting.Accept) + SUBSECTION_PADDING
            + SECTION_GAP
            + SUBSECTION_PADDING + GetHeightCorpseTypeNonFresh(tierSettings.Dessicated.Accept) + SUBSECTION_PADDING
            + SECTION_GAP
         ;

        return height;
    }

    private static float GetHeightCorpseTypeFresh()
    {
        var textLineHeight = Text.LineHeight;
        var height = (Text.LineHeight * LINE_HEIGHT_MULTIPIER) * 3f;

        return height;
    }

    private static float GetHeightCorpseTypeNonFresh(bool enabled)
    {
        var textLineHeight = Text.LineHeight;
        var height = (Text.LineHeight * LINE_HEIGHT_MULTIPIER) * (enabled ? 4f : 2f);

        return height;
    }
}
