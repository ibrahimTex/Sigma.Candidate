using Sigma.Candidate.Contracts.DTOs;

namespace Sigma.Candidate.Contracts.Services.SaveCandidate
{
	public record SaveCandidateResponse(CandidateDTO Candidate) : BaseResponse;
}
