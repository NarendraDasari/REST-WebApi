
using Api.DomainObjects;
using LiteDB;

namespace Api.Services
{
    public abstract class ServiceBase
    {
        // These values to be made configurable
        public const string dbFilePath = @"C:\Api.Database\";
        public const string dbName = "LiteDB_Demos.db";
        private readonly string connectionString = default(string);

        internal ServiceBase()
        {
            connectionString = string.Format("{0}{1}",dbFilePath,dbName);
 
        }

        internal LiteDatabase GetDatabaseInstance()
        {
            ObjectPool<LiteDatabase> pool = new ObjectPool<LiteDatabase>(() => new LiteDatabase(connectionString));
            LiteDatabase dbContext =  pool.GetObject();
            pool.PutObject(dbContext);

            return dbContext;
        }

        internal LiteCollection<Contact> GetContactsDbContext()
        {
            using (var db = GetDatabaseInstance())
            {
                var contactDbContext = db.GetCollection<Contact>("Contacts");
                return contactDbContext;

            }
            
        }
    }
}
