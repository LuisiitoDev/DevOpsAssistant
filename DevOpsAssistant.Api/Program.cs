using DevOpsAssistant.Api.Configuration;
using DevOpsAssistant.Api.Extensions;
using DevOpsAssistant.Application.Extensions;
using DevOpsAssistant.Infrastructure.Extensions;
using Microsoft.AspNetCore.OpenApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    options.SerializerOptions.WriteIndented = true;

});


builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<OpenApiDocumentTransformer>();
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowAnyOrigin();
    });
});


builder.Services.AddApplication();

builder.Services.AddInfraestructure(builder.Configuration);

builder.Services.AddProblemDetails();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.WithTitle("LuisiitoDev - DevOps Assistant API")
               .WithTheme(ScalarTheme.Kepler)
               .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
               .WithModels(true)
               .WithDarkMode(true);
    });
}

app.UseHttpsRedirection();

app.MapDevOpsEndpoints();

// Add info endpoint
app.MapGet("/info", () => TypedResults.Ok(new
{
    Service = "DevOps Assistant API",
    Version = "1.0.0",
    Owner = "LuisiitoDev",
    CurrentDateTime = "2025-07-06 14:21:35",
    TimeZone = "UTC",
    ApiSpecification = "OpenAPI 3.0",
    OpenApiEndpoint = "/openapi/v1.json",
    DocumentationUrl = "/scalar/v1",
    PlaygroundUrl = "/scalar/v1",
    Status = "Healthy"
}))
.WithName("GetInfo")
.WithSummary("API Information")
.ExcludeFromDescription();

app.Run();