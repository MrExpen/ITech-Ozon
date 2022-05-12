﻿using OzonHelper.Data.Model;

namespace OzonHelper.Services;

public interface IDumpsHelper
{
    Task<DumpResponse> GetDumps(int categoryId, CancellationToken token = default);
}