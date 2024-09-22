using Serilog;
using Sigma.Candidate.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.AddServicesAndInfra();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "Candidate.Api");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
