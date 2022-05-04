using System.Text.RegularExpressions;

namespace SiteParserLibrary.Utils;

public partial class RegexUtils
{
    [RegexGenerator(@"(?<=location\.href ="").*?(?="")", RegexOptions.CultureInvariant)]
    private static partial Regex HrefLocationRegex();

    public static string? GetRedirectLocation(string html) => HrefLocationRegex().Match(html).Value;
}