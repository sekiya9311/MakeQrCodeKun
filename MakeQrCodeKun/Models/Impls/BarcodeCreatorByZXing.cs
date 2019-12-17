using MakeQrCodeKun.Models.Interfaces;
using System;
using System.Windows.Media.Imaging;
using ZXing.Presentation;

namespace MakeQrCodeKun.Models.Impls
{
    public class BarcodeCreatorByZXing : IBarcodeCreator
    {
        public BitmapSource Create(string value, BarcodeCreatorOption option)
        {
            var formatForZXing = ConvertBarcodeFormatType(option.Format);
            var writer = new BarcodeWriter
            {
                Format = formatForZXing,
                Options = new ZXing.Common.EncodingOptions
                {
                    Height = option.Height,
                    Width = option.Width,
                    Margin = option.Margin
                }
            };

            var image = writer.Write(value);
            return image;
        }

        private static ZXing.BarcodeFormat ConvertBarcodeFormatType(BarcodeFormat format)
            => format switch
            {
                BarcodeFormat.CODE_39 => ZXing.BarcodeFormat.CODE_39,
                BarcodeFormat.CODE_93 => ZXing.BarcodeFormat.CODE_93,
                BarcodeFormat.CODE_128 => ZXing.BarcodeFormat.CODE_128,
                BarcodeFormat.QR_CODE => ZXing.BarcodeFormat.QR_CODE,
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}
