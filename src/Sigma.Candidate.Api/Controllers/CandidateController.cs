using Microsoft.AspNetCore.Mvc;
using Sigma.Candidate.Contracts.Services;

namespace Sigma.Candidate.Api.Controllers;

public class CandidateController(ICandidateService service) : ControllerBase
{
	private readonly ICandidateService _service = service;

	[HttpPost("SaveCandidate")]
	public IActionResult SaveCandidate()
	{
		//var response = _service.
		return Ok();
	}
}
