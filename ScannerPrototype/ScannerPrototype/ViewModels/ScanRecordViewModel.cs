using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using ScannerPrototype.Views;
using ScannerPrototype.Common.Tables;
using ScannerPrototype.Queries;

namespace ScannerPrototype.ViewModels
{
    public class ScanRecordViewModel : BaseViewModel
    {
        public ObservableCollection<TableScanRecord> Datasource { get; set; }
        public Command LoadItemsCommand { get; set; }

        private QueryHelperTableScanRecord _queryhelper;

        public ScanRecordViewModel()
        {
            _queryhelper = new QueryHelperTableScanRecord();
           
            Title = "Barcodes";
            Datasource = new ObservableCollection<TableScanRecord>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<ItemsPage, TableScanRecord>(this, "AddBarcode", async (obj, item) =>
            {
                var entity = item as TableScanRecord;
                await _queryhelper.SaveRecordAsync(entity);
                Datasource.Add(entity);
            });

            MessagingCenter.Subscribe<ItemsPage, TableScanRecord>(this, "UpdateBarcode", async (obj, item) =>
            {
                var entity = item as TableScanRecord;
                await _queryhelper.SaveRecordAsync(entity);
            });

            MessagingCenter.Subscribe<ItemsPage, TableScanRecord>(this, "DeleteBarcode", async (obj, item) =>
            {
                var entity = item as TableScanRecord;
                await _queryhelper.DeleteRecordAsync(entity);
                Datasource.Remove(entity);
            });
        }

        public async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await _queryhelper.Prepare<TableScanRecord>();

                Datasource.Clear();
               
                var entities = await _queryhelper.GetAllAsync();
                foreach (var entity in entities)
                    Datasource.Add(entity);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}