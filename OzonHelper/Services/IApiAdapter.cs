using OzonHelper.Data.Model;

namespace OzonHelper.Services;

public interface IApiAdapter
{
    Task<IEnumerable<Category>?> GetCategoryTreeAsync(string search = "", CancellationToken token = default);
    Task<IEnumerable<string>?> GetSuggestedName(string query, CancellationToken token = default);
    Task<IEnumerable<Search>?> GetUserSearchesAsync(DumpWeeks weeks = DumpWeeks.One, string text = "", int offset = 0,
        int limit = 50, IEnumerable<int>? categories = null, CancellationToken token = default);
    
    Task<SiteDump?> DumpDataAsync(DumpWeeks weeks = DumpWeeks.One, CancellationToken token = default);
}