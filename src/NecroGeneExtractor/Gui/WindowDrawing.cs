using System;
using Bardez.Biotech.NecroGeneExtractor.Settings;
using Bardez.Biotech.NecroGeneExtractor.Settings.Tiers;
using RimWorld;
using UnityEngine;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Gui;

/// <summary>Class that performs drawing and such via Unity and Rimworld windows</summary>
internal static class WindowDrawing
{
    private const float SCROLL_BAR_WIDTH_MARGIN = 20f;
    private const float SCROLLABLE_AREA_HEIGHT = 9999f;

    private const float HOURS_MIN = 1f;
    private const float HOURS_MAX = 24f;
    private const float NEUTROAMINE_MIN = 1f;
    private const float NEUTROAMINE_MAX = 50f;

    private const float INCREMENT_TIME = 0.25f;
    private const float INCREMENT_RESOURCE = 0.25f;

    private const float SLIDER_LABEL_AREA_PCT = 0.3f;
    private const float SLIDER_VALUE_AREA_PCT = 0.1f;
    private const float SLIDER_LABEL_SEPARATION_PCT = 0.1f;
    private const float BUTTON_DEFAULTS_AREA_PCT = 0.2f;

    private const float LINE_HEIGHT_MULTIPIER = 1.4f;

    private const float SECTION_GAP = 20f;
    private const float LINE_MARGIN_VERTICAL = 12f;
    private const float SUBSECTION_PADDING = 2f;

    private static Vector2 _scrollPosition = new(0f, 0f);
    private static float _totalContentHeight = 800f;

    public static void DrawSettings(Rect settingsArea, NecroGeneExtractorSettings settings)
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

            DrawSettings_DefaultButton(listing, settings.SetDefaults);
            DrawGapBetweenSections(listing);
            DrawSettingsCorpses(listing, viewRect.width, settings);
            DrawGapBetweenSections(listing);
            DrawSettingsTiers(listing, viewRect.width, settings);

