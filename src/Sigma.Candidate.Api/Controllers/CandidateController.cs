using Microsoft.AspNetCore.Mvc;
using Sigma.Candidate.Contracts.Services;
using Sigma.Candidate.Contracts.Services.SaveCandidate;

namespace Sigma.Candidate.Api.Controllers;

public class CandidateController(ICandidateService service) : ControllerBase
{
	private readonly ICandidateService _service = service;

	/// <summary>
	/// Api to add and update the candidate using the email as identifier
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	[ProducesResponseType(typeof(SaveCandidateResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
	[HttpPost("SaveCandidate")]
	public async Task<IActionResult> SaveCandidateAsync([FromBody] SaveCandidateRequest request)
	{
		var response = await _service.SaveCandidateAsync(request);
		return Ok(response);
	}
}