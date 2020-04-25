using System;

namespace MongoDbDAL
{
    public class QuickAppDatabaseSettings : IQuickAppDatabaseSettings
    {
        public string QuickAppCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IQuickAppDatabaseSettings
    {
        string QuickAppCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
