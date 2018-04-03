using ScannerPrototype.Common.Interfaces;
using ScannerPrototype.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ScannerPrototype.Common.Tables;

namespace ScannerPrototype.Queries
{
    public abstract class BaseQueryHelper
    {
        protected readonly IDatabaseFactory DatabaseFactory;

        protected BaseQueryHelper()
        {
            DatabaseFactory = InstanceManager.DatabaseFactory;
        }

        //TODO: move this for performance reasons
        public async Task Prepare<T>() where T : ITable, new()
        {
            await DatabaseFactory.CreateTableAsync<T>();
        }

        protected async Task SaveRecordAsync<T>(T entity) where T : ITable, new()
        {
            await DatabaseFactory.SaveRecordAsync(entity);
        }

        protected async Task DeleteRecordAsync<T>(T entity) where T : ITable, new()
        {
            await DatabaseFactory.DeleteRecordAsync(entity);
        }

        protected async Task<List<T>> GetAllAsync<T>() where T : ITable, new()
        {
            return await DatabaseFactory.GetTableAsync<T>();
        }

        protected async Task<T> GetOneById<T>(int id) where T : ITable, new()
        {
            var table = await GetAllAsync<T>();

            return table.FirstOrDefault(t => t.Id == id);
        }
    }
}
