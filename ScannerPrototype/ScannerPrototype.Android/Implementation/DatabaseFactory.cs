using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ScannerPrototype.Common;
using ScannerPrototype.Common.Interfaces;
using ScannerPrototype.Common.Tables;
using ScannerPrototype.Droid.Implementation;
using Xamarin.Forms;
using System.Linq;
using SQLite;
using System;

[assembly: Dependency(typeof(DatabaseFactory))]
namespace ScannerPrototype.Droid.Implementation
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private SQLiteAsyncConnection _connectionAsync;

        private SQLiteConnection _connection;

        public SQLiteAsyncConnection ConnectionAsync => _connectionAsync ?? (_connectionAsync = GetAsyncConnection());

        public SQLiteConnection Connection => _connection ?? (_connection = GetConnection());

        private SQLiteAsyncConnection GetAsyncConnection()
        {
            var path = GetDatabaseLocalPath(Constants.DatabaseFileName);

            var connection = new SQLiteAsyncConnection(path);

            return connection;
        }

        private SQLiteConnection GetConnection()
        {
            var path = GetDatabaseLocalPath(Constants.DatabaseFileName);

            var connection = new SQLiteConnection(path);

            return connection;
        }

        public void CreateTable<T>() where T : ITable
        {
            Connection.CreateTable<T>();
        }

        public async Task CreateTableAsync<T>() where T : ITable, new()
        {
            await ConnectionAsync.CreateTableAsync<T>();
        }

        public async Task SaveRecordAsync<T>(T record) where T : ITable, new()
        {
            record.MutationDateTime = DateTime.Now;
            if (record.Id == 0)
            {
                var lastRecord = (await GetTableAsync<T>()).OrderByDescending(t => t.Id).FirstOrDefault();
                record.Id = lastRecord != null ? lastRecord.Id + 1 : 1;
                record.CreationDateTime = DateTime.Now;
                await ConnectionAsync.InsertAsync(record);
            }
            else
                await ConnectionAsync.UpdateAsync(record);
        }

        public async Task DeleteRecordAsync(ITable record)
        {
            await ConnectionAsync.DeleteAsync(record);
        }

        public List<T> GetTable<T>() where T : ITable, new()
        {
            var table = Connection.Table<T>();

            return table.ToList();
        }

        public async Task<List<T>> GetTableAsync<T>() where T : ITable, new()
        {
            var table = ConnectionAsync.Table<T>();

            return await table.ToListAsync();
        }

        private string GetDatabaseLocalPath(string filename)
        {
            var docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
                Directory.CreateDirectory(libFolder);

            return Path.Combine(libFolder, filename);
        }
    }
}