using System;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace RenameSettlements;

using Settings;
using UnityEngine;

public class RenameSettlements : Mod
{
    public RenameSettlements(ModContentPack content)
        : base(content)
    {
#if v1_5
        const string GAME_VERSION = "v1.5";
#else
#error No version defined
        const string GAME_VERSION = "UNDEFINED";
#endif

#if DEBUG
        const string build = "Debug";
#else
        const string build = "Release";
#endif
        Log(
            $"Running Version {Assembly.GetAssembly(typeof(RenameSettlements)).GetName().Version} {build} compiled for RimWorld version {GAME_VERSION}"
        );

        Harmony harmony = new("dev.tobot.rename-settlements");
        harmony.PatchAll();

        DebugLog("PatchAll ran successfully");

        Settings = GetSettings<RenameSettlementsSettings>();
        // In case some of the values were null (e.g. added between versions), write with default values.
        // Also prevents repeat errors for namespace change of Settings class
        WriteSettings();
    }

#nullable disable // Set in constructor.

    public static RenameSettlementsSettings Settings { get; private set; }

#nullable enable

    public void ResetSettings()
    {
        Settings = new();
        WriteSettings();
    }

    public override void DoSettingsWindowContents(Rect inRect) =>
        RenameSettlementsSettingsWindow.DoSettingsWindowContents(inRect);

    public override string SettingsCategory() => RenameSettlementsSettingsWindow.SettingsCategory();

    const string LogPrefix = "Rename Settlements - ";

    public static void DebugError(string message)
    {
#if DEBUG
        Error(message);
#endif
    }

    public static void Error(string message)
    {
        Verse.Log.Error(LogPrefix + message);
    }

    public static void DebugWarn(string message)
    {
#if DEBUG
        Warn(message);
#endif
    }

    public static void Warn(string message)
    {
        Verse.Log.Warning(LogPrefix + message);
    }

    public static void DebugLog(string message)
    {
#if DEBUG
        Log(message);
#endif
    }

    public static void Log(string message)
    {
        Verse.Log.Message(LogPrefix + message);
    }
}
