using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:3000");
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
    });
});

// builder.Services.AddScoped<OldDbContext>();
builder.Services.AddDbContext<DormitoryDbContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString("Database"));
    });

builder.Services.AddScoped<IResidentRepository, ResidentRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IFloorRepository, FloorRepository>();

var app = builder.Build();

// using var scope = app.Services.CreateScope();
// await using var dbContext = scope.ServiceProvider.GetRequiredService<OldDbContext>();
// await dbContext.Database.EnsureCreatedAsync();

// Configure the HTTP request pipeline.
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