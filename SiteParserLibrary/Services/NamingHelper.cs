using CoreLibrary.Services;
using OfficialOzonApi;

namespace SiteParserLibrary.Services;

public class NamingHelper : INamingHelper
{
    private readonly OfficialOzonApiClient _officialOzonApiClient;

    public NamingHelper(OfficialOzonApiClient officialOzonApiClient)
    {
        _officialOzonApiClient = officialOzonApiClient;
    }

    public IEnumerable<string> GetSuggestions(string name)
        => GetSuggestionsAsync(name).GetAwaiter().GetResult();

    public async Task<IEnumerable<string>> GetSuggestionsAsync(string name, CancellationToken token = default) =>
        (await _officialOzonApiClient.GetSuggestedTapTagsAsync(name)).Items.Select(x =>
            x.CellTrackingInfo.SearchString + x.Title);
}