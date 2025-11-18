using Bardez.Biotech.NecroGeneExtractor.Gui;
using Bardez.Biotech.NecroGeneExtractor.Settings;
using RimWorld;
using UnityEngine;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor;

[StaticConstructorOnStartup]
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

        WindowDrawing.DrawSettings(outerRect, _settings);

        base.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return "NGET_ModName".Translate();
    }
}
