using System.ComponentModel.DataAnnotations;

namespace Sigma.Candidate.DataAccess.Models;

public class Candidate
{
	[Required]
	public string Email { get; set; }
	
	[Required]
	[MaxLength(150)]
	public string FirstName { get; set; }

	[Required]
	[MaxLength(150)]
	public string LastName { get; set; }

	public string PhoneNumber { get; set; }

	public string TimeInterval { get; set; }

	public string LinkedInUrl { get; set; }

	public string GitHubUrl { get; set; }

	[Required]
	[MaxLength(1000)]
	public string Comment { get; set; }
}