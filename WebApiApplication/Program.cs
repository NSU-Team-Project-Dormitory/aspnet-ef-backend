using DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddScoped<OldDbContext>();
builder.Services.AddDbContext<DormitoryDbContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString("Database"));
    });

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

app.UseAuthorization();

app.MapControllers();

app.Run();