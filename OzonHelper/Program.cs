using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using OfficialOzonApi;
using OzonHelper.Data;
using OzonHelper.Realisations;
using OzonHelper.Realisations.Test;
using OzonHelper.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers().AddNewtonsoftJson();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => 
     options.AddDefaultPolicy(corsPolicyBuilder => corsPolicyBuilder 
         .AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddSingleton(new OfficialOzonApiClient(
    Environment.GetEnvironmentVariable("OZON_TOKEN") ?? "",
    int.Parse(Environment.GetEnvironmentVariable("OZON_COMPANY_ID") ?? "0")
));

builder.Services.AddSingleton<IApiAdapter, ApiAdapter>();



builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder
    => optionsBuilder.UseSqlite("Data Source=data.db").UseLazyLoadingProxies());

builder.Services.AddScoped<IKeyWordHelper, TestKeyWordHelper>();
builder.Services.AddScoped<IDumpsHelper, DumpHelper>();
builder.Services.AddScoped<INamingHelper, DbNamingHelper>();
builder.Services.AddScoped<IPriceHelper<PriceInfo>, DbPriceHelper>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
