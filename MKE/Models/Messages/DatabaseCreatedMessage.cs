using MKE.Data;

namespace MKE.Models.Messages
{
    public class DatabaseCreatedMessage
    {
        public Database NewDatabase { get; }

        public DatabaseCreatedMessage(Database newDatabase)
        {
            NewDatabase = newDatabase;
        }
    }
}
