
using Microsoft.EntityFrameworkCore;
using Songs.Data;
using System.Text.Json.Serialization;

namespace Songs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Använder MySql och använder anslutningssträngen "MySqlSongString"
            builder.Services.AddDbContext<SongContext>(options =>
            options.UseMySql(builder.Configuration.GetConnectionString("MySqlSongString"), new MySqlServerVersion(new Version())));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }

    }
}