using System.Globalization;
using Android.Content;

namespace MoneyNote.Maui;

public sealed class LegacySettingsReader
{
    private readonly IReadOnlyList<ISharedPreferences> _sources;

    public LegacySettingsReader()
    {
        var context = Android.App.Application.Context ?? throw new InvalidOperationException("Android application context is unavailable.");
        var sources = new ISharedPreferences?[]
        {
            context.GetSharedPreferences($"{context.PackageName}_preferences", FileCreationMode.Private),
            context.GetSharedPreferences($"{context.PackageName}.plugin.settings", FileCreationMode.Private),
            context.GetSharedPreferences(context.PackageName, FileCreationMode.Private),
            context.GetSharedPreferences("settingsplugin", FileCreationMode.Private)
        };

        _sources = sources
            .Where(source => source is not null)
            .Cast<ISharedPreferences>()
            .ToList();
    }

    public int GetInt(string key, int defaultValue)
    {
        var rawValue = GetRawValue(key);
        if (rawValue is null)
        {
            return defaultValue;
        }

        return rawValue switch
        {
            int value => value,
            long value => (int)value,
            Java.Lang.Integer value => value.IntValue(),
            Java.Lang.Long value => (int)value.LongValue(),
            string value when int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed) => parsed,
            Java.Lang.String value when int.TryParse(value.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed) => parsed,
            _ => defaultValue
        };
    }

    public bool GetBool(string key, bool defaultValue)
    {
        var rawValue = GetRawValue(key);
        if (rawValue is null)
        {
            return defaultValue;
        }

        return rawValue switch
        {
            bool value => value,
            Java.Lang.Boolean value => value.BooleanValue(),
            string value when bool.TryParse(value, out var parsed) => parsed,
            Java.Lang.String value when bool.TryParse(value.ToString(), out var parsed) => parsed,
            _ => defaultValue
        };
    }

    public decimal GetDecimal(string key, decimal defaultValue)
    {
        var rawValue = GetRawValue(key);
        if (rawValue is null)
        {
            return defaultValue;
        }

        return rawValue switch
        {
            decimal value => value,
            double value => Convert.ToDecimal(value, CultureInfo.InvariantCulture),
            float value => Convert.ToDecimal(value, CultureInfo.InvariantCulture),
            int value => value,
            long value => value,
            Java.Lang.Double value => Convert.ToDecimal(value.DoubleValue(), CultureInfo.InvariantCulture),
            Java.Lang.Float value => Convert.ToDecimal(value.FloatValue(), CultureInfo.InvariantCulture),
            Java.Lang.Integer value => value.IntValue(),
            Java.Lang.Long value => value.LongValue(),
            string value when TryParseDecimal(value, out var parsed) => parsed,
            Java.Lang.String value when TryParseDecimal(value.ToString(), out var parsed) => parsed,
            _ => defaultValue
        };
    }

    public string GetString(string key, string defaultValue = "")
    {
        var rawValue = GetRawValue(key);
        if (rawValue is null)
        {
            return defaultValue;
        }

        return rawValue switch
        {
            string value => value,
            Java.Lang.String value => value.ToString() ?? defaultValue,
            _ => rawValue.ToString() ?? defaultValue
        };
    }

    private object? GetRawValue(string key)
    {
        foreach (var source in _sources)
        {
            var entries = source.All;
            if (entries is not null && entries.ContainsKey(key))
            {
                return entries[key];
            }
        }

        return null;
    }

    private static bool TryParseDecimal(string value, out decimal result)
    {
        return decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result)
            || decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out result);
    }
}
