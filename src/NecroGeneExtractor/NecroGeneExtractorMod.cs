using Bardez.Biotech.NecroGeneExtractor.Settings;
using Bardez.Biotech.NecroGeneExtractor.Utilities;
using RimWorld;
using UnityEngine;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor;

public class NecroGeneExtractorMod : Mod
{
    public static NecroGeneExtractorSettings _settings = null;
    public static NecroGeneExtractorSettings Settings => _settings;

    public NecroGeneExtractorMod(ModContentPack content) : base(content)
    {
        _settings = GetSettings<NecroGeneExtractorSettings>();

        //ApplyHarmonyPatches();
    }

    /// <summary>
    /// The (optional) GUI part to set your settings.
    /// </summary>
    /// <param name="inRect">A Unity Rect with the size of the settings window.</param>
    public override void DoSettingsWindowContents(Rect inRect)
    {
        Rect outerRect = new Rect(inRect);

        DebugMessaging.DebugMessage($"{nameof(NecroGeneExtractorSettings)}.{nameof(NecroGeneExtractorSettings.SettingsTier2)}: {_settings.SettingsTier2}");
        WindowDrawing.DrawSettings_Variables(outerRect, _settings);

        base.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return "NGET_ModName".Translate();
    }
}
