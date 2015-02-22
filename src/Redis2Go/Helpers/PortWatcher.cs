using Redis2Go.Exceptions;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace Redis2Go.Helpers
{
    public class PortWatcher : IPortWatcher
    {
        public int FindOpenPort(int startPort)
        {
            int port = startPort;
            do
            {
                if (IsPortAvailable(port))
                {
                    break;
                }

                if (port == RedisDefaults.AlternateDefaultPort + 100)
                {
                    throw new NoFreePortFoundException();
                }

                ++port;

            } while (true);

            return port;
        }

        public bool IsPortAvailable(int portNumber)
        {
            IPEndPoint[] tcpConnInfoArray = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners();
            return tcpConnInfoArray.All(endpoint => endpoint.Port != portNumber);
        }
    }
}
