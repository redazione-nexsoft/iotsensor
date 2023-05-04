using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace SensorsApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.Configure<ServiceSettings>(builder.Configuration.GetSection("ServiceSettings"));
			builder.Services.AddOptions<ServiceSettings>();

			builder.Services.AddHttpClient("MeasuresRepositoryApi", (serviceProvider, httpClient) =>
			{
				var settings = serviceProvider.GetRequiredService<IOptions<ServiceSettings>>().Value;

				httpClient.BaseAddress = new Uri(settings.MeasuresRepositoryApi);

				//// using Microsoft.Net.Http.Headers;
				//// The GitHub API requires two headers.
				//httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/vnd.github.v3+json");
				//httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpRequestsSample");
			});

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

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