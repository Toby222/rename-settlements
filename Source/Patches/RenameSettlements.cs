using System.Collections.Generic;

using HarmonyLib;

using RimWorld;
using RimWorld.Planet;

using Verse;

namespace RenameSettlements.Patches;

[HarmonyPatch(typeof(Settlement), nameof(Settlement.GetGizmos))]
public static class RenameSettlementsPatch
{
#pragma warning disable IDE1006 // Names are enforced by Harmony
    public static void Postfix(Settlement __instance, ref IEnumerable<Gizmo> __result)
#pragma warning restore IDE1006
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
                    defaultLabel = RenameSettlements.Translate("RenameLabel"),
                    defaultDesc = RenameSettlements.Translate("RenameDesc"),
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
