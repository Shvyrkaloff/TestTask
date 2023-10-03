namespace PromFuture.TestTask.Services
{
    public class TokenStorageService : ITokenStorageService
    {
        public string tokenValue { get; set; }
    }

    public interface ITokenStorageService
    {
        string tokenValue { get; set; }
    }
}
