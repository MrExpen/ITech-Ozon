using CoreLibrary.Services;

namespace SiteParserLibrary.Services;

public class NameCategoryComparer : INameCategoryComparer
{
    public double GetSuggestions(string name, string category)
    {
        throw new NotImplementedException();
    }

    public Task<double> GetSuggestionsAsync(string name, string category, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}