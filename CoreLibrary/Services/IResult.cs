namespace CoreLibrary.Services;

public interface IResult<T> : IResult
{
    T? Result { get; }
}

public interface IResult
{
    string? Error { get; }
    bool Success { get; } 
}