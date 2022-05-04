using CoreLibrary.Realisations;
using CoreLibrary.Services;
using Microsoft.AspNetCore.Mvc;

namespace OzonHelper.Controllers;

[ApiController]
[Route("[controller]")]
public class OzonController : ControllerBase
{
    private readonly ILogger<OzonController> _logger;
    private readonly INamingHelper _namingHelper;
    private readonly INameCategoryComparer _nameCategoryComparer;
    private readonly IPriceHelper _priceHelper;
    private readonly IKeyWordHelper _keyWordHelper;


    public OzonController(ILogger<OzonController> logger, INamingHelper namingHelper, INameCategoryComparer nameCategoryComparer, IPriceHelper priceHelper, IKeyWordHelper keyWordHelper)
    {
        _logger = logger;
        _namingHelper = namingHelper;
        _nameCategoryComparer = nameCategoryComparer;
        _priceHelper = priceHelper;
        _keyWordHelper = keyWordHelper;
    }

    [HttpGet]
    [Route("Name")]
    public async Task<IResult<IEnumerable<string>>> GetNameSuggestions([FromQuery] string? query, CancellationToken token)
    {
        _logger.LogDebug("{MethodName}({Query})", nameof(GetNameSuggestions), query);
        var result = new ApiResult<IEnumerable<string>>();
        
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
    [Route("CompareNameCategory")]
    public async Task<IResult<double>> CompareNameCategory([FromQuery] string name, [FromQuery] string category, CancellationToken token)
    {
        _logger.LogDebug("{MethodName}({Name}, {Category})", nameof(CompareNameCategory), name, category);
        var result = new ApiResult<double>();
        
        try
        {
            result.Result = await _nameCategoryComparer.GetSuggestionsAsync(name, category, token);
        }
        catch (Exception e)
        {
            _logger.LogError(e, nameof(CompareNameCategory));
            result.Success = false;
            result.Error = e.Message;
        }

        return result;
    }
    
    [HttpGet]
    [Route("KeyWords")]
    public async Task<IResult<IEnumerable<string>>> GetKeyWords([FromQuery] string? name, [FromQuery] string? category, CancellationToken token)
    {
        _logger.LogDebug("{MethodName}({Name}, {Category})", nameof(GetKeyWords), name, category);
        var result = new ApiResult<IEnumerable<string>>();
        
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
    public async Task<IResult<IPriceInfo>> GetPriceInfo([FromQuery] string name, [FromQuery] string? category, CancellationToken token)
    {
        _logger.LogDebug("{MethodName}({Name}, {Category})", nameof(GetPriceInfo), name, category);
        var result = new ApiResult<IPriceInfo>();
        
        try
        {
            result.Result = await _priceHelper.GetPriceAsync(name, category, token);
        }
        catch (Exception e)
        {
            _logger.LogError(e, nameof(GetPriceInfo));
            result.Success = false;
            result.Error = e.Message;
        }

        return result;
    }
}
