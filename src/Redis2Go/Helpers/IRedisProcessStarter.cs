
namespace Redis2Go.Helpers
{
    public interface IRedisProcessStarter
    {
        IRedisProcess Start(string binariesDirectory, int port);
    }
}
