
using cheburechnaya_core.Controllers;
using MediatR;

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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ConfigureCorsPolicy(WebApplicationBuilder builder) {
	builder.Services.AddCors(opt => {
		opt.AddPolicy("CorsPolicy", policy => {
			policy.AllowAnyHeader()
				.AllowAnyMethod()
				.WithOrigins("http://localhost:8080")
				.AllowCredentials();
		});
	});
}