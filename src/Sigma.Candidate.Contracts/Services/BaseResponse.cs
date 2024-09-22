namespace Sigma.Candidate.Contracts.Services;

public abstract record BaseResponse
{
	/// <summary>
	/// Indicates the success or failure of the operation.
	/// </summary>
	public bool IsSuccess { get; set; }

	/// <summary>
	/// A human-readable message providing more details about the operation's result.
	/// </summary>
	public string Message { get; set; }

	/// <summary>
	/// A list of errors that occurred during the request.
	/// </summary>
	public List<string> Errors { get; set; }

	protected BaseResponse()
	{
		Errors = [];
		IsSuccess = true;
		Message = string.Empty;
	}
}