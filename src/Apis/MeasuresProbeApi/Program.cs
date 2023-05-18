using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using NLog.Web;
using SensorsApi.Repositories;

namespace SensorsApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
			logger.Debug("init main");

			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddScoped<IMeasureRepository, MeasureRepository>();
			builder.Services.Configure<ServiceSettings>(builder.Configuration.GetSection("ServiceSettings"));
			builder.Services.AddOptions<ServiceSettings>();
 

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Logging.ClearProviders();
			builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
			builder.Host.UseNLog();
			builder.Host.ConfigureLogging((hostingContext, logging) =>
			{
				logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
				logging.AddDebug();
				logging.AddNLog("nlog.config");
			});

			var app = builder.Build();

			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});


			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			//app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}