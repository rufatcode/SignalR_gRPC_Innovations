using Microsoft.AspNetCore.OutputCaching;
using SignalR_gRPC_Innovations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Registration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseOutputCache();
app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/Method",() =>
{
    return Results.Ok(DateTime.Now);
}).CacheOutput();

app.MapGet("/Attribute", [OutputCache] () =>
{
    return Results.Ok(DateTime.Now);
});

app.Run();

