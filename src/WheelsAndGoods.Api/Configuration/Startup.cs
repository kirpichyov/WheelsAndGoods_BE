using FluentValidation;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;
using WheelsAndGoods.Api.Configuration.Middleware.Filters;
using WheelsAndGoods.Api.Configuration.Middleware.Filters.Exceptions;
using WheelsAndGoods.Api.Configuration.Swagger;
using WheelsAndGoods.Api.Validation;
using WheelsAndGoods.Application;
using WheelsAndGoods.DataAccess;

namespace WheelsAndGoods.Api.Configuration;

public class Startup
{
	private readonly IConfiguration _configuration;
	private readonly IWebHostEnvironment _environment;

	public Startup(IConfiguration configuration, IWebHostEnvironment environment)
	{
		_configuration = configuration;
		_environment = environment;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddHttpContextAccessor();

		// services.AddFriendlyJwt();

		services.AddDataAccessServices(_configuration, _environment);
		services.AddApplicationServices(_configuration);

		services.AddRouting(options => options.LowercaseUrls = true);
		services.AddCors();

		services.AddControllers()
			.AddNewtonsoftJson(options =>
			{
				options.SerializerSettings.Converters.Add(
					new StringEnumConverter(new CamelCaseNamingStrategy())
				);
			})
			// .AddFriendlyJwtAuthentication(configuration =>
			// {
			//     //var authOptions = _configuration.BindFromAppSettings<AuthOptions>();
			//     //configuration.Bind(authOptions);
			// })
			.AddMvcOptions(options =>
			{
				options.Filters.Add<ExceptionFilter>();
				options.Filters.Add<FluentValidationFilter>();
			})
			.ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

		services.AddValidatorsFromAssemblyContaining<SampleValidator>();

		services.AddEndpointsApiExplorer();
		services.AddSwaggerGenNewtonsoftSupport()
			.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo {Version = "v1", Title = "WheelsAndGoods API"});

				options.AddSecurityDefinition("Bearer",
					new OpenApiSecurityScheme
					{
						Name = HeaderNames.Authorization,
						Type = SecuritySchemeType.OAuth2,
						Scheme = "Basic",
						In = ParameterLocation.Header,
						Description = "Obtained JWT."
					});

				options.OperationFilter<AuthOperationFilter>();
			});
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseCors(builder =>
			{
				builder.AllowAnyOrigin();
				builder.AllowAnyHeader();
				builder.AllowAnyMethod();
			});

			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
				options.RoutePrefix = string.Empty;
			});
		}
		else
		{
			app.UseCors(builder =>
			{
				// var authOptions = _configuration.BindFromAppSettings<AuthOptions>();

				builder.AllowAnyHeader();
				builder.AllowAnyMethod();
				// builder.WithOrigins(authOptions.CorsAllowedList);
			});
		}

		app.UseHttpsRedirection();
		app.UseSerilogRequestLogging();
		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
			endpoints.MapGet("/healthcheck",
				async context => { await context.Response.WriteAsync($"Healthy! [{DateTime.UtcNow}]"); });
		});
	}
}
