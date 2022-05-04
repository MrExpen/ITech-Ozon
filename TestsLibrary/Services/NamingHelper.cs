﻿using CoreLibrary.Services;

namespace TestsLibrary.Services;

public class NamingHelper : INamingHelper
{
    public IEnumerable<string> GetSuggestions(string name)
        => new[]
        {
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString()
        };

    public Task<IEnumerable<string>> GetSuggestionsAsync(string name, CancellationToken token = default)
        => Task.FromResult(GetSuggestions(name));
}