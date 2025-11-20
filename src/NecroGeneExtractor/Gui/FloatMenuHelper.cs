using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Gui;

public static class FloatMenuHelper
{
    public static void BuildFloatMenuAvailableCorpses(Map map, Func<Corpse, AcceptanceReport> canAcceptCorpse, Action<Corpse> selectCorpse)
    {
        List<FloatMenuOption> list = [];
        var corpses = map.listerThings.AllThings
            .OfType<Corpse>();

        foreach (Corpse corpse in corpses)
        {
            if (corpse.InnerPawn.genes != null)
            {
                AcceptanceReport acceptanceReport = canAcceptCorpse(corpse);
                string text = corpse.LabelShortCap + ", " + corpse.InnerPawn.genes.XenotypeLabelCap;
                if (!acceptanceReport.Accepted)
                {
                    if (!acceptanceReport.Reason.NullOrEmpty())
                    {
                        list.Add(new FloatMenuOption(text + ": " + acceptanceReport.Reason, null, corpse, Color.white));
                    }
                }
                else
                {
                    list.Add(new FloatMenuOption(text, () => selectCorpse(corpse), corpse, Color.white));
                }
            }
        }

        if (!list.Any())
        {
            list.Add(new FloatMenuOption("NoExtractablePawns".Translate(), null));
        }

        Find.WindowStack.Add(new FloatMenu(list));
    }
}
