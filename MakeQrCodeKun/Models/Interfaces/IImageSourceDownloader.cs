using System.Windows.Media.Imaging;

namespace MakeQrCodeKun.Models.Interfaces
{
    public interface IImageSourceDownloader
    {
        void Download(BitmapSource? image, string filePath);
    }
}
