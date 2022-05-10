using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using OfficialOzonApi;
using OfficialOzonApi.Models.Stats;
using OfficialOzonApi.Utils;

var api = new OfficialOzonApiClient(
    "AeKJEAAAAAAAFGg5VnNCdlMwOGtDTkZzYWVtSWgzA3dlYlMAAAAARW5QcIABAABFNs8KgQEAAAENU2VjdXJpdHlTdGFtcA0xNjUxMTUxNzYwMDAw.//iqvWh4n6Xwe8d+JSpcSv/lvZdgUtqFiFd3dXTHUfo=",
    457192);

var adapter = new OzonApiAdapter(api, new NullLogger<OzonApiAdapter>());
//
// // var res = await adapter.GetSuggestedNames("Процессор");
// // var res2 = await adapter.GetNodesTree();
// // var res3 = res2.SelectMany(x => x.GetAllNodesRecursion()).ToArray();
//
// // var res1 = await adapter.GetUserSearchResultsAsync(days: 28);
//
// for (int i = 40; i <= 40; i++)
// {
//     var result = await adapter.GetUserSearchResultsAsync(days: 28, offset: 50 * i, limit:20);
//     await File.WriteAllTextAsync($"data_28_{i}_{DateTime.Today.ToString("yyyy-M-d")}.json",
//         JsonConvert.SerializeObject(result));
// }

// var list = new List<SearchResult>();
//
// for (int i = 0; i < 40; i++)
// {
//     var data = await File.ReadAllTextAsync($"data_28_{i}_{DateTime.Today.ToString("yyyy-M-d")}.json");
//     list.AddRange(JsonConvert.DeserializeObject<IEnumerable<OfficialOzonApi.Models.Stats.SearchResult>>(data));
// }

var optionsBuilder = new DbContextOptionsBuilder<CoreLibrary.Data.ApplicationDbContext>();
optionsBuilder.UseSqlite("Data Source=data.db;").UseLazyLoadingProxies();
 
await using var db = new CoreLibrary.Data.ApplicationDbContext(optionsBuilder.Options);
await db.Database.EnsureCreatedAsync();

// await DbUtils.PrepareDb(adapter, db);

// await DbUtils.DumpDataAsync(adapter, db);

Console.WriteLine();