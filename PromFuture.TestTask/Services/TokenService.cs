using Microsoft.Extensions.Caching.Distributed;
using PromFuture.TestTask.Models;
using PromFuture.TestTask.Services.Bases;

namespace PromFuture.TestTask.Services
{
    public class TokenService : BaseService<Token>
    {
        public TokenService(IHttpClientFactory clientFactory, IDistributedCache cache, ITokenStorageService tokenStorageService) : base(clientFactory, cache, tokenStorageService)
        {
        }
    }
}
