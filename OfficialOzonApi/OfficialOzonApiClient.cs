using Newtonsoft.Json;
using OfficialOzonApi.Models.CategoriesTree;
using OfficialOzonApi.Models.Stats;
using OfficialOzonApi.Models.SuggestedTapTags;
using RestSharp;

namespace OfficialOzonApi;

public class OfficialOzonApiClient
{
    private readonly RestClient _restClient;
    private readonly int _companyId;

    public OfficialOzonApiClient(string accessToken, int companyId)
    {
        _restClient = new RestClient();
        _companyId = companyId;
        _restClient.Options.UserAgent =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.54 Safari/537.36";
        _restClient.AddDefaultHeader("accesstoken", accessToken);
        _restClient.AddDefaultHeader("x-o3-company-id", companyId.ToString());
    }

    public async Task<SuggestedTapTags> GetSuggestedTapTagsAsync(string name, CancellationToken token = default)
    {
        var request = new RestRequest("https://www.ozon.ru/api/composer-api.bx/_action/getSuggestedTapTags",
            Method.Post);
        request.AddJsonBody(new { text = name.ToLower() });
        var response = await _restClient.ExecuteAsync(request, token);
        var result = JsonConvert.DeserializeObject<Models.SuggestedTapTags.ApiResponse>(response.Content);
        return result.SuggestedTapTags;
    }

    public async Task<Dictionary<int, ModelNode>> GetCategoriesTreeAsync(string search = "",  CancellationToken token = default)
    {
        var request = new RestRequest("https://seller.ozon.ru/api/site/category/navigation-category/get-tree",
            Method.Post);
        request.AddJsonBody(new { company_id = _companyId, search = search.ToLower(), commercial_category_ids = Enumerable.Empty<int>() });
        var response = await _restClient.ExecuteAsync(request, token);
        var result = JsonConvert.DeserializeObject<Models.CategoriesTree.ApiResponse>(response.Content);
        return result.Result;
    }

    public async Task<IEnumerable<SearchResult>> GetUserSearchResultsAsync(IEnumerable<int>? categories = null, DateTime? from = null,
        DateTime? to = null, string text = "", int offset = 0, int limit = 50, SortingParams? sortingParams = null,
        CancellationToken token = default)
    {
        var request = new RestRequest("https://seller.ozon.ru/api/site/searchteam/Stats/query/v2", Method.Post);
        
        categories ??= Enumerable.Empty<int>();
        to ??= DateTime.Today.AddDays(-1);
        from ??= to.Value.AddDays(-27);
        sortingParams ??= new SortingParams("count", "desc");

        request.AddStringBody(JsonConvert.SerializeObject(new
        {
            end_categories = categories.Select(x => x.ToString()),
            from = from.Value.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            to = to.Value.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            limit,
            offset,
            sorting = new
            {
                attribute = sortingParams.Value.Attribute,
                order = sortingParams.Value.Order
            },
            text = text.ToLower()
        }), "application/json");
        var response = await _restClient.ExecuteAsync(request, token);
        var result = JsonConvert.DeserializeObject<Models.Stats.ApiResponse>(response.Content);
        return result.Data;
    }
}