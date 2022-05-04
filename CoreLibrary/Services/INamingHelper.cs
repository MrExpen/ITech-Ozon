namespace CoreLibrary.Services;

public interface INamingHelper
{
    IEnumerable<string> GetSuggestions(string name);
    Task<IEnumerable<string>> GetSuggestionsAsync(string name, CancellationToken token = default);
}