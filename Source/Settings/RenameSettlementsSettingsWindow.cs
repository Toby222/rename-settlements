using UnityEngine;

using Verse;

namespace RenameSettlements.Settings;

public static class RenameSettlementsSettingsWindow
{
    private static Vector2 settingsScrollPosition = new();

    private static float settingsHeight;
    private static bool confirmReset;

    private static RenameSettlementsSettings Settings => RenameSettlements.Settings;

    public static void DoSettingsWindowContents(Rect inRect)
    {
        Listing_Standard listing = new();

        Rect viewRect = new(inRect.x, inRect.y, inRect.width - 16f, settingsHeight);
        Widgets.BeginScrollView(inRect, ref settingsScrollPosition, viewRect);

        listing.Begin(new Rect(viewRect.x, viewRect.y, viewRect.width, float.PositiveInfinity));

        void Checkbox(string labelKey, ref bool value) => listing.CheckboxLabeled(RenameSettlements.Translate(labelKey), ref value);
        bool ButtonText(string labelKey) => listing.ButtonText(RenameSettlements.Translate(labelKey));

        if (confirmReset)
        {
            Color prevColor = GUI.color;
            GUI.color = Color.red;
            if (ButtonText("ResetSettings.Confirmation"))
            {
                Settings.Reset();
                confirmReset = false;
            }
            GUI.color = prevColor;
        }
        else if (ButtonText("ResetSettings"))
        {
            confirmReset = true;
        }

        listing.GapLine();

        Checkbox("DebugLog", ref Settings.DebugLog);
        Checkbox("RenameColonies", ref Settings.RenamePlayerSettlements);
        Checkbox("RenameOthers", ref Settings.RenameOtherSettlements);

        listing.End();
        settingsHeight = listing.CurHeight;
        Widgets.EndScrollView();
    }
}
