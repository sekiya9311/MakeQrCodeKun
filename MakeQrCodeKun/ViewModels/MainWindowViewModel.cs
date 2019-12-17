using MakeQrCodeKun.Models.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Media.Imaging;

namespace MakeQrCodeKun.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public string Title => "QR コード作るくん";

        private string _plainValue = "";
        public string PlainValue
        {
            get => _plainValue;
            set { SetProperty(ref _plainValue, value); }
        }

        private BitmapSource? _barcodeImage;
        public BitmapSource? BarcodeImage
        {
            get => _barcodeImage;
            set { SetProperty(ref _barcodeImage, value); }
        }

        private DelegateCommand? _makeQrCodeCommand;
        public DelegateCommand MakeQrCodeCommand
            => _makeQrCodeCommand ??= new DelegateCommand(MakeQrCode);

        private DelegateCommand? _downloadQrCodeCommand;
        public DelegateCommand DownloadQrCodeCommand
            => _downloadQrCodeCommand ??= new DelegateCommand(DownloadQrCode);

        private readonly IBarcodeCreator _barcodeCreator;
        private readonly IInquirerFilePath _inquirerFilePath;
        private readonly IImageSourceDownloader _imageSourceDownloader;

        public MainWindowViewModel(
            IBarcodeCreator barcodeCreator,
            IInquirerFilePath inquirerFilePath,
            IImageSourceDownloader imageSourceDownloader)
        {
            _barcodeCreator = barcodeCreator;
            _inquirerFilePath = inquirerFilePath;
            _imageSourceDownloader = imageSourceDownloader;
        }

        private void MakeQrCode()
        {
            BarcodeImage = _barcodeCreator.Create(
                PlainValue,
                new BarcodeCreatorOption
                {
                    Format = BarcodeFormat.QR_CODE,
                    Height = 240,
                    Width = 240,
                    Margin = 5
                });
        }

        private void DownloadQrCode()
        {
            var saveFilePath = _inquirerFilePath.Inquery();

            _imageSourceDownloader.Download(BarcodeImage, saveFilePath);
        }
    }
}
