using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Nova;

using LocalizedStrings = Dictionary<SystemLanguage, string>;
using TranslationBundle = Dictionary<string, object>;

public static class I18n
{
    public const string LocalizedResourcesPath = "LocalizedResources/";
    public const string LocalizedStringsPath = "LocalizedStrings/";

    public static readonly SystemLanguage[] SupportedLocales;

    public static SystemLanguage DefaultLocale => SupportedLocales.First();

    public static event Action LocaleChanged;

    private static SystemLanguage _currentLocale = FallbackLocale(GetSystemLocale());

    public static SystemLanguage CurrentLocale
    {
        get => _currentLocale;
        set
        {
            value = FallbackLocale(value);
            if (_currentLocale == value)
            {
                return;
            }

            _currentLocale = value;
            LocaleChanged.Invoke();
        }
    }

    private static SystemLanguage FallbackLocale(SystemLanguage locale)
    {
        if (locale == SystemLanguage.Chinese ||
            locale == SystemLanguage.ChineseSimplified ||
            locale == SystemLanguage.ChineseTraditional)
        {
            return SystemLanguage.ChineseSimplified;
        }
        else
        {
            return SystemLanguage.English;
        }
    }

    private static SystemLanguage GetSystemLocale()
    {
        try
        {
            var cultureInfo = CultureInfo.CurrentUICulture;
            if (cultureInfo.Name.StartsWith("zh-", StringComparison.Ordinal))
            {
                return SystemLanguage.ChineseSimplified;
            }
        }
        catch (Exception e)
        {
            GD.Print($"Nova: Failed to get CurrentUICulture.\n{e.Message}");
        }

        return SystemLanguage.ChineseSimplified;
    }

    private static bool Inited;
    private static void Init()
    {
        if (Inited) return;
        LoadTranslationBundles();
        Inited = true;
    }

    private static void LoadTranslationBundles()
    {
        foreach (var locale in SupportedLocales)
        {
            string textAsset = ResourceLoader.Load<string>($"{LocalizedStringsPath}{locale}");
            // TODO: Parse JSON
        }
        throw new NotImplementedException();
    }

    private static readonly Dictionary<SystemLanguage, TranslationBundle> TranslationBundles = new();

    private static string __(SystemLanguage locale, string key, params object[] args)
    {
        Init();

        string translation = key;

        if (TranslationBundles[locale].TryGetValue(key, out var raw))
        {
            if (raw is string value)
            {
                translation = value;
            }
            else if (raw is string[] formats)
            {
                if (formats.Length == 0)
                {
                    GD.Print($"Nova: Empty translation string list for: {key}");
                }
                else if (args.Length == 0)
                {
                    translation = formats[0];
                }
                else
                {
                    // The first argument will determine the quantity
                    object arg1 = args[0];
                    if (arg1 is int i)
                    {
                        translation = formats[Math.Min(i, formats.Length - 1)];
                    }
                }
            }
            else
            {
                GD.Print($"Nova: Invalid translation format for: {key}");
            }

            if (args.Length > 0)
            {
                translation = string.Format(translation, args);
            }
        }
        else
        {
            GD.Print($"Nova: Missing translation for: {key}");
        }

        return translation;
    }

    public static string __(string key, params object[] args)
    {
        return __(CurrentLocale, key, args);
    }


    public static string __(LocalizedStrings dict)
    {
        if (dict == null)
        {
            return null;
        }

        if (dict.ContainsKey(CurrentLocale))
        {
            return dict[CurrentLocale];
        }
        else
        {
            return dict[DefaultLocale];
        }
    }
}
