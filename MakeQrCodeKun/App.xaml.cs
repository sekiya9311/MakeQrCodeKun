using Prism.Ioc;
using MakeQrCodeKun.Views;
using System.Windows;
using MakeQrCodeKun.Models.Interfaces;
using MakeQrCodeKun.Models.Impls;

namespace MakeQrCodeKun
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IBarcodeCreator, BarcodeCreatorByZXing>();
            containerRegistry.RegisterSingleton<IFilePathInquirer, FilePathInquirer>();
            containerRegistry.RegisterSingleton<IImageSourceDownloader, ImageSourceDownloader>();
        }
    }
}
