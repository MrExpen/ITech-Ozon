namespace CoreLibrary.Services;

public interface INameCategoryComparer
{
    double GetSuggestions(string name, string category);
    Task<double> GetSuggestionsAsync(string name, string category, CancellationToken token = default);
}