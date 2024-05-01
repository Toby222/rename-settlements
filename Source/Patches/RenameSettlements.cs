using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace RenameSettlements.Patches;

[HarmonyPatch(typeof(Settlement), nameof(Settlement.GetGizmos))]
public static class RenameSettlementsPatch
{
    public static void Postfix(Settlement __instance, ref IEnumerable<Gizmo> __result)
    {
        if (Find.WorldSelector.NumSelectedObjects != 1)
            return;
        if (
            (RenameSettlements.Settings.RenamePlayerSettlements && __instance.Faction.IsPlayer)
            || (RenameSettlements.Settings.RenameOtherSettlements && !__instance.Faction.IsPlayer)
        )
        {
            __result = __result.AddItem(
                new Command_Action
                {
                    defaultLabel = "RenameSettlements.RenameLabel".Translate(),
                    defaultDesc = "RenameSettlements.RenameDesc".Translate(),
                    icon = TexButton.Rename,
                    action = delegate
                    {
                        Find.WindowStack.Add(new Dialog_NamePlayerSettlement(__instance));
                    },
                    Disabled = false,
                }
            );
        }
    }
}
