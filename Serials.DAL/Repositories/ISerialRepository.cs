using Serials.DAL.Models;

namespace Serials.DAL.Repositories
{
    public interface ISerialRepository
    {
        void Save(Serial serial);
        Serial Get(string title);
    }
}