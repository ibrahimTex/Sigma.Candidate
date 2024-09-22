using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Sigma.Candidate.Contracts.Exceptions;

namespace Sigma.Candidate.Api.Infrastructure
{
	public class ExceptionFilter : IExceptionFilter, IFilterMetadata
	{
		public void OnException(ExceptionContext context)
		{
			Log.Error(context.Exception, "Opps, Something went wrong.");

			var serviceError = CreateServiceError(context.Exception);

			context.Result = new ObjectResult(serviceError);

			context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
			context.ExceptionHandled = true;
		}

		private static ServiceError CreateServiceError(Exception exception)
		{
			if (exception is ServiceException ex)
			{
				return new ServiceError()
				{
					IsSuccess = false,
					Errors = ex.Errors,
					Message = ex.Message
				};
			}

			return new ServiceError()
			{
				IsSuccess = false,
				Errors = [exception.InnerException?.Message],
				Message = exception.Message
			};
		}
	}
}
