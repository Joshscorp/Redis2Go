using Redis2Go.Exceptions;
using Redis2Go.Helpers;
using System;

namespace Redis2Go
{
    public class RedisRunner : IDisposable
    {
        private const string BinariesSearchPattern = @"packages\Redis*\tools";
        private const string BinariesSearchPatternSolution = @"Redis*\tools";
        public const string WindowsNugetCacheLocation = @"%USERPROFILE%\.nuget\packages";

        private readonly IRedisProcess _redisProcess;
        private readonly int _port;

        public State State { get; private set; }
        public int Port { get { return this._port; } }
        public bool Disposed { get; private set; }

        public static RedisRunner Start()
        {
            return new RedisRunner(PortPool.GetInstance, new RedisProcessStarter());
        }

        public static RedisRunner StartForDebugging()
        {
            return new RedisRunner(new ProcessWatcher(), new PortWatcher(), new RedisProcessStarter());
        }

        private RedisRunner(IProcessWatcher processWatcher, IPortWatcher portWatcher, IRedisProcessStarter processStarter)
        {
            _port = RedisDefaults.DefaultPort;

            if (processWatcher.IsProcessRunning(RedisDefaults.ProcessName) && !portWatcher.IsPortAvailable(_port))
            {
                State = State.AlreadyRunning;
                return;
            }

            if (!portWatcher.IsPortAvailable(_port))
            {
                throw new PortTakenException(String.Format("Redis can't be started. The TCP port {0} is already taken.", this._port));
            }

            _redisProcess = processStarter.Start(BinariesDirectory, this._port);

            State = State.Running;
        }

        /// <summary>
        /// usage: integration tests
        /// </summary>
        private RedisRunner(IPortPool portPool, IRedisProcessStarter processStarter)
        {
            _port = portPool.GetNextOpenPort();

            _redisProcess = processStarter.Start(BinariesDirectory, this._port);

            State = State.Running;
        }

        private static string BinariesDirectory
        {
            get
            {
                // 1st: path when installed via nuget
                // 2st: path when installed via nuget using PackageReference
                string binariesFolder = FolderSearch.CurrentExecutingDirectory().FindFolderUpwards(BinariesSearchPattern) ??
                                        Environment.ExpandEnvironmentVariables(WindowsNugetCacheLocation).FindFolderUpwards(BinariesSearchPattern) ??
                                        FolderSearch.CurrentExecutingDirectory().FindFolderUpwards(BinariesSearchPatternSolution);

                if (binariesFolder == null)
                {
                    throw new RedisBinariesNotFoundException();
                }
                return binariesFolder;
            }
        }

        ~RedisRunner()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        public void Dispose(bool disposing)
        {
            if (this.Disposed)
                return;

            if (State != State.Running)
                return;

            if (disposing)
                GC.SuppressFinalize(this);

            if (this._redisProcess != null)
                this._redisProcess.Dispose();

            Disposed = true;
            State = State.Stopped;
        }
    }
}
