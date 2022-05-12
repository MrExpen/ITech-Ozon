using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OzonHelper.Data;
using OzonHelper.Realisations;
using  OzonHelper.Services;
using OzonHelper.Utils;

namespace OzonHelper.Controllers;

[ApiController]
[Route("[controller]")]
public class OzonController : ControllerBase
{
    private readonly IApiAdapter _apiAdapter;
    private readonly ILogger<OzonController> _logger;
    private readonly INamingHelper _namingHelper;
    private readonly IDumpsHelper _dumpsHelper;
    private readonly IPriceHelper<PriceInfo> _priceHelper;
    private readonly IKeyWordHelper _keyWordHelper;
    private readonly ApplicationDbContext _db;


    public OzonController(ILogger<OzonController> logger, INamingHelper namingHelper, IPriceHelper<PriceInfo> priceHelper, IKeyWordHelper keyWordHelper, ApplicationDbContext db, IDumpsHelper dumpsHelper, IApiAdapter apiAdapter)
    {
        _logger = logger;
        _namingHelper = namingHelper;
        _priceHelper = priceHelper;
        _keyWordHelper = keyWordHelper;
        _db = db;
        _dumpsHelper = dumpsHelper;
        _apiAdapter = apiAdapter;
    }

    [HttpGet]
    [Route("Name")]
    public async Task<IResult<IEnumerable<string>>> GetNameSuggestions([FromQuery] string? query, CancellationToken token)
    {
        _logger.LogDebug("{MethodName}({Query})", nameof(GetNameSuggestions), query);
        var result = new Realisations.ApiResult<IEnumerable<string>>();
        
        try
        {
            result.Result = await _namingHelper.GetSuggestionsAsync(query, token);
        }
        catch (Exception e)
        {
            _logger.LogError(e, nameof(GetNameSuggestions));
            result.Success = false;
            result.Error = e.Message;
        }

        return result;
    }

    [Obsolete]
    [HttpGet]
    [Route("KeyWords")]
    public async Task<IResult<IEnumerable<string>>> GetKeyWords([FromQuery] string? name, [FromQuery] string? category, CancellationToken token)
    {
        _logger.LogDebug("{MethodName}({Name}, {Category})", nameof(GetKeyWords), name, category);
        var result = new Realisations.ApiResult<IEnumerable<string>>();
        
        try
        {
            result.Result = await _keyWordHelper.GetKeyWordsAsync(name, category, token);
        }
        catch (Exception e)
        {
            _logger.LogError(e, nameof(GetKeyWords));
            result.Success = false;
            result.Error = e.Message;
        }

        return result;
    }
    
    [HttpGet]
    [Route("Price")]
    public async Task<IResult<IEnumerable<PriceInfo>>> GetPriceInfo([FromQuery] int? categoryId, [FromQuery] string? categoryName, CancellationToken token)
    {
        _logger.LogDebug("{MethodName}({CategoryId}, {CategoryName})", nameof(GetPriceInfo), categoryId, categoryName);
        var result = new ApiResult<IEnumerable<PriceInfo>>();

        try
        {
            IEnumerable<PriceInfo> r;
            if (categoryId.HasValue)
            {
                r = await _priceHelper.GetPriceAsync(categoryId.Value, token);
            }
            else
            {
                r = await _priceHelper.GetPriceAsync(categoryName, token);
            }

            result.Result = r;
        }
        catch (Exception e)
        {
            _logger.LogError(e, nameof(GetPriceInfo));
            result.Success = false;
            result.Error = e.Message;
        }

        return result;
    }
    
    [HttpGet]
    [Route("Dumps")]
    public async Task<IResult<IDumpsInfoResult>> GetDumpsInfo([FromQuery] int categoryId, CancellationToken token)
    {
        _logger.LogDebug("{MethodName}({CategoryId})", nameof(GetDumpsInfo), categoryId);
        var result = new ApiResult<IDumpsInfoResult>();

        try
        {
            result.Result = await _dumpsHelper.GetDumps(categoryId, token);
        }
        catch (Exception e)
        {
            _logger.LogError(e, nameof(GetPriceInfo));
            result.Success = false;
            result.Error = e.Message;
        }

        return result;
    }

    [HttpGet]
    [Route("DumpDate_TEST")]
    public async Task<IActionResult> Dump()
    {
        await _db.Database.EnsureCreatedAsync();
        
        var dump = await _apiAdapter.DumpDataAsync();
        var categories = (await _apiAdapter.GetCategoryTreeAsync()).ToArray();

        await _db.Categories.ClearAsync();
        await _db.Categories.AddRangeAsync(categories);
        await _db.SaveChangesAsync();

        await _db.SiteDumps.AddAsync(dump!);
        await _db.SaveChangesAsync();
        
        return Ok();
    }
}
