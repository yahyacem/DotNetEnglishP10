namespace Mediscreen.AssessmentAPI.Services
{
    public interface ITriggerTermsService
    {
        /// <summary>
        /// Create a trigger term and insert it to the database.
        /// </summary>
        /// <param name="term">Term to create.</param>
        public Task CreateAsync(string term);
    }
}
