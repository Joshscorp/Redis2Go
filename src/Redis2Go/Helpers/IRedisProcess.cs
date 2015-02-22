using System;
using System.Collections.Generic;

namespace Redis2Go.Helpers
{
    public interface IRedisProcess : IDisposable
    {
        IEnumerable<string> StandardOutput { get; }
        IEnumerable<string> ErrorOutput { get; }
    }
}
