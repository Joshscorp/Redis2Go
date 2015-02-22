# Redis2Go
# Quickly spins up Redis in .NET, for integration tests or local debugging.

Inspired by https://github.com/JohannesHoppe/Mongo2Go but for redis instead, and a lot simpler

**Installation**
The Redis2Go Nuget package can be found at https://nuget.org/packages/Redis2Go/

**Sample usage**

```
public class RedisFixture : IDisposable
{
    private readonly RedisRunner _runner;

    public RedisFixture()
    {
        this._runner = RedisRunner.StartForDebugging();
        // this._runner = RedisRunner.Start();
    }

    public void Dispose()
    {
        this._runner.Dispose();
    }
}
```    
**Default ports and process name**
```
public static class RedisDefaults
{
    public const string ProcessName = "redis-server";

    public const string RedisExecutable = "redis-server";

    public const int DefaultPort = 6379;

    public const int AlternateDefaultPort = 6380;
}
```
