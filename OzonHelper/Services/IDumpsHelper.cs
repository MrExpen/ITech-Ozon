namespace OzonHelper.Services;

public interface IDumpsHelper
{
    Task<IDumpsInfoResult> GetDumps(int categoryId, CancellationToken token = default);
}