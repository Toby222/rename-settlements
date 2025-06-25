using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

using Verse;

namespace RenameSettlements.Settings;

public class RenameSettlementsSettings : ModSettings
{
    private static RenameSettlementsSettings DefaultValues() => new();

    public RenameSettlementsSettings() => Reset();

    #region Scribe Helpers
    private void LookStruct<T>(Expression<Func<T>> expression)
    {
        if (
            expression.Body
            is not MemberExpression
            {
                Member: MemberInfo { MemberType: MemberTypes.Field, Name: string memberName }
            }
        )
        {
            throw new ArgumentException(
                "Invalid expression passed to LookField",
                nameof(expression)
            );
        }

        FieldInfo fieldInfo = typeof(RenameSettlementsSettings).GetField(memberName);
        T? value = fieldInfo.GetValue(this).ChangeType<T>();
        T defaultValue = fieldInfo.GetValue(DefaultValues()).ChangeType<T>();
        Scribe_Values.Look(ref value, memberName, defaultValue);
        fieldInfo.SetValue(this, value);
    }
    #endregion

    public bool DebugLog;
    public bool RenamePlayerSettlements;
    public bool RenameOtherSettlements;

    public void Reset()
    {
        // Set default values here, not on the indvidual fields
        DebugLog = false;
        RenamePlayerSettlements = true;
        RenameOtherSettlements = true;
    }

    public override void ExposeData()
    {
        base.ExposeData();
        LookStruct(() => DebugLog);
        LookStruct(() => RenamePlayerSettlements);
        LookStruct(() => RenameOtherSettlements);
    }
}