            _totalContentHeight = listing.CurHeight;
            listing.End();
        }
        finally
        {
            Widgets.EndScrollView();
        }
    }

    private static void DrawSettings_DefaultButton(Listing_Standard listing, Action restore)
    {
        //did this "Default Settings" button just get pressed?
        if (listing.ButtonText("NGET_RestoreDefaultSettings".Translate(), widthPct: BUTTON_DEFAULTS_AREA_PCT))
        {
            restore();
            Messages.Message("NGET_DefaultSettingsRestored".Translate(), null, MessageTypeDefOf.NeutralEvent, historical: false);
        }
    }

    private static void DrawSettingsCorpses(Listing_Standard parent, float width, NecroGeneExtractorSettings settings)
    {
        TaggedString header = "<b><color=\"green\">" + "NGET_HeaderCorpses".Translate() + "</color></b>";
        float height = GetHeightCorpsesSection();
        Listing_Standard subSection = BeginSubSection(parent, height, width: width);
        try
        {
            DrawHeader(subSection, header);
            DrawSettingsFresh(subSection, width, ref settings.CorpseFresh.CostTime, ref settings.CorpseFresh.CostResource);
            DrawGapBetweenSections(subSection);
            DrawSettingsNonFresh(subSection, width, "RotStateRotting", ref settings.CorpseFresh.CostTime, ref settings.CorpseFresh.CostResource);
            DrawGapBetweenSections(subSection);
            DrawSettingsNonFresh(subSection, width, "RotStateDessicated", ref settings.CorpseFresh.CostTime, ref settings.CorpseFresh.CostResource);
        }
        finally
        {
            parent.EndSection(subSection);
        }
    }

    private static void DrawSettingsTiers(Listing_Standard parent, float width, NecroGeneExtractorSettings settings)
    {
        DrawSettingsTier(parent, width, "NGET_ExtractorTier2", settings.SettingsTier2);
        DrawGapBetweenSections(parent);
        DrawSettingsTier(parent, width, "NGET_ExtractorTier3", settings.SettingsTier3);
        DrawGapBetweenSections(parent);
        DrawSettingsTier(parent, width, "NGET_ExtractorTier4", settings.SettingsTier4);
    }

    private static void DrawSettingsTier(Listing_Standard parent, float width, string tierName, TierSettings tierSettings)
    {
        TaggedString header = "<b><color=\"green\">" + tierName.Translate() + "</color></b>";
        float height = GetHeightTierSubsection();
        Listing_Standard subSection = BeginSubSection(parent, height, width: width);
        try
        {
            DrawHeader(subSection, header);
            subSection.CheckboxLabeled("NGET_CorpseTypeRottingEnabled".Translate(), ref tierSettings.AcceptRotten, tooltip: "NGET_CorpseTypeRottingEnabledTooltip".Translate());
            subSection.CheckboxLabeled("NGET_CorpseTypeDesiccatedEnabled".Translate(), ref tierSettings.AcceptDesiccated, tooltip: "NGET_CorpseTypeDesiccatedEnabledTooltip".Translate());

            DrawHoursAndNeutroamine(subSection, ref tierSettings.CostMultiplierTime, ref tierSettings.CostMultiplierResource,
                "NGET_WorkHoursMultiplier", "NGET_WorkHoursMultiplierTooltip",
                "NGET_CostNeutroamineMultiplier", "NGET_CostNeutroamineMultiplierTooltip");
            DrawHoursAndNeutroamine(subSection, ref tierSettings.CostMultiplierTime, ref tierSettings.CostMultiplierResource,
                "NGET_WorkHoursMultiplierOverdrive", "NGET_WorkHoursMultiplierOverdriveTooltip",
                "NGET_CostNeutroaminMultiplierOverdrive", "NGET_CostNeutroaminMultiplierOverdriveTooltip");
        }
        finally
        {
            parent.EndSection(subSection);
        }
    }

    private static void DrawHeader(Listing_Standard subSection, TaggedString header)
    {
        // Make section text bigger
        var previousFont = Text.Font;
        Text.Font = GameFont.Medium;
        subSection.Label(header);
        Text.Font = previousFont;
    }

    private static void DrawSettingsFresh(Listing_Standard parent, float width, ref float hours, ref float neutroamine)
    {
        var height = GetHeightCorpseType();
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

    private static void DrawSettingsNonFresh(Listing_Standard parent, float width, string corpseType, ref float hours, ref float neutroamine)
    {
        var height = GetHeightCorpseType();
        Listing_Standard subSection = BeginSubSection(parent, height, width);

        try
        {
            DrawCorpseTypeHeader(subSection, corpseType);
            DrawHoursAndNeutroamine(subSection, ref hours, ref neutroamine,
                "NGET_WorkHoursMultiplier", "NGET_WorkHoursMultiplierTooltip",
                "NGET_CostNeutroamineMultiplier", "NGET_CostNeutroamineMultiplierTooltip");
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
        var multiplier = "NGET_MultiplierValue".Translate();

        var hoursLabel = hoursLabelKey.Translate();
        var hoursTooltip = hoursTooltipKey.Translate();
        var hoursValue = multiplier.Formatted(hours.ToString("F2"));
        DrawHorizontalSlider(subSection, ref hours, hoursLabel, hoursTooltip, hoursValue,
            HOURS_MIN, HOURS_MAX, INCREMENT_TIME);

        var neutroamineLabel = neutroLabelKey.Translate();
        var neutroTooltip = neutroTooltipKey.Translate();
        var neutroValue = multiplier.Formatted(neutroamine.ToString("F2"));
        DrawHorizontalSlider(subSection, ref neutroamine, neutroamineLabel, neutroTooltip, neutroValue,
            NEUTROAMINE_MIN, NEUTROAMINE_MAX, INCREMENT_RESOURCE);
    }

    private static void DrawHorizontalSlider(Listing_Standard subSection, ref float variable,
        TaggedString label, TaggedString tooltip, TaggedString value,
        float min, float max, float increment)
    {
        float y = subSection.CurHeight;
        float width = subSection.ColumnWidth;
        float labelWidth = SLIDER_LABEL_AREA_PCT;
        float remainingWidth = 1f - SLIDER_LABEL_AREA_PCT - SLIDER_LABEL_SEPARATION_PCT;
        float valueWidth = SLIDER_VALUE_AREA_PCT;
        float sliderWidth = 1f - SLIDER_VALUE_AREA_PCT - SLIDER_LABEL_SEPARATION_PCT;
        float heightNeeded = Text.LineHeight * LINE_HEIGHT_MULTIPIER;

        var area = subSection.GetRect(heightNeeded); //reserve the area for the slider/label from the Listing_Standard
        var labelArea = area.LeftPart(labelWidth);
        labelArea.y += 8; //move it down to center vertically a bit
        var remainingArea = area.RightPart(remainingWidth);
        var valueArea = remainingArea.LeftPart(valueWidth);
        valueArea.y += 8; //move it down to center vertically a bit
        var sliderArea = remainingArea.RightPart(sliderWidth);
        sliderArea.y += 14; //it was drawing over the previous controls on screen

        GUIContent labelInfo = new GUIContent(label, tooltip);
        var range = new FloatRange(min, max);

        Widgets.Label(labelArea, labelInfo);
        Widgets.Label(valueArea, value);
        Widgets.HorizontalSlider(sliderArea, ref variable, range, roundTo: increment);
    }

    private static Listing_Standard BeginSubSection(Listing_Standard parent, float height, float width, float sectionBorder = 6f, float bottomBorder = 4f)
    {
        Rect rect = parent.GetRect(height + sectionBorder + bottomBorder);
        Widgets.DrawMenuSection(rect);
        Listing_Standard listing_Standard = new Listing_Standard();
        Rect rect2 = new Rect(rect.x + sectionBorder, rect.y + sectionBorder, rect.width - sectionBorder * 2f, rect.height - (sectionBorder + bottomBorder));
        listing_Standard.Begin(rect2);

        return listing_Standard;
    }

    private static void DrawGapBetweenSections(Listing_Standard listing)
    {
        listing.Gap(SECTION_GAP);
    }

    private static float GetHeightTierSubsection()
    {
        var previousFont = Text.Font;

        Text.Font = GameFont.Medium;
        var headerHeight = Text.LineHeight + LINE_MARGIN_VERTICAL;
        var height = Text.LineHeight * LINE_HEIGHT_MULTIPIER * 6f;
        Text.Font = previousFont;

        return height;
    }

    private static float GetHeightCorpseType()
    {
        var textLineHeight = Text.LineHeight;
        var height = Text.LineHeight * LINE_HEIGHT_MULTIPIER * 3f;

        return height;
    }

    private static float GetHeightCorpsesSection()
    {
        var previousFont = Text.Font;

        Text.Font = GameFont.Medium;
        var headerHeight = Text.LineHeight + LINE_MARGIN_VERTICAL;
        Text.Font = previousFont;

        var height = headerHeight
            + (GetHeightCorpseType() + SECTION_GAP) * 3f
            + SUBSECTION_PADDING * 6f
         ;

        return height;
    }
}
