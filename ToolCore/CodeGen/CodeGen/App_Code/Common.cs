using System;

public static class CommonUtil
{
    public static string ToFirstUper(this string text)
    {
        if (text == null)
        {
            return "";
        }
        return text.Substring(0, 1).ToUpper() + text.Substring(1);
    }
    public static string ToFirstLower(this string text)
    {
        if (text == null)
        {
            return "";
        }
        return text.Substring(0, 1).ToLower() + text.Substring(1);
    }

    public static bool IsNullOrEmpty(this string text)
    {
        return string.IsNullOrEmpty(text);
    }

    public static string NoSpace(this string val)
    {
        if (val == null)
        {
            return val;
        }
        return val.Replace(" ", "");
    }

    public static string GetTableClassName(this string text)
    {
        var result = "";

        var classNameArray = text.Split(new String[] { "_" }, StringSplitOptions.None);
        foreach (var item in classNameArray)
        {
            result += item.ToFirstUper();
        }

        return result;
    }
}