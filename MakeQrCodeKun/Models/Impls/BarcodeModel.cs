using MakeQrCodeKun.Models.Interfaces;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace MakeQrCodeKun.Models.Impls
{
    public class BarcodeModel : IBarcodeModel
    {
        private BitmapSource? _barcode;
        public BitmapSource? Barcode
        {
            get => _barcode;
            set
            {
                _barcode = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Barcode)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IBarcodeCreator _barcodeCreator;
        private readonly IFilePathInquirer _filePathInquirer;
        private readonly IImageSourceDownloader _imageSourceDownloader;

        public BarcodeModel(
            IBarcodeCreator barcodeCreator,
            IFilePathInquirer filePathInquirer,
            IImageSourceDownloader imageSourceDownloader)
        {
            _barcodeCreator = barcodeCreator;
            _filePathInquirer = filePathInquirer;
            _imageSourceDownloader = imageSourceDownloader;
        }

        public void Create(string value, BarcodeCreatorOption option)
        {
            Barcode = _barcodeCreator.Create(value, option);
        }

        public void Download()
        {
            var filePath = _filePathInquirer.Inquery();

            _imageSourceDownloader.Download(Barcode, filePath);
        }
    }
}
