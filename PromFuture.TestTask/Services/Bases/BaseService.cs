using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PromFuture.TestTask.Models;

namespace PromFuture.TestTask.Services.Bases
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity>
        where TEntity : class, new()
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient? _httpClient;

        /// <summary>
        /// The cache
        /// </summary>
        private readonly IDistributedCache _cache;

        private readonly ITokenStorageService _tokenStorageService;

        /// <summary>
        /// Gets the base path.
        /// </summary>
        /// <value>The base path.</value>
        protected string BasePath = typeof(TEntity).Name.ToLower();

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{TEntity}"/> class.
        /// </summary>
        /// <param name="clientFactory">The client factory.</param>
        /// <param name="cache">The cache.</param>
        protected BaseService(IHttpClientFactory clientFactory, IDistributedCache cache, ITokenStorageService tokenStorageService)
        {
            _cache = cache;
            _httpClient = clientFactory.CreateClient("API");
            _tokenStorageService = tokenStorageService;
        }

        /// <summary>
        /// Get all as an asynchronous operation.
        /// </summary>
        /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
        public virtual async Task<IEnumerable<TEntity>?> GetAllAsync()
        {
            var result = await _httpClient?.GetFromJsonAsync<IEnumerable<TEntity>>($"api/{BasePath}")!;
            return result;
        }

        /// <summary>
        /// Get all by filter as an asynchronous operation.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
        public virtual async Task<IEnumerable<TEntity>?> GetAllByFilterAsync(string query)
        {
            var responseMessage = await _httpClient?.PostAsJsonAsync($"api/{BasePath}/filter/{query}", query)!;
            var result = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(await responseMessage.Content.ReadAsStringAsync());
            return result;
        }

        /// <summary>
        /// Get identifier as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A Task&lt;TEntity&gt; representing the asynchronous operation.</returns>
        public virtual async Task<TEntity?> GetIdAsync(int id)
        {
            var responseMessage = await _httpClient!.PostAsJsonAsync($"api/{BasePath}/{id}", id);
            var result = JsonConvert.DeserializeObject<TEntity>(await responseMessage.Content.ReadAsStringAsync());
            return result;
        }

        public virtual async Task GetToken(string login, string password)
        {
            var responseMessage = await _httpClient!.GetAsync($"{BasePath}?login={login}&password={password}");
            var result = JsonConvert.DeserializeObject<Token>(await responseMessage.Content.ReadAsStringAsync());

            _tokenStorageService.tokenValue = result.token;
        }


        public virtual async Task<IEnumerable<TEntity>> GetByQuery(string suffix = "")
        {
            if (suffix != string.Empty)
                BasePath += $"{suffix}";

            if(BasePath.StartsWith("terminalresponse"))
            {
                BasePath = BasePath.Replace("terminalresponse", "terminals");
            }

            var responseMessage = await _httpClient!.GetAsync($"{BasePath}?token={_tokenStorageService.tokenValue}");
            var result = JsonConvert.DeserializeObject<ResponseModel<TEntity>>(await responseMessage.Content.ReadAsStringAsync());

            BasePath = BasePath.Replace(suffix, string.Empty);
            return result.items;
        }

        /// <summary>
        /// Delete as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A Task&lt;HttpResponseMessage&gt; representing the asynchronous operation.</returns>
        public virtual async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            var result = await _httpClient?.DeleteAsync($"api/{BasePath}/{id}")!;
            return result;
        }

        /// <summary>
        /// Create as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A Task&lt;HttpResponseMessage&gt; representing the asynchronous operation.</returns>
        public virtual async Task<TEntity> PostAsync(TEntity entity, int id, string suffix = "")
        {
            if (suffix != string.Empty)
                BasePath += $"/{id}/{suffix}";

            if (BasePath.StartsWith("terminalrequest"))
            {
                BasePath = BasePath.Replace("terminalrequest", "terminals");
            }

            var responseMessage = await _httpClient?.PostAsJsonAsync($"{BasePath}?token={_tokenStorageService.tokenValue}", entity)!;
            var result = JsonConvert.DeserializeObject<ResponseSingleModel<TEntity>>(await responseMessage.Content.ReadAsStringAsync());

            BasePath = BasePath.Replace(suffix, string.Empty);

            if (result.item == null)
                return null;

            return result.item;
        }

        /// <summary>
        /// Update as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A Task&lt;HttpResponseMessage&gt; representing the asynchronous operation.</returns>
        public virtual async Task<HttpResponseMessage> UpdateAsync(TEntity entity)
        {
            var result = await _httpClient?.PostAsJsonAsync($"api/{BasePath}/Update", entity)!;
            return result;
        }
    }
}
