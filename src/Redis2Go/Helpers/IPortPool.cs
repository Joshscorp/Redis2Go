
namespace Redis2Go.Helpers
{
    public interface IPortPool
    {
        /// <summary>
        /// Returns and reserves a new port
        /// </summary>
        int GetNextOpenPort();
    }
}
