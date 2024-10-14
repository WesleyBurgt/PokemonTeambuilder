using Microsoft.EntityFrameworkCore;
using PokémonTeambuilder.core.Classes;
using PokémonTeambuilder.DbContext;

namespace PokémonTeambuilder
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
            webBuilder => webBuilder.UseStartup<Startup>());

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new CamelCaseEnumConverter<StatsEnum>());
            });


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost3000",
                    policy => policy.WithOrigins("http://localhost:3000")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            var connectionString = builder.Configuration.GetConnectionString("PokemonBuilderDatabaseConnectionString");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors("AllowLocalhost3000");

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

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
            => services.AddDbContext<AppDbContext>();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
