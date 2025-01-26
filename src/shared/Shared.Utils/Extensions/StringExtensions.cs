namespace System;

public static class StringExtensions
{
    public static Guid ToGuid(this string input)
    {
        return Guid.Parse(input);
    }

    public static string AppendToStart(this string text, string prefix)
    {
        return prefix + text;
    }

    public static string AppendToEnd(this string text, string suffix)
    {
        return text + suffix;
    }

    public static bool IsNullOrEmpty(this string? text)
    {
        return string.IsNullOrEmpty(text);
    }

    public static bool IsNullOrWhiteSpace(this string? text)
    {
        return string.IsNullOrWhiteSpace(text);
    }
}
