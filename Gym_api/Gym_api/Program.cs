using Gym_api.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var gymConnectionsString = builder.Configuration.GetConnectionString("GymDB");
builder.Services.AddDbContext<GymDbContext>(options =>
    options.UseSqlServer(gymConnectionsString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Obs³uga pliku index.html z folderu Frontend
app.MapGet("/", async context =>
{
    context.Response.ContentType = "text/html";
    await context.Response.SendFileAsync("Frontend/index.html");
});


// Mapowanie kontrolerów
app.MapControllers();

app.UseCors("AllowAll");

app.Run();
