using System.Diagnostics;
using System.Linq;

namespace Redis2Go.Helpers
{
    public class ProcessWatcher : IProcessWatcher
    {
        public bool IsProcessRunning(string processName)
        {
            return Process.GetProcessesByName(processName).Any();
        }
    }
}
