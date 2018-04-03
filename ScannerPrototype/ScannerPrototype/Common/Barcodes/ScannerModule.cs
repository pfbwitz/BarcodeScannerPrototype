using ScannerPrototype.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace ScannerPrototype.Common.Barcodes
{
    public class ScannerModule : IDisposable
    {
        private IScanner _scanner;

        public event EventHandler<BarcodeEventArgs> BarcodeScanned;

        public ScannerModule()
        {
            _scanner = InstanceManager.Scanner;
        }

        private void OnBarcodeScanned(BarcodeEventArgs args)
        {
            BarcodeScanned?.Invoke(this, args);
        }

        public async Task Scan()
        {
            _scanner = InstanceManager.Scanner;
            _scanner.AutoFocus();
            var result = await _scanner.Scan();

            OnBarcodeScanned(new BarcodeEventArgs(result));
        }

        public void Dispose()
        {
            _scanner = null;
        }
    }
}
