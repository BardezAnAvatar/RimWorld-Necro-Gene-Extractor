using System;
using GeneExtractorTiers.Extractors;
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
}
