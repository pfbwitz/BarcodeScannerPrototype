using ScannerPrototype.Common.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ScannerPrototype.Queries
{
    public class QueryHelperTableScanRecord : BaseQueryHelper
    {
        public async Task SaveRecordAsync(TableScanRecord entity)
        {
            await SaveRecordAsync<TableScanRecord>(entity);
        }

        public async Task DeleteRecordAsync(TableScanRecord entity)
        {
            await DeleteRecordAsync<TableScanRecord>(entity);
        }

        public async Task<List<TableScanRecord>> GetAllAsync()
        {
            return await GetAllAsync<TableScanRecord>();
        }

        public async Task<TableScanRecord> GetOneById(int id)
        {
            return await GetOneById(id);
        }

        public async Task<TableScanRecord> GetOneByBarcode(string barcode)
        {
            return (await GetAllAsync()).FirstOrDefault(t => t.Barcode == barcode);
        }
    }
}
