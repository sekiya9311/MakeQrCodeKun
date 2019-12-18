using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace MakeQrCodeKun.Models.Interfaces
{
    public interface IBarcodeModel : INotifyPropertyChanged
    {
        BitmapSource? Barcode { get; }

        void Create(string value, BarcodeCreatorOption option);

        void Download();
    }
}
