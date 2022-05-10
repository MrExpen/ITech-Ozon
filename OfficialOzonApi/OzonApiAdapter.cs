using Microsoft.Extensions.Logging;
using OfficialOzonApi.Models.CategoriesTree;
using OfficialOzonApi.Models.Stats;

namespace OfficialOzonApi;

public class OzonApiAdapter
{
    private readonly OfficialOzonApiClient _officialOzonApiClient;
    private readonly ILogger<OzonApiAdapter> _logger;

    public OzonApiAdapter(OfficialOzonApiClient officialOzonApiClient, ILogger<OzonApiAdapter> logger)
    {
        _officialOzonApiClient = officialOzonApiClient;
        _logger = logger;
    }

    public async Task<IEnumerable<string>> GetSuggestedNames(string query)
    {
        return (await _officialOzonApiClient.GetSuggestedTapTagsAsync(query)).Items.Select(x =>
            x.CellTrackingInfo.SearchString + x.Title);
    }

    public async Task<IEnumerable<MyNode>> GetNodesTree(string search = "", CancellationToken token = default)
    {
        var result = new List<MyNode>();
        var data = await _officialOzonApiClient.GetCategoriesTreeAsync(search, token);
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

    public async Task<IEnumerable<SearchResult>> GetUserSearchResultsAsync(IEnumerable<int>? categories = null,
        int days = 7,
        DateTime? to = null, string text = "", int offset = 0, int limit = 50, SortingParams? sortingParams = null,
        CancellationToken token = default)
    {
        to ??= DateTime.Today.AddDays(-1);
        var from = to.Value.AddDays(-days + 1);
        return await GetUserSearchResultsAsync(categories, from, to, text, offset, limit,
            sortingParams,
            token);
    }

    public async Task<IEnumerable<SearchResult>> GetAllUserSearchResultsAsync(IEnumerable<int>? categories = null,
        DateTime? from = null,
        DateTime? to = null, string text = "", int offset = 0, int limit = 500, SortingParams? sortingParams = null,
        int waitSeconds = 180, int maxRetries = 10, CancellationToken token = default)
    {
        categories = categories?.ToArray();
        var results = new List<SearchResult>();
        List<SearchResult>? localResult = null;
        int i = 0;
        var retries = 0;
        do
        {
            try
            {
                localResult =
                    (await GetUserSearchResultsAsync(categories, from, to, text, limit * i++, limit, sortingParams, token))
                    .ToList();
                results.AddRange(localResult);
                retries = 0;
            }
            catch (Exception e)
            {
                i--;
                if (retries >= maxRetries)
                {
                    _logger.LogCritical("Max reties ({MaxRetries}) reached. Exiting.....", maxRetries);
                }
                _logger.LogError(e, "Retrying after {Seconds} seconds.....", waitSeconds * ++retries);
                await Task.Delay(TimeSpan.FromSeconds(waitSeconds * retries), token);
            }
        } while (localResult?.Count != 0);

        return results;
    }
    
    public async Task<IEnumerable<SearchResult>> GetAllUserSearchResultsAsync(IEnumerable<int>? categories = null,
        int days = 7,
        DateTime? to = null, string text = "", int offset = 0, int limit = 500, SortingParams? sortingParams = null,
        int waitSeconds = 180, int maxRetries = 10,
        CancellationToken token = default)
    {
        to ??= DateTime.Today.AddDays(-1);
        var from = to.Value.AddDays(-days + 1);
        return await GetAllUserSearchResultsAsync(categories, from, to, text, offset, limit,
            sortingParams, waitSeconds, maxRetries, token);
    }
}