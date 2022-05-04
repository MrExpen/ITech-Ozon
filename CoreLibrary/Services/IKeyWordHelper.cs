namespace CoreLibrary.Services;

public interface IKeyWordHelper
{
    IEnumerable<string> GetKeyWords(string name, string category);
    Task<IEnumerable<string>> GetKeyWordsAsync(string name, string category, CancellationToken token = default);
}