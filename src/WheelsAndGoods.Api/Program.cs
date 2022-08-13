using Serilog;
using WheelsAndGoods.Api.Configuration;

namespace WheelsAndGoods.Api;

public static class Program
{
	public static void Main(string[] args)
	{
		var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.Build();

		SerilogConfigurator.Configure(configuration);

		try
		{
			Log.Information("Application Starting Up");
			CreateHostBuilder(args).Build().Run();
		}
		catch (Exception exception)
		{
			Log.Fatal(exception, "The application failed to start correctly");
		}
		finally
		{
			Log.CloseAndFlush();
		}
	}

	private static IHostBuilder CreateHostBuilder(string[] args)
	{
		return Host.CreateDefaultBuilder(args)
			.UseDefaultServiceProvider((_, options) =>
			{
				options.ValidateScopes = true;
				options.ValidateOnBuild = true;
			})
			.UseSerilog()
			.ConfigureWebHostDefaults(webBuilder =>
			{
				webBuilder.UseStartup<Startup>();
			});
	}
}
