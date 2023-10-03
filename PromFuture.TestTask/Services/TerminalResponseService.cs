using Microsoft.Extensions.Caching.Distributed;
using PromFuture.TestTask.Models;
using PromFuture.TestTask.Services.Bases;

namespace PromFuture.TestTask.Services
{
    public class TerminalResponseService : BaseService<TerminalResponse>
    {
        public TerminalResponseService(IHttpClientFactory clientFactory, IDistributedCache cache, ITokenStorageService tokenStorageService) : base(clientFactory, cache, tokenStorageService)
        {
        }
    }
}
