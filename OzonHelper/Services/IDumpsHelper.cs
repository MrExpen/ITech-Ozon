using OzonHelper.Data.Model;

namespace OzonHelper.Services;

public interface IDumpsHelper
{
    Task<IEnumerable<DumpCategoryResponse>> GetDumps(string category, CancellationToken token = default);
    Task<IEnumerable<DumpInfo>> GetDumpsByQuery(string query, CancellationToken token = default);
}