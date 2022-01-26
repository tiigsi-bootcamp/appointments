var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add Swagger services.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.MapGet("/home", () => "Hello World!");

app.Run();
