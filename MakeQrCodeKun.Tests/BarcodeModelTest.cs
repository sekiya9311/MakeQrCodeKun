using MakeQrCodeKun.Models.Impls;
using MakeQrCodeKun.Models.Interfaces;
using Moq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Xunit;

namespace MakeQrCodeKun.Tests
{
    public class BarcodeModelTest
    {
        private readonly Mock<IBarcodeCreator> _barcodeCreator;
        private readonly Mock<IFilePathInquirer> _filePathInquirer;
        private readonly Mock<IImageSourceDownloader> _imageSourceDownloader;

        private readonly BarcodeModel _target;

        public BarcodeModelTest()
        {
            _barcodeCreator = new Mock<IBarcodeCreator>();
            _filePathInquirer = new Mock<IFilePathInquirer>();
            _imageSourceDownloader = new Mock<IImageSourceDownloader>();

            _target = new BarcodeModel(
                _barcodeCreator.Object,
                _filePathInquirer.Object,
                _imageSourceDownloader.Object);
        }

        [Fact]
        public void InitializeTest()
        {
            Assert.Null(_target.Barcode);
        }

        [Fact]
        public void CreateTest()
        {
            var pf = PixelFormats.Pbgra32;
            int rawStride = (200 * pf.BitsPerPixel + 7) / 8;
            byte[] rawImage = new byte[rawStride * 200];
            var bitmap = BitmapSource.Create(
                200, 200,
                96, 96,
                pf, null,
                rawImage, rawStride);
            _barcodeCreator
                .Setup(x => x.Create(It.IsAny<string>(), It.IsAny<BarcodeCreatorOption>()))
                .Returns(bitmap);
            var option = new BarcodeCreatorOption
            {
                Format = BarcodeFormat.QR_CODE,
                Height = 240,
                Width = 240,
                Margin = 5
            };

            _target.Create("foo_bar", option);

            Assert.Equal(bitmap, _target.Barcode);
            _barcodeCreator.Verify(
                x => x.Create("foo_bar", option),
                Times.Once);
        }

        [Fact]
        public void DownloadTest()
        {
            _filePathInquirer
                .Setup(x => x.Inquery())
                .Returns("foo_bar");

            _target.Download();

            _imageSourceDownloader.Verify(
                x => x.Download(null, "foo_bar"), Times.Once);
        }
    }
}
