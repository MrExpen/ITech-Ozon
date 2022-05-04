using CoreLibrary.Services;
using TestsLibrary.Services;
using NamingHelper = SiteParserLibrary.Services.NamingHelper;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers().AddNewtonsoftJson();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IKeyWordHelper, KeyWordHelper>();
builder.Services.AddScoped<INameCategoryComparer, NameCategoryComparer>();
builder.Services.AddScoped<INamingHelper, NamingHelper>();
builder.Services.AddScoped<IPriceHelper, PriceHelper>();


var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();