using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");
Console.WriteLine($"Connection: {connectionString}");
builder.Services.AddDbContext<Data.AppointmentsDbContext>(
	config => config.UseNpgsql(connectionString)
);

builder.Services.AddControllers()
	.AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAuthentication("Bearer") // Token: JWT = Json Web Token
	.AddJwtBearer(config =>
	{
		config.RequireHttpsMetadata = false;

		var keyInput = "random_text_with_at_least_32_chars";

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyInput));

		config.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidIssuer = "MyAPI",
			ValidateAudience = true,
			ValidAudience = "MyFrontendApp",
			ValidateLifetime = true,
			IssuerSigningKey = key
		};
	});

// Add Swagger services.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable/Configure CORS.
builder.Services.AddCors(config =>
{
	config.AddDefaultPolicy(policy => policy
		.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader()
	);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/home", () => "Hello World!");

app.Run();
