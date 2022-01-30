using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<Data.AppointmentsDbContext>(
	config => config.UseNpgsql(connectionString)
);

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
