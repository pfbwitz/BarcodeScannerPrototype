using ScannerPrototype.Common.Interfaces;
using Xamarin.Forms;

namespace ScannerPrototype.Common
{
    public static class InstanceManager
    {
        public static IDatabaseFactory DatabaseFactory => DependencyService.Get<IDatabaseFactory>();

        public static IScanner Scanner => DependencyService.Get<IScanner>();
    }
}
