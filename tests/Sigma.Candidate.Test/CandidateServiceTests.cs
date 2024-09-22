using Moq;
using Sigma.Candidate.Contracts.Exceptions;
using Sigma.Candidate.Contracts.Repositories;
using Sigma.Candidate.Contracts.Services.SaveCandidate;
using Sigma.Candidate.Service.Services;
using Sigma.Candidate.Service.Validator;

namespace Sigma.Candidate.Test;

public class CandidateServiceTests
{
	#region Constructor and private properites

	private readonly Mock<ICandidateRepository> _mockRepository;
	private readonly CandidateService _candidateService;

	public CandidateServiceTests()
	{
		_mockRepository = new Mock<ICandidateRepository>();
		_candidateService = new CandidateService(_mockRepository.Object);
	}

	#endregion

	#region Test scenarios

	[Fact]
	public async Task SaveCandidateAsync_ShouldCallRepository_WhenRequestIsValid()
	{
		// Arrange
		var dto = new Contracts.DTOs.CandidateDTO()
		{
			Email = "ighanem_36@hotmail.com",
			FirstName = "Ibrahim",
			LastName = "Ahmad",
			Comment = "Hi there, it's me unit test"
		};

		var request = new SaveCandidateRequest(dto);

		// Mock repository response
		var mockResponse = new SaveCandidateResponse(dto)
		{
			 IsSuccess = true
		};

		_mockRepository.Setup(repo => repo.SaveCandidateAsync(request.Candidate)).ReturnsAsync(mockResponse);

		// Act
		var result = await _candidateService.SaveCandidateAsync(request);

		// Assert
		Assert.NotNull(result);
		Assert.True(result.IsSuccess);
		_mockRepository.Verify(repo => repo.SaveCandidateAsync(request.Candidate), Times.Once);
	}

	[Fact]
	public async Task SaveCandidateAsync_ShouldThrowSerivceException_WhenRequestIsInValid()
	{
		var dto = new Contracts.DTOs.CandidateDTO()
		{
			Email = "",
			FirstName = "Ibrahim",
			LastName = "Ahmad",
			Comment = "Hi there, it's me unit test"
		};
		var request = new SaveCandidateRequest(dto);
		SaveCandidateValidator validator = new();
		var validationResult = validator.Validate(request);

		// Act & Assert
		var exception = await Assert.ThrowsAsync<ServiceException>(() => _candidateService.SaveCandidateAsync(request));

		// Assert error message and validation errors
		Assert.Equal("The request is invalid, please check the errors.", exception.Message);
		Assert.NotEmpty(exception.Errors);
	}

	#endregion
}