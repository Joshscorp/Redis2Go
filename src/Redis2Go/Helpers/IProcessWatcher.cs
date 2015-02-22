
namespace Redis2Go.Helpers
{
    public interface IProcessWatcher
    {
        bool IsProcessRunning(string processName);
    }
}
