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

        private DelegateCommand? _createBarcodeCommand;
        public DelegateCommand CreateBarcodeCommand
            => _createBarcodeCommand ??= new DelegateCommand(CreateBarcode);

        private DelegateCommand? _downloadBarodeCommand;
        public DelegateCommand DownloadBarodeCommand
            => _downloadBarodeCommand ??= new DelegateCommand(DownloadBarcode);

        private readonly IBarcodeModel _model;

        public MainWindowViewModel(IBarcodeModel model)
        {
            _model = model;

            _model.PropertyChanged += (s, e) =>
            {
                OnPropertyChanged(e);
            };
        }

        private void CreateBarcode()
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

        private void DownloadBarcode()
        {
            _model.Download();
        }
    }
}
