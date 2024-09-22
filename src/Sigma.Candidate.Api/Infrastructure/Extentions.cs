using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Sigma.Candidate.Contracts.Repositories;
using Sigma.Candidate.Contracts.Services;
using Sigma.Candidate.DataAccess;
using Sigma.Candidate.DataAccess.Repositories;
using Sigma.Candidate.Service.Services;
using Sigma.Candidate.Service.Validator;

namespace Sigma.Candidate.Api.Infrastructure;

public static class Extentions
{
	public static WebApplicationBuilder AddServicesAndInfra(this WebApplicationBuilder applicationBuilder)
	{
		applicationBuilder.Services.InjectDatabase(applicationBuilder.Configuration.GetConnectionString("CandidateConstr"));

		applicationBuilder.Services.InjectServices();

		applicationBuilder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

		applicationBuilder.Services.AddValidatorsFromAssemblyContaining<SaveCandidateValidator>();

		applicationBuilder.Services.AddSwaggerGen();

		return applicationBuilder;
	}

	public static IServiceCollection InjectServices(this IServiceCollection services)
	{
		services.AddScoped<ICandidateService, CandidateService>();
		services.AddScoped<ICandidateRepository, CandidateRepository>();

		return services;
	}

	public static IServiceCollection InjectDatabase(this IServiceCollection services, string connectionString)
	{
		services.AddDbContext<CandidateContext>(options =>
		{
			options.UseSqlServer(connectionString);
		});

		return services;
	}

	/// <summary>
	/// // Create the database if it does not exist
	/// </summary>
	public static WebApplication CheckIfDbExists(this WebApplication app)
	{
		using (var scope = app.Services.CreateScope())
		{
			var context = scope.ServiceProvider.GetRequiredService<CandidateContext>();
			context.Database.EnsureCreated();
		}

		return app;
	}
}
