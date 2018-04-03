namespace ScannerPrototype.Common.Tables
{
    public class TableScanRecord : BaseTable
    {
        private string _barcode { get; set; }

        public string Barcode
        {
            get { return _barcode; }
            set {
                _barcode = value;
                OnPropertyChanged(nameof(Barcode));
            }
        }
    }
}
