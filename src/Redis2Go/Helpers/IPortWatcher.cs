
namespace Redis2Go.Helpers
{
    public interface IPortWatcher
    {
        int FindOpenPort(int startPort);
        bool IsPortAvailable(int portNumber);
    }
}
