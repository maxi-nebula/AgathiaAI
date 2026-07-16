var builder = WebApplication.CreateBuilder(args);

// Register controller support with the dependency-injection container.
builder.Services.AddControllers();

// Register OpenAPI document generation.
builder.Services.AddOpenApi();

var app = builder.Build();

// Expose the OpenAPI document only during development.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Connect attribute-routed controller actions to the HTTP pipeline.
app.MapControllers();

app.Run();