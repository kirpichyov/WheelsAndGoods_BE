using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WheelsAndGoods.DataAccess.Connection;
using WheelsAndGoods.DataAccess.Contracts;
using WheelsAndGoods.DataAccess.Repositories;

namespace WheelsAndGoods.DataAccess;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDataAccessServices(
		this IServiceCollection services,
		IConfiguration configuration,
		IHostEnvironment environment)
	{
		services.AddDbContext<DatabaseContext>(options =>
			{
				options.UseNpgsql(configuration.GetConnectionString(nameof(DatabaseContext)), npgsql =>
					{
						npgsql.MigrationsAssembly("WheelsAndGoods.DataAccess.Migrations");
					})
					.UseSnakeCaseNamingConvention();

				AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

				if (environment.IsDevelopment())
				{
					options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
					options.EnableSensitiveDataLogging();
				}
			}
		);

		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}
}
