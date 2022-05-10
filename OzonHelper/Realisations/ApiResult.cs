using OzonHelper.Services;
using IResult = OzonHelper.Services.IResult;

namespace OzonHelper.Realisations;

public class ApiResult : IResult
{
    public string? Error { get; set; }
    public bool Success { get; set; }

    public ApiResult(bool success = true, string? error = null)
    {
        Error = error;
        Success = success;
    }
}

public class ApiResult<T> : ApiResult, IResult<T>
{
    public T? Result { get; set; }

    public ApiResult(T? result = default, bool success = true, string? error = null) : base(success, error)
    {
        Result = result;
    }
}