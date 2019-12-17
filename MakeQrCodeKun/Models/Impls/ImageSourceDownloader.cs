using MakeQrCodeKun.Models.Interfaces;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace MakeQrCodeKun.Models.Impls
{
    public class ImageSourceDownloader : IImageSourceDownloader
    {
        public void Download(BitmapSource? image, string filePath)
        {
            if (image is null) return;

            BitmapEncoder encoder = Path.GetExtension(filePath) switch
            {
                ".jpg" => new JpegBitmapEncoder(),
                ".jpeg" => new JpegBitmapEncoder(),
                ".png" => new PngBitmapEncoder(),
                ".bmp" => new BmpBitmapEncoder(),
                _ => throw new ArgumentException()
            };
            encoder.Frames.Add(BitmapFrame.Create(image));

            using var stream = new FileStream(filePath, FileMode.Create);
            encoder.Save(stream);
        }
    }
}
