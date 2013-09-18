using System;
using System.Linq;
using Serials.DAL.Models;
using ServiceStack.Redis;

namespace Serials.DAL.Repositories
{
    public class SerialRepository : ISerialRepository
    {
        private const string RedisServer = "localhost";
        public void Save(Serial serial)
        {
            using (var client = new RedisClient(RedisServer))
            using (var serials = client.As<Serial>())
            {
                if (
                    serials.GetAll()
                           .Any(x => string.Equals(x.Title, serial.Title, StringComparison.InvariantCultureIgnoreCase)))
                    return;
                serials.Store(serial);
            }
        }

        public Serial Get(string title)
        {
            using (var client = new RedisClient(RedisServer))
            using (var serials = client.As<Serial>())
            {
                var season = serials.GetAll()
                    .SingleOrDefault(x => string.Equals(x.Title, title, StringComparison.InvariantCultureIgnoreCase));
                

                return season;
            }
        }
    }
}