using OzonHelper.Services;

namespace OzonHelper.Realisations.Test;

public class KeyWordHelper : IKeyWordHelper
{
    public IEnumerable<string> GetKeyWords(string name, string category)
        => new[]
        {
            "Test",
            "Information",
            "Do not publish",
            name,
            category,
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString()
        };

    public Task<IEnumerable<string>> GetKeyWordsAsync(string name, string category, CancellationToken token = default)
        => Task.FromResult(GetKeyWords(name, category));
}