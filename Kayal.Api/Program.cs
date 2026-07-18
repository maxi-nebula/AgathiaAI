#pragma warning disable OPENAI001

using Azure.Identity;
using Kayal.Api.Services;
using OpenAI.Responses;
using System.ClientModel.Primitives;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<ResponsesClient>(serviceProvider =>
{
    IConfiguration configuration =
        serviceProvider.GetRequiredService<IConfiguration>();

    string endpoint =
        configuration["AzureOpenAI:Endpoint"]
        ?? throw new InvalidOperationException(
            "AzureOpenAI:Endpoint is missing.");

    BearerTokenPolicy tokenPolicy = new(
        new DefaultAzureCredential(),
        "https://ai.azure.com/.default");

    return new ResponsesClient(
        authenticationPolicy: tokenPolicy,
        options: new ResponsesClientOptions
        {
            Endpoint = new Uri(endpoint)
        });
});

builder.Services.AddScoped<IKayalService, KayalService>();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Not needed while localhost is HTTP-only.
// app.UseHttpsRedirection();

app.MapControllers();

app.Run();