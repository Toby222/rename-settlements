global using static RenameSettlements.Extensions.TranslateExtension;

using System;

using Verse;

namespace RenameSettlements.Extensions;

internal static class TranslateExtension
{
    public static TaggedString TranslateSafe(
        this string key,
        params NamedArgument[] args
    )
    {
        if (key is null)
            throw new ArgumentNullException(nameof(key));
        if (LanguageDatabase.activeLanguage is null)
            throw new ArgumentNullException("activeLanguage");

        if (!key.CanTranslate())
        {
            RenameSettlements.DebugError(
                $"Untranslated key: {key}",
                key.GetHashCodeSafe()
            );
        }
        return key.Translate(args);
    }
}
