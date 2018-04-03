using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ScannerPrototype.ViewModels;
using ScannerPrototype.Common.Barcodes;
using System.Threading.Tasks;
using ScannerPrototype.Common.Tables;
using System.Linq;
using ScannerPrototype.Common;

namespace ScannerPrototype.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsPage : ContentPage
	{
        private ScanRecordViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ScanRecordViewModel();
        }

        /// <summary>
        /// Select item from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as TableScanRecord;
            if (item == null)
                return;

            var choice = await DisplayActionSheet(item.Barcode, CommonResources.BtnCancel, null, CommonResources.LblUpdateModificationDate, CommonResources.BtnDelete);

           try
            {
                if (choice == CommonResources.LblUpdateModificationDate)
                    MessagingCenter.Send(this, "UpdateBarcode", item);
                else if (choice == CommonResources.BtnDelete)
                    MessagingCenter.Send(this, "DeleteBarcode", item);
            }
            catch(Exception ex)
            {
                await DisplayAlert(CommonResources.TtlError, ex.Message, CommonResources.BtnOk);
            }
            finally
            {
                ItemsListView.SelectedItem = null;
            }
        }

        /// <summary>
        /// Scan a new barcode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void AddBarcode_Clicked(object sender, EventArgs e)
        {
            await Scan();
        }

        /// <summary>
        /// Load the list when the screen appears
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!_viewModel.Datasource.Any())
                _viewModel.LoadItemsCommand.Execute(null);
        }

        /// <summary>
        /// Scan a barcode
        /// </summary>
        /// <returns></returns>
        private async Task Scan()
        {
            using (var scannerModule = new ScannerModule())
            {
                scannerModule.BarcodeScanned += async (s, a) => await ReportResult(a.Barcode);
                await scannerModule.Scan();
            }
        }

        /// <summary>
        /// Callback method for barcode scanner
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private async Task ReportResult(string barcode)
        {
           try
            {
                if (string.IsNullOrEmpty(barcode))
                    return;

                var choice = await DisplayActionSheet($"Barcode {barcode} scanned", CommonResources.BtnDiscard, null, CommonResources.BtnSave, CommonResources.BtnSaveScanAgain);

                if (choice == CommonResources.BtnSave || choice == CommonResources.BtnSaveScanAgain)
                {
                    MessagingCenter.Send(this, "AddBarcode", new TableScanRecord { Barcode = barcode });
                }
                if (choice == CommonResources.BtnSaveScanAgain)
                    await Scan();
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, CommonResources.BtnOk);
            }
        }
    }
}