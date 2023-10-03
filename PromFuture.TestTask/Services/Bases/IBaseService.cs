namespace PromFuture.TestTask.Services.Bases
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>Task&lt;System.Nullable&lt;IEnumerable&lt;TEntity&gt;&gt;&gt;.</returns>
        Task<IEnumerable<TEntity>?> GetAllAsync();

        /// <summary>
        /// Gets all by filter asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>Task&lt;System.Nullable&lt;IEnumerable&lt;TEntity&gt;&gt;&gt;.</returns>
        Task<IEnumerable<TEntity>?> GetAllByFilterAsync(string query);

        /// <summary>
        /// Gets the identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;System.Nullable&lt;TEntity&gt;&gt;.</returns>
        Task<TEntity?> GetIdAsync(int id);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> DeleteAsync(int id);

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<TEntity> PostAsync(TEntity entity, int id, string suffix = "");

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> UpdateAsync(TEntity entity);

        Task GetToken(string login, string password);

        Task<IEnumerable<TEntity>> GetByQuery(string suffix = "");
    }
}
