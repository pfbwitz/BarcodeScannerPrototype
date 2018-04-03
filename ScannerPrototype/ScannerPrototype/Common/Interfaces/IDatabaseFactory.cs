using ScannerPrototype.Common.Tables;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScannerPrototype.Common.Interfaces
{
    public interface IDatabaseFactory
    {
        void CreateTable<T>() where T : ITable;

        Task CreateTableAsync<T>() where T : ITable, new();

        Task SaveRecordAsync<T>(T record) where T : ITable, new();

        Task DeleteRecordAsync(ITable record);

        List<T> GetTable<T>() where T : ITable, new();

        Task<List<T>> GetTableAsync<T>() where T : ITable, new();

        SQLiteAsyncConnection ConnectionAsync { get; }

        SQLiteConnection Connection { get; }
    }
}
