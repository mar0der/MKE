using MKE.Data;

namespace MKE.Models.Messages
{
    public class DatabaseCreatedMessage
    {
        public FEMDatabase NewDatabase { get; }

        public DatabaseCreatedMessage(FEMDatabase newDatabase)
        {
            NewDatabase = newDatabase;
        }
    }
}
