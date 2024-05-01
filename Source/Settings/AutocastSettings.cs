using Verse;

namespace RenameSettlements.Settings;

public class RenameSettlementsSettings : ModSettings
{
    public RenameSettlementsSettings()
    {
        RenameSettlements.DebugLog("Initiating settings");
        RenamePlayerSettlements = true;
        RenameOtherSettlements = true;
    }

    #region Scribe Helpers
    private static void LookField<T>(ref T value, string label, T defaultValue)
        where T : struct
    {
        Scribe_Values.Look(ref value, label, defaultValue);
    }
    #endregion

    public bool RenamePlayerSettlements = true;
    public bool RenameOtherSettlements = true;

    public override void ExposeData()
    {
        base.ExposeData();
        LookField(ref RenamePlayerSettlements, nameof(RenamePlayerSettlements), true);
        LookField(ref RenameOtherSettlements, nameof(RenameOtherSettlements), true);
    }
}
