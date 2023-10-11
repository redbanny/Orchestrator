using Microsoft.EntityFrameworkCore;
using OrchestratorAPI.Contexts;
using OrchestratorAPI.JWT.Filters;
using System.Text.Json.Serialization;
using NLog;
using NLog.Web;


var logger = NLogBuilder.ConfigureNLog("Nlog.config").GetCurrentClassLogger();
logger.Info("Запуск сервиса согласования");
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddJsonFile("appsettings.json");
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();   

    #region Add services to the container.
    builder.Services.AddDbContext<TurnDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetSection("DefaultConnection").Value));
    builder.Services.AddControllers().AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
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
}catch(Exception ex)
{
    logger.Error(ex, "Остановка программы из-за исключения");
}
finally
{
    LogManager.Shutdown();
}

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