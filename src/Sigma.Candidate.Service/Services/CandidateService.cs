using Sigma.Candidate.Contracts.Exceptions;
using Sigma.Candidate.Contracts.Repositories;
using Sigma.Candidate.Contracts.Services;
using Sigma.Candidate.Contracts.Services.SaveCandidate;
using Sigma.Candidate.Service.Validator;

namespace Sigma.Candidate.Service.Services;

public class CandidateService(ICandidateRepository candidateRepository) : ICandidateService
{
	private readonly ICandidateRepository _candidateRepository = candidateRepository;

	public async Task<SaveCandidateResponse> SaveCandidateAsync(SaveCandidateRequest request)
	{
		SaveCandidateValidator validationRules = new();
		var validationResult = validationRules.Validate(request);

		if (!validationResult.IsValid)
		{
			List<string> errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
			throw new ServiceException("The request is invalid, please check the errors.", errors);
		}

		var response = await _candidateRepository.SaveCandidateAsync(request.Candidate);
		return response;
	}
}