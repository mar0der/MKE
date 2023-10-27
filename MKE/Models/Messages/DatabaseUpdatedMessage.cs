using MKE.Data;

namespace MKE.Models.Messages
{
    public class DatabaseUpdatedMessage
    {
        public Database Database { get; }

        public DatabaseUpdatedMessage(Database database)
        {
            Database = database;
        }
    }
}
