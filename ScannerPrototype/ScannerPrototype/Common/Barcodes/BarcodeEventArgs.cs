using System;

namespace ScannerPrototype.Common.Barcodes
{
    public class BarcodeEventArgs : EventArgs
    {
        public string Barcode { get; private set; }

        public BarcodeEventArgs(string barcode)
        {
            Barcode = barcode;
        }
    }
}
