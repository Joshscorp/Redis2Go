using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Redis2Go.Helpers
{
    public class RedisProcessStarter : IRedisProcessStarter
    {
        /// <summary>
        /// Starts a new process.
        /// </summary>
        public IRedisProcess Start(string binariesDirectory, int port = RedisDefaults.DefaultPort)
        {
            string fileName = string.Format(@"{0}\{1}", binariesDirectory, RedisDefaults.RedisExecutable);
            string arguments = string.Format(@"--port {0}", port);
            List<string> errorOutput = new List<string>();
            List<string> standardOutput = new List<string>();
            Process redisServerProcess = null;
            try
            {
                redisServerProcess = Process.Start(new ProcessStartInfo(fileName, arguments));
                standardOutput.Add("redis-server started on successfully");
            }
            catch (Exception ex)
            {
                errorOutput.Add(string.Format("Cound not start redis-server.  Error: {0}", ex.Message));
            }

            RedisProcess redisProcess = new RedisProcess(redisServerProcess)
            {
                ErrorOutput = errorOutput,
                StandardOutput = standardOutput
            };

            return redisProcess;
        }
    }
}
