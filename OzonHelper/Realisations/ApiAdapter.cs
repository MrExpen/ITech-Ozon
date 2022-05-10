using OfficialOzonApi;
using OzonHelper.Data.Model;
using OzonHelper.Services;
using OzonHelper.Utils;

namespace OzonHelper.Realisations;

public class ApiAdapter : IApiAdapter
{
    private readonly OfficialOzonApiClient _apiClient;

    public ApiAdapter(OfficialOzonApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IEnumerable<string>?> GetSuggestedName(string query, CancellationToken token = default)
    {
        var response = await _apiClient.GetSuggestedTapTagsAsync(query, token);
        return response?.SuggestedTapTags?.Items?.Select(x => x.CellTrackingInfo.SearchString + x.Title);
    }

    public async Task<IEnumerable<Category>?> GetCategoryTreeAsync(string search = "", CancellationToken token = default)
    {
        var response = await _apiClient.GetCategoriesTreeAsync(search, token);
        return response?.Result?.Select(pair => pair.Value.ConvertToCategory()).ToList();
    }

    public async Task<IEnumerable<Search>?> GetUserSearchesAsync(DumpWeeks weeks = DumpWeeks.One, string text = "", int offset = 0, int limit = 50,
        IEnumerable<int>? categories = null, CancellationToken token = default)
    {
        var to = DateTime.Today.AddDays(-1);
        var from = to.AddDays(-((int)weeks * 7) + 1);
        return (await _apiClient.GetUserSearchResultsAsync(categories, from, to, text, offset, limit, token: token))
            ?.Data?.Select(x => x.ConvertToSearch());
    }

    public async Task<SiteDump?> DumpDataAsync(DumpWeeks weeks = DumpWeeks.One, CancellationToken token = default)
    {
        var to = DateTime.Today.AddDays(-1);
        var from = to.AddDays(-((int)weeks * 7) + 1);

        const int limit = 500;
        
        var results = new List<Search>();
        int? total = null;
        int i = 0;
        while (!total.HasValue || results.Count < total)
        {
            var response = await _apiClient.GetUserSearchResultsAsync(from: from, to: to, limit: limit,
                offset: i++ * limit, token: token);
            total ??= response?.Total;
            if (response is null) break;
            results.AddRange(response.Data.Select(x => x.ConvertToSearch()));
        }

        var categories = await GetCategoryTreeAsync(token: token) ?? Enumerable.Empty<Category>();
        
        return new SiteDump
        {
            Searches = results,
            Date = to.AddDays(1),
            DumpWeeks = weeks,
            Categories = categories.ToList()
        };
    }
}