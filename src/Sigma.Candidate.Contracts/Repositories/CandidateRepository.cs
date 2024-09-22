using Sigma.Candidate.Contracts.DTOs;
using Sigma.Candidate.Contracts.Services.SaveCandidate;

namespace Sigma.Candidate.Contracts.Repositories;

public interface ICandidateRepository
{
	public Task<SaveCandidateResponse> SaveCandidateAsync(CandidateDTO candidateDTO);
}