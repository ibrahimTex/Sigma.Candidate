using Microsoft.EntityFrameworkCore;

namespace Sigma.Candidate.DataAccess;

public class CandidateContext(DbContextOptions<CandidateContext> options) : DbContext(options)
{
	public virtual DbSet<Models.Candidate> Candidates { get; set; }
}