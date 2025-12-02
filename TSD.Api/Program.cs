
using Microsoft.EntityFrameworkCore;
using TSD.Data;
using TSD.Data.Extension.SolutionName.Data.Extensions;
using TSD.Services.Mapping;
using AutoMapper;
namespace TSD.Api
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            //builder.Services.AddDbContext<TSD_DbContext>(options =>
            // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
           
            builder.Services.AddDataInfrastructure("DefaultConnection");
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

