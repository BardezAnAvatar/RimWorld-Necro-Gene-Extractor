using System;
using System.Collections.Generic;
using Bardez.Biotech.NecroGeneExtractor.Utilities;
using GeneExtractorTiers.Extractors;
using RimWorld;
using UnityEngine;
using Verse;

namespace Bardez.Biotech.NecroGeneExtractor.Gui;

public static class GizmoHelper
{
    public static Command_Action BuildGizmoInsertCorpse(Action corpseSelector, bool powered)
    {
        var insertPerson = new Command_Action()
        {
            defaultLabel = "NGET_InsertCorpse".Translate() + "...",
            defaultDesc = "NGET_InsertCorpseTooltip".Translate(),
            icon = Textures.InsertPawn,
            action = corpseSelector,
        };

        if (!powered)
        {
            insertPerson.Disable("NoPower".Translate().CapitalizeFirst());
        }

        return insertPerson;
    }

    /// <remarks>Lifted from <see cref="Building.SelectContainedItemGizmo" /></remarks>
    public static Gizmo BuildCorpseSelectGizmo(Thing container, Corpse corpse)
    {
        if (!container.Faction.IsPlayerSafe())
        {
            return null;
        }

        if (!CanSelect(container, corpse))
        {
            return null;
        }

        string label = "CommandSelectContainedThing";
        string description = "CommandSelectContainedThingDesc";
        return CreateSelectCorpseGizmo(label, description, corpse, corpse);
    }

    /// <remarks>Lifted from <see cref="ContainingSelectionUtility.CanSelect" /></remarks>
    private static bool CanSelect(Thing container, Corpse corpse)
    {
        if (!corpse.def.selectable)
        {
            return false;
        }

        return true;
    }

    /// <remarks>Lifted from <see cref="ContainingSelectionUtility.CreateSelectGizmo" /></remarks>
    private static readonly Dictionary<string, TaggedString> labelCache = new Dictionary<string, TaggedString>();

    /// <remarks>Lifted from <see cref="ContainingSelectionUtility.CreateSelectGizmo" /></remarks>
    private static Gizmo CreateSelectCorpseGizmo(string label, string description, Corpse corpse, Corpse iconThing = null)
    {
        if (!corpse.def.selectable)
        {
            return null;
        }

        float scale = 1f;
        float angle = 0f;
        Vector2 iconProportions = Vector2.one;
        Color color = Color.white;
        Texture icon = (iconThing == null) 
            ? TexCommand.SelectCarriedThing
            : Widgets.GetIconFor(iconThing, new Vector2(75f, 75f), iconThing.def.defaultPlacingRot, 
            stackOfOne: true, out scale, out angle, out iconProportions, out color, out var _);

        return new Command_SelectStorage
        {
            defaultLabel = label.Translate(corpse).TruncateHeight(75f, 37.5f, labelCache),
            defaultDesc = description.Translate(),
            icon = icon,
            iconDrawScale = scale * 0.85f,
            iconAngle = angle,
            defaultIconColor = color,
            iconProportions = iconProportions,
            action = delegate
            {
                Find.Selector.ClearSelection();
                Find.Selector.Select(corpse);
            }
        };
    }
}
