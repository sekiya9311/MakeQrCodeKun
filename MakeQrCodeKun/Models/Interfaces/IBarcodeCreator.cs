using System.Windows.Media.Imaging;

namespace MakeQrCodeKun.Models.Interfaces
{
    public interface IBarcodeCreator
    {
        BitmapSource Create(string value, BarcodeCreatorOption option);
    }

    public enum BarcodeFormat
    {
        CODE_39,
        CODE_93,
        CODE_128,
        QR_CODE
    }

    public struct BarcodeCreatorOption
    {
        public BarcodeFormat Format { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Margin { get; set; }
    }
}
