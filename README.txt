-----------------------------------READ ME--------------------------------------
--------------------------------------------------------------------------------
here Some point to about the project and the freamework 

PROJECT TITLE:
	- Job Candidate API
		- A REST API for managing job candidate information, designed to create 
		or update candidate profiles based on their email as a unique identifier.

PROJECT FEATURES:
	- Add or Update Candidate Information.

PROJECT TECH STACK:
	- Framework: .NET 8 (ASP.NET Core Web API)
	- Database: Entity Framework Core / SQL
	- Testing: xUnit for unit tests
	- Caching: Normally i'm using InMemory (not implemented)
		* Suggestion: In future if there a get API to fetch CandidateByEmail, 
		We can use caching by store the response of the add API to in-memory cache
		and check in memory if the candidate exists or not.

API ENDPOINTS:
	- replace swagger after the localhost to open swagger ui
	- SaveCandidate API --> {{BaseAddress}}/api/Candidate/SaveCandidate

Time Spent: 
	- Development: 2:30 hours
	- Documentation and testing: 2 hours
	NOTE: I did not use unit testing before, So i got some help from Google and ChatGPT.