using Sigma.Candidate.Contracts.Services.SaveCandidate;

namespace Sigma.Candidate.Contracts.Services;

public interface ICandidateService
{
	Task<SaveCandidateResponse> SaveCandidateAsync(SaveCandidateRequest request);
}
