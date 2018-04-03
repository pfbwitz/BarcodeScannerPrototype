using System.Threading.Tasks;

namespace ScannerPrototype.Common.Interfaces
{
    public interface IScanner
    {
        Task<string> Scan();

        void AutoFocus();
    }
}
