using System.ComponentModel.DataAnnotations;

namespace Sigma.Candidate.Contracts.DTOs;

public record CandidateDTO
{
	[Required]
	[EmailAddress]
	public string Email { get; set; }

	[Required]
	[MaxLength(150)]
	public string FirstName { get; set; }

	[Required]
	[MaxLength(150)]
	public string LastName { get; set; }

	public string PhoneNumber { get; set; }

	public string TimeInterval { get; set; }

	[Url]
	public string LinkedInUrl { get; set; }

	[Url]
	public string GitHubUrl { get; set; }

	[Required]
	[MaxLength(1000)]
	public string Comment { get; set; }
}
