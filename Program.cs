using Microsoft.EntityFrameworkCore;
using MITC_Smart_Solution.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

SQLitePCL.Batteries.Init();

builder.Services.AddDbContext<SmartSolutionContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SmartSolution")));
    

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
