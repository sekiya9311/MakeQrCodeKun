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

        public BitmapSource? Barcode => _model.Barcode;

        private DelegateCommand? _makeQrCodeCommand;
        public DelegateCommand MakeQrCodeCommand
            => _makeQrCodeCommand ??= new DelegateCommand(MakeQrCode);

        private DelegateCommand? _downloadQrCodeCommand;
        public DelegateCommand DownloadQrCodeCommand
            => _downloadQrCodeCommand ??= new DelegateCommand(DownloadQrCode);

        private readonly IBarcodeModel _model;

        public MainWindowViewModel(IBarcodeModel model)
        {
            _model = model;

            _model.PropertyChanged += (s, e) =>
            {
                OnPropertyChanged(e);
            };
        }

        private void MakeQrCode()
        {
            _model.Create(
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
            _model.Download();
        }
    }
}
