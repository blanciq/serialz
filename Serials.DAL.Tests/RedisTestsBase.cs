using NUnit.Framework;
using ServiceStack.Redis;

namespace Serials.DAL.Tests
{
    public class RedisTestsBase
    {
        protected RedisClient client;

        [SetUp]
        public void Before()
        {
            client = new RedisClient("localhost");
            client.FlushAll();
        }

        [TearDown]
        public void After()
        {
            client.Dispose();
        }
    }
}