using Mapster;
using Microsoft.EntityFrameworkCore;
using Sigma.Candidate.Contracts.DTOs;
using Sigma.Candidate.Contracts.Repositories;
using Sigma.Candidate.Contracts.Services.SaveCandidate;

namespace Sigma.Candidate.DataAccess.Repositories;

public class CandidateRepository(CandidateContext context) : ICandidateRepository
{
	private readonly CandidateContext _context = context;

	public async Task<SaveCandidateResponse> SaveCandidateAsync(CandidateDTO candidateDTO)
	{
		var candidateModel = await GetCandidateAsync(candidateDTO.Email);
		Models.Candidate candidate = candidateDTO.Adapt<Models.Candidate>();

		if (candidateModel is null)
		{
			await AddCandidateAsync(candidate);
			return GenerateResponse(candidate, true);
		}

		await UpdateCandidateAsync(candidateModel, candidate);
		return GenerateResponse(candidate, false);
	}

	#region Private methods
	private async Task<Models.Candidate> GetCandidateAsync(string email)
	{
		return await _context.Candidates
							 .FirstOrDefaultAsync(c => c.Email == email);
	}

	private async Task<Models.Candidate> AddCandidateAsync(Models.Candidate candidate)
	{
		await _context.Candidates.AddAsync(candidate);
		await _context.SaveChangesAsync();

		return candidate;
	}

	private async Task<Models.Candidate> UpdateCandidateAsync(Models.Candidate existingCandidate, Models.Candidate candidate)
	{
		existingCandidate.Email = candidate.Email;
		existingCandidate.FirstName = candidate.FirstName;
		existingCandidate.LastName = candidate.LastName;
		existingCandidate.PhoneNumber = candidate.PhoneNumber;
		existingCandidate.TimeInterval = candidate.TimeInterval;
		existingCandidate.LinkedInUrl = candidate.LinkedInUrl;
		existingCandidate.GitHubUrl = candidate.GitHubUrl;
		existingCandidate.Comment = candidate.Comment;

		await _context.SaveChangesAsync();
		return candidate;
	}

	private static SaveCandidateResponse GenerateResponse(Models.Candidate candidate, bool isAdding)
	{
		CandidateDTO candidateDTO = candidate.Adapt<CandidateDTO>();
		return new(candidateDTO)
		{
			IsSuccess = true,
			Message = isAdding ? "The candidate added successfully" : "The candidate updated successfully"
		};
	}

	#endregion
}
