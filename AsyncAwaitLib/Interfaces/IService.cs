
namespace AsyncAwaitLib
{
    public interface IService
    {
        Task<string> executeAsync();
        string executeSync();
    }
}