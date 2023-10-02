using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using OrchestratorAPI.Contexts;
using OrchestratorAPI.JWT.Filters;
using OrchestratorAPI.JWT.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=QueuesTable;Integrated Security=true";
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

#region Add services to the container.
builder.Services.AddDbContext<TurnDbContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddControllers().AddJsonOptions(x=>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var section = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddSingleton(section.Get<JwtSettings>());
builder.Services.AddScoped<DataServiceSettings>();
builder.Services.AddHttpClient<DataHttpService>((service, client) =>
{
    var setting = service.GetRequiredService<IOptions<DataServiceSettings>>().Value;
    var jwtToken = JwtGenerator.GenerateJWT(setting.SecretKey, setting.Subject, setting.Issuer);

    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.UserAgent.Clear();
    client.BaseAddress = new Uri(setting.Url);
    client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
    client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(setting.AuthScheme, jwtToken);
});
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class DataHttpService
{
    private readonly HttpClient _httpClient;
    public DataHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}

public class DataServiceSettings
{
    public const string ConfigSectionName = "JwtSettings";
    public string Url { get; set; }
    public string SecretKey { get; set; }
    public string Subject { get; set; }
    public string Issuer { get; set; }
    public string AuthScheme { get; set; }
}