using CoreLibrary.Services;
using Newtonsoft.Json;
using RestSharp;
using SiteParserLibrary.Models.Api;

namespace SiteParserLibrary.Services;

public class NamingHelper : INamingHelper
{
    private readonly RestClient _restClient;

    public NamingHelper()
    {
        _restClient = new RestClient();
    }

    public IEnumerable<string> GetSuggestions(string name)
        => GetSuggestionsAsync(name).GetAwaiter().GetResult();

    public async Task<IEnumerable<string>> GetSuggestionsAsync(string name, CancellationToken token = default)
    {
        var request = new RestRequest("https://www.ozon.ru/api/composer-api.bx/_action/getSuggestedTapTags",
            Method.Post);
        request.AddJsonBody(new { text = name });
        var response = await _restClient.ExecuteAsync(request, token);
        var result = JsonConvert.DeserializeObject<ApiResponse>(response.Content);
        return result.SuggestedTapTags.Items.Select(x => x.CellTrackingInfo.SearchString + x.Title);
    }
}