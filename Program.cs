using System.Security.Claims;
using System.Text;
using ApiGateway.Src.Interceptors;
using Microsoft.IdentityModel.Tokens;
using ApiGateway.DTOs; // Asegúrate de que esta línea esté agregada para los DTOs que has integrado.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Agregar clientes gRPC para interactuar con los microservicios (si los usas)
builder.Services.AddGrpcClient<UserManagementService.Grpc.UserService.UserServiceClient>(options =>
{
    options.Address = new Uri("http://localhost:5171");
})
.AddInterceptor<AuthInterceptor>();

builder.Services.AddGrpcClient<CareerService.Grpc.CareerService.CareerServiceClient>(options =>
{
    options.Address = new Uri("http://localhost:5274");
})
.AddInterceptor<AuthInterceptor>();

// Configurar autenticación con JWT
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASuperSecureKey12345!@#$%67890")),
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role
        };

        options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Headers.ContainsKey("Authorization"))
                {
                    var token = context.Request.Headers["Authorization"];
                    Console.WriteLine($"Middleware JWT Token recibido: {token}");
                    context.Token = token.ToString().Replace("Bearer ", "");
                }
                else
                {
                    Console.WriteLine("Middleware JWT: No se encontró el encabezado 'Authorization'.");
                }

                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Middleware JWT Error: {context.Exception.Message}");
                return Task.CompletedTask;
            }
        };
    });

// Agregar autorización y configuraciones adicionales
builder.Services.AddAuthorization();
builder.Services.AddTransient<AuthInterceptor>();
builder.Services.AddHttpContextAccessor();

// Configuración para hacer solicitudes HTTP al microservicio AuthService
builder.Services.AddHttpClient(); // Esto te permitirá hacer peticiones HTTP desde la API Gateway

// Swagger para documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar Swagger y UI de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Usar autenticación JWT
app.UseAuthorization(); // Usar autorización
app.MapControllers(); // Mapear los controladores

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
