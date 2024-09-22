----------------------------IMPROVEMENTS-----------------------------
---------------------------------------------------------------------

**** Code Improvements
** Add middleware to log the incoming request and the reponse.
** Add more logging for easier troubleshooting.
** Implement in-memory caching (using IMemoryCache) for candidate
   lookups to reduce database calls.

**** Database Improvements
** Indexing the Email field as it's the primary identifier key
** Add CreateAt/CreateBy/UpdatedAt/UpdateBy to easy trace the modifications.

**** Deployment Improvements
** Deploy the app to docker and setup the CI/CD piplines 