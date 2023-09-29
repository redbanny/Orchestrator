using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.Configuration;
using OrchestratorAPI.Contexts;
using OrchestratorAPI.JWT;
using OrchestratorAPI.JWT.Filters;
using OrchestratorAPI.JWT.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=QueuesTable;Integrated Security=true";
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
// Add services to the container.
builder.Services.AddDbContext<TurnDbContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddControllers().AddJsonOptions(x=>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var section = builder.Configuration.GetSection("JwtSettings");
var jwtSettings = section.Get<JwtSettings>();

builder.Services.AddSingleton(jwtSettings);
JwtGenerator.GenerateJWT(jwtSettings.SecretKey, jwtSettings.Subject, jwtSettings.Issuer);

//builder.Services.AddHttpClient<DataHttpService>((service, client) =>
//{
//    var setting = service.GetRequiredService<IOptions<JwtSettings>>().Value;
//    var jwtToken = JwtGenerator.GenerateJWT(setting.SecretKey, setting.Subject, setting.Issuer);

//    client.DefaultRequestHeaders.Accept.Clear();
//    client.DefaultRequestHeaders.UserAgent.Clear();
//    client.BaseAddress = new Uri(setting.Url);
//    client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
//    client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
//    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(setting.AuthScheme, jwtToken);
//});


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