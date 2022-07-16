
using cheburechnaya_core.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigureCorsPolicy(builder);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddControllers(option => {
	option.Filters.Add<ValidationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
        ConfigAuthOptions(options)
	);
builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();

static void ConfigureCorsPolicy(WebApplicationBuilder builder) {
	builder.Services.AddCors(opt => {
		opt.AddPolicy("CorsPolicy", policy => {
			policy.AllowAnyHeader()
				.AllowAnyMethod()
				.WithOrigins("http://localhost:8080", "https://cheburechnaya.vercel.app/")
				.AllowCredentials();
		});
	});
}

void ConfigAuthOptions(JwtBearerOptions options) {
    options.TokenValidationParameters = new TokenValidationParameters {
        // ���������, ����� �� �������������� �������� ��� ��������� ������
        ValidateIssuer = true,
        // ������, �������������� ��������
        ValidIssuer = AuthOptions.ISSUER,
        // ����� �� �������������� ����������� ������
        ValidateAudience = true,
        // ��������� ����������� ������
        ValidAudience = AuthOptions.AUDIENCE,
        // ����� �� �������������� ����� �������������
        ValidateLifetime = true,
        // ��������� ����� ������������
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        // ��������� ����� ������������
        ValidateIssuerSigningKey = true,
    };
}
public class AuthOptions {
    public const string ISSUER = "https://localhost:7065"; // �������� ������
    public const string AUDIENCE = "https://localhost:8080"; // ����������� ������
    const string KEY = "qwert#321_secret";   // ���� ��� ��������
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}