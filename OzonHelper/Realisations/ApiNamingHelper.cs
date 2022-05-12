using OzonHelper.Services;

namespace OzonHelper.Realisations;

public class ApiNamingHelper : INamingHelper
{
    private readonly IApiAdapter _apiAdapter;

    public ApiNamingHelper(IApiAdapter apiAdapter)
    {
        _apiAdapter = apiAdapter;
    }

    public IEnumerable<string> GetSuggestions(string name)
        => GetSuggestionsAsync(name).GetAwaiter().GetResult();

    public async Task<IEnumerable<string>> GetSuggestionsAsync(string name, CancellationToken token = default) 
        => await _apiAdapter.GetSuggestedName(name, token);
}