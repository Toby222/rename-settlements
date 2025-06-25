using System.Reflection;
using HarmonyLib;
using Verse;
using UnityEngine;

using RenameSettlements.Settings;

namespace RenameSettlements;

#if DEBUG
#warning Compiling in Debug mode
#endif

public class RenameSettlements : Mod
{
    public const string TRANSLATION_KEY = "RenameSettlements";
    public const string LOG_PREFIX = "Rename Settlements - "; // Can't be defined in xml because it gets used before Languages load

    public RenameSettlements(ModContentPack content)
        : base(content)
    {
#if v1_5
        const string GAME_VERSION = "v1.5";
#elif v1_6
        const string GAME_VERSION = "v1.6";
#else
#error No version defined
        const string GAME_VERSION = "UNDEFINED";
#endif

#if DEBUG
        const string BUILD = "Debug";
#else
        const string BUILD = "Release";
#endif
        Log(
            $"Running Version {Assembly.GetAssembly(typeof(RenameSettlements)).GetName().Version} {BUILD} compiled for RimWorld version {GAME_VERSION}"
        );

        Harmony harmony = new("dev.tobot.rename-settlements");
        harmony.PatchAll();

        DebugLog("PatchAll ran successfully");

        Settings = GetSettings<RenameSettlementsSettings>();
        // In case some of the values were null (e.g. added between versions), write with default values.
        // Also prevents repeat errors for namespace change of Settings class
        WriteSettings();
    }

#nullable disable // Set in constructor on game startup; Do not use it before startup is finished!

    public static RenameSettlementsSettings Settings { get; private set; }

#nullable enable

    public override void DoSettingsWindowContents(Rect inRect) => RenameSettlementsSettingsWindow.DoSettingsWindowContents(inRect);

    public override string SettingsCategory() => Translate("SettingsCategory");

    public static string GetTranslationKey(string key) => TRANSLATION_KEY + "." + key;

    public static string Translate(string key, params NamedArgument[] args) => GetTranslationKey(key).TranslateSafe(args);

    public static void DebugError(string message, int? key = null)
    {
#if !DEBUG
        if (Settings?.DebugLog == true)
#endif
        Error(message, key);
    }

    public static void Error(string message, int? key = null)
    {
        if (key is int keyNotNull)
            Verse.Log.ErrorOnce(LOG_PREFIX + message, keyNotNull);
        else
            Verse.Log.Error(LOG_PREFIX + message);
    }

    public static void DebugWarn(string message, int? key = null)
    {
#if !DEBUG
        if (Settings?.DebugLog == true)
#endif
        Warn(message, key);
    }

    public static void Warn(string message, int? key = null)
    {
        if (key is int keyNotNull)
            Verse.Log.WarningOnce(LOG_PREFIX + message, keyNotNull);
        else
            Verse.Log.Warning(LOG_PREFIX + message);
    }

    public static void DebugLog(string message)
    {
#if !DEBUG
        if (Settings?.DebugLog == true)
#endif
        Log(message);
    }

    public static void Log(string message) => Verse.Log.Message(LOG_PREFIX + message);
}
