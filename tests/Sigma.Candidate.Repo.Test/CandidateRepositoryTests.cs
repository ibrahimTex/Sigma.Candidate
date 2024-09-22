using Microsoft.EntityFrameworkCore;
using Moq;
using Sigma.Candidate.Contracts.DTOs;
using Sigma.Candidate.DataAccess;
using Sigma.Candidate.DataAccess.Models;
using Sigma.Candidate.DataAccess.Repositories;

namespace Sigma.Candidate.Repo.Test
{
	public class CandidateRepositoryTests
	{
		private readonly CandidateRepository _repository;
		private readonly CandidateContext _context;

		public CandidateRepositoryTests()
		{
			// Set up an in-memory database for testing
			var options = new DbContextOptionsBuilder<CandidateContext>()
							.UseInMemoryDatabase("Candidate")
							.Options;

			_context = new CandidateContext(options);

			_repository = new CandidateRepository(_context);
		}

		[Fact]
		public async Task SaveCandidateAsync_ShouldAddCandidate_WhenNewCandidate()
		{
			// Arrange
			var candidateDTO = new CandidateDTO
			{
				Email = "ighanem36New@mail.com",
				FirstName = "ibrahim",
				LastName = "ahmad",
				Comment = "Hi im testing"
			};

			// Act
			var response = await _repository.SaveCandidateAsync(candidateDTO);

			// Assert
			Assert.True(response.IsSuccess);
			Assert.Equal("The candidate added successfully", response.Message);

			var candidateInDb = await _context.Candidates.FirstOrDefaultAsync(c => c.Email == candidateDTO.Email);
			Assert.NotNull(candidateInDb);
			Assert.Equal(candidateDTO.Email, candidateInDb.Email);
		}

		[Fact]
		public async Task SaveCandidateAsync_ShouldUpdateCandidate_WhenExistingCandidate()
		{
			// Arrange
			var existingCandidate = new DataAccess.Models.Candidate
			{
				Email = "ighanem36@mail.com",
				FirstName = "ibrahim",
				LastName = "ahmad",
				Comment = "Hi im testing"
			};
			_context.Candidates.Add(existingCandidate);
			await _context.SaveChangesAsync();

			var candidateDTO = new CandidateDTO
			{
				Email = "ighanem36@mail.com",
				FirstName = "ibrahim Updated",
				LastName = "ahmad",
				Comment = "Hi im testing"
			};

			// Act
			var response = await _repository.SaveCandidateAsync(candidateDTO);

			// Assert
			Assert.True(response.IsSuccess);
			Assert.Equal("The candidate updated successfully", response.Message);

			var candidateInDb = await _context.Candidates.FirstOrDefaultAsync(c => c.Email == candidateDTO.Email);
			Assert.NotNull(candidateInDb);
			Assert.Equal(candidateDTO.FirstName, candidateInDb.FirstName);
			Assert.Equal(candidateDTO.PhoneNumber, candidateInDb.PhoneNumber);
		}
	}
}