using System.Threading.Tasks;
using ScannerPrototype.Common.Interfaces;
using ScannerPrototype.iOS.Implementation;
using Xamarin.Forms;
using ZXing.Mobile;

[assembly: Dependency(typeof(Scanner))]
namespace ScannerPrototype.iOS.Implementation
{
    public class Scanner : IScanner
    {
        private MobileBarcodeScanner _scanner;

        public Scanner()
        {
            _scanner = new MobileBarcodeScanner();
        }

        public async Task<string> Scan()
        {
            var result = await _scanner.Scan();

            return result?.Text;
        }

        public void AutoFocus()
        {
            _scanner.AutoFocus();
        }
    }
}