using Microsoft.EntityFrameworkCore;
using PokémonTeambuilder.dal.DbContext;

namespace PokémonTeambuilder
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

            builder.Services.AddHttpClient("CustomClient")
                .ConfigureHttpClient(client =>
                {
                    client.Timeout = TimeSpan.FromMinutes(5);
                });

            var connectionString = builder.Configuration.GetConnectionString("PokemonBuilderDatabaseConnectionString");

            builder.Services.AddDbContext<PokemonTeambuilderDbContext>(options =>
                options.UseSqlServer(connectionString));

            var app = builder.Build();

            app.UseCors(options =>
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

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
