
using Microsoft.EntityFrameworkCore;
using SS23Tiers.Api.Data;
using SS23Tiers.Api.Service;

namespace SS23Tiers.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //Connection String
        var conString = @"Server=localhost;Database=SS23Tiers;User Id=sa;Password=Strong.Pwd-123;TrustServerCertificate=true;";
        builder.Services.AddDbContext<AppDbContext>
            (o => o.UseSqlServer(conString));
        //DI
        builder.Services.AddTransient<IEmployeeService, EmployeeService>();
        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors(builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

