using Microsoft.EntityFrameworkCore;
using Serilog;
using Sigma.Candidate.DataAccess;

namespace Sigma.Candidate.Api.Infrastructure;

public static class Extentions
{
	public static WebApplicationBuilder AddServicesAndInfra(this WebApplicationBuilder applicationBuilder)
	{
		applicationBuilder.Services.InjectDatabase(applicationBuilder.Configuration.GetConnectionString("CandidateConstr"));

		applicationBuilder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

		return applicationBuilder;
	}

	public static IServiceCollection InjectDatabase(this IServiceCollection services, string connectionString)
	{
		services.AddDbContext<CandidateContext>(options =>
		{
			options.UseSqlServer(connectionString);
		});

		return services;
	}
}
