using OfficialOzonApi.Models.CategoriesTree;
using OfficialOzonApi.Models.Stats;

namespace OfficialOzonApi;

public class OzonApiAdapter
{
    private readonly OfficialOzonApiClient _officialOzonApiClient;

    public OzonApiAdapter(OfficialOzonApiClient officialOzonApiClient)
    {
        _officialOzonApiClient = officialOzonApiClient;
    }

    public async Task<IEnumerable<string>> GetSuggestedNames(string query)
    {
        return (await _officialOzonApiClient.GetSuggestedTapTagsAsync(query)).Items.Select(x =>
            x.CellTrackingInfo.SearchString + x.Title);
    }

    public async Task<IEnumerable<MyNode>> GetNodesTree(string search = "")
    {
        var result = new List<MyNode>();
        var data = await _officialOzonApiClient.GetCategoriesTreeAsync(search);
        foreach (var pair in data)
        {
            result.Add(new MyNode(pair.Value));
        }

        return result;
    }

    public async Task<IEnumerable<SearchResult>> GetUserSearchResultsAsync(IEnumerable<int>? categories = null,
        DateTime? from = null,
        DateTime? to = null, string text = "", int offset = 0, int limit = 50, SortingParams? sortingParams = null,
        CancellationToken token = default) =>
        await _officialOzonApiClient.GetUserSearchResultsAsync(categories, from, to, text, offset, limit, sortingParams,
            token);
}