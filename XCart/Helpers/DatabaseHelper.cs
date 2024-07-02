using SQLite;
using System.IO;
using System.Threading.Tasks;
using XCart.Models.Product;
using XCart.Models.User;

namespace XCart.Helpers
{
    public class DatabaseHelper
    {
        private const string DatabaseFilename = "xcart.db";
        private readonly string DatabasePath;

        private readonly SQLiteAsyncConnection _databaseConnection;

        public DatabaseHelper()
        {
            DatabasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
            _databaseConnection = new SQLiteAsyncConnection(DatabasePath);
            InitializeTables();
        }

        private void InitializeTables()
        {
            _databaseConnection.CreateTableAsync<Session>().Wait();
            _databaseConnection.CreateTableAsync<UserModel>().Wait();
            _databaseConnection.CreateTableAsync<ProductModel>().Wait();
        }

        public SQLiteAsyncConnection GetDatabaseConnection()
        {
            return _databaseConnection;
        }

        public async Task<Session> GetSessionAsync()
        {
            return await _databaseConnection.Table<Session>().FirstOrDefaultAsync();
        }

        public async Task SaveSessionAsync(Session session)
        {
            await _databaseConnection.InsertOrReplaceAsync(session);
        }

        public async Task DeleteSessionAsync()
        {
            await _databaseConnection.DeleteAllAsync<Session>();
        }
    }
}
