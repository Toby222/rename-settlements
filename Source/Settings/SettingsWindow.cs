using UnityEngine;
using Verse;

namespace RenameSettlements.Settings;

using System.Linq;

public static class RenameSettlementsSettingsWindow
{
    private static Vector2 settingsScrollPosition = new();

    private static float settingsHeight;

    private static RenameSettlementsSettings Settings => RenameSettlements.Settings;

    static void Checkbox(Listing_Standard listing, string labelKey, ref bool value)
    {
        listing.CheckboxLabeled(("RenameSettlements." + labelKey).Translate(), ref value);
    }

    static void Label(Listing_Standard listing, string labelKey)
    {
        listing.Label(("RenameSettlements." + labelKey).Translate());
    }

    public static void DoSettingsWindowContents(Rect inRect)
    {
        Listing_Standard listing = new();

        void Checkbox(string labelKey, ref bool value)
        {
            RenameSettlementsSettingsWindow.Checkbox(listing, labelKey, ref value);
        }
        void Label(string labelKey)
        {
            RenameSettlementsSettingsWindow.Label(listing, labelKey);
        }
        Rect viewRect = new(inRect.x, inRect.y, inRect.width - 16f, settingsHeight);
        Widgets.BeginScrollView(inRect, ref settingsScrollPosition, viewRect);
        listing.Begin(new Rect(viewRect.x, viewRect.y, viewRect.width, float.PositiveInfinity));

        Label("RenameSettlements");

        listing.GapLine();

        Checkbox("RenameColonies", ref Settings.RenamePlayerSettlements);
        Checkbox("RenameOthers", ref Settings.RenameOtherSettlements);

        listing.End();
        settingsHeight = listing.CurHeight;
        Widgets.EndScrollView();
    }

    public static string SettingsCategory() => "RenameSettlements.SettingsCategory".Translate();
}
