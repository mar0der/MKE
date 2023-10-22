using MKE.Data;

namespace MKE.Models.Messages
{
    public class DatabaseUpdatedMessage
    {
        public FEMDatabase Database { get; }

        public DatabaseUpdatedMessage(FEMDatabase database)
        {
            Database = database;
        }
    }
}
