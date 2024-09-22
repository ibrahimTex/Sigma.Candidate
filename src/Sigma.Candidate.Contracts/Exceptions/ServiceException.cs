namespace Sigma.Candidate.Contracts.Exceptions;

public class ServiceException : Exception
{
	public List<string> Errors { get; }

	public ServiceException(string message, List<string> errors) : base(message)
	{
		Errors = errors;
	}
}
