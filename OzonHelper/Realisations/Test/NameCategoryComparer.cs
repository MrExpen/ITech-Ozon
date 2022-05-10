using OzonHelper.Services;

namespace OzonHelper.Realisations.Test;

public class NameCategoryComparer : INameCategoryComparer
{
    public double GetSuggestions(string name, string category)
        => Random.Shared.NextDouble() * 100;

    public Task<double> GetSuggestionsAsync(string name, string category, CancellationToken token = default)
        => Task.FromResult(GetSuggestions(name, category));
}