using Serilog;
using Sigma.Candidate.Api.Infrastructure;
using Sigma.Candidate.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
	options.Filters.Add(typeof(ExceptionFilter));
});

builder.AddServicesAndInfra();

var app = builder.Build();

app.CheckIfDbExists();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "Candidate.Api");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
