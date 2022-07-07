using GacDirectory.Models;
using GacDirectory.Others;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
// Add services to the container.

builder.Services.ConfigureCors();

builder.Services.AddSwagger();

builder.Services.SetupJWTService(builder.Configuration);

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(config["ConnectionStrings:dbHRMS"]));

//builder.Services.ConfigureDB(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("./swagger/v1/swagger.json", "Directory API V1");
    c.DocumentTitle = "GAC Egy Directory API";
    c.RoutePrefix = string.Empty;
    c.InjectStylesheet("/swagger-ui/custom.css");
});

app.UseCors(builder => builder
       //.AllowAnyOrigin()
       .SetIsOriginAllowed((host) => true)
       .AllowCredentials()
       .AllowAnyHeader()
       .AllowAnyMethod());

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


