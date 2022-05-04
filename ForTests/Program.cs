using OfficialOzonApi;

var api = new OfficialOzonApiClient(
    "",
    0);

var adapter = new OzonApiAdapter(api);

var res = await adapter.GetSuggestedNames("Процессор");
var res2 = await adapter.GetNodesTree();
var res1 = await adapter.GetUserSearchResultsAsync(text: "Процессор");


Console.WriteLine();