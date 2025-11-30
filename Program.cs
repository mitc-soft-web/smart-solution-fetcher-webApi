using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MITC_Smart_Solution.Context;
using MITC_Smart_Solution.Implementation;
using MITC_Smart_Solution.Interface;
using MITC_Smart_Solution.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

SQLitePCL.Batteries.Init();

builder.Services.AddDbContext<SmartSolutionContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SmartSolution"))).AddMemoryCache();
    
builder.Services.AddScoped<ISmartSolutionGeneratorService, SmartSolutionGeneratorService>();
builder.Services.AddSingleton<SearchCategorizer>();
builder.Services.AddSingleton<QueryNormalizer>();
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
                o.TokenLifespan = TimeSpan.FromHours(3));

builder.Services.AddHttpClient("mitc-smart-solution");


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MITC Smart Solution API V1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
