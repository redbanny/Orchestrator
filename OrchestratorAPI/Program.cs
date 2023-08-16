using Microsoft.EntityFrameworkCore;
using OrchestratorAPI.Contexts;
using System.Text.Json.Serialization;

string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=QueuesTable;Integrated Security=true";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TurnDbContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddControllers().AddJsonOptions(x=>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
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