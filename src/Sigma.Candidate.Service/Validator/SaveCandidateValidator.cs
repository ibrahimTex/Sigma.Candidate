using FluentValidation;
using Sigma.Candidate.Contracts.Services.SaveCandidate;

namespace Sigma.Candidate.Service.Validator;

public class SaveCandidateValidator : AbstractValidator<SaveCandidateRequest>
{
	public SaveCandidateValidator()
	{
		RuleFor(x => x.Candidate)
			.NotEmpty().NotNull()
			.WithMessage("The candidate is empty.");

		RuleFor(x => x.Candidate.Email)
			.NotEmpty().WithMessage("The candidate email is required.")
			.EmailAddress().WithMessage("The candidate email is not valid.");

		RuleFor(x => x.Candidate.FirstName).NotEmpty()
			.WithMessage("The candidate first name is required.");

		RuleFor(x => x.Candidate.LastName)
			.NotEmpty().WithMessage("The candidate last name is required.");

		RuleFor(x => x.Candidate.Comment)
			.NotEmpty().WithMessage("The comment is required.");

		RuleFor(x => x.Candidate.PhoneNumber)
			.Matches(@"^\+?[1-9]\d{1,14}$")
			.When(x => !string.IsNullOrEmpty(x.Candidate.PhoneNumber))
			.WithMessage("The candidate phone number is not valid.");

		RuleFor(x => x.Candidate.LinkedInUrl)
			.Must(BeAValidUrl)
			.When(c => !string.IsNullOrEmpty(c.Candidate.LinkedInUrl))
			.WithMessage("The candidate LinkedIn url is not valid.");

		RuleFor(x => x.Candidate.GitHubUrl)
			.Must(BeAValidUrl)
			.When(c => !string.IsNullOrEmpty(c.Candidate.GitHubUrl))
			.WithMessage("The candidate GitHub url is not valid.");
	}

	private bool BeAValidUrl(string url)
	{
		return Uri.TryCreate(url, UriKind.Absolute, out var result) &&
			   (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
	}
}
