using MakeQrCodeKun.Models.Interfaces;
using MakeQrCodeKun.ViewModels;
using Moq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Xunit;

namespace MakeQrCodeKun.Tests
{
    public class MainWindowViewModelTest
    {
        private readonly Mock<IBarcodeCreator> _barcodeCreator;
        private readonly Mock<IFilePathInquirer> _filePathInquirer;
        private readonly Mock<IImageSourceDownloader> _imageSourceDownloader;

        private readonly MainWindowViewModel _target;

        public MainWindowViewModelTest()
        {
            _barcodeCreator = new Mock<IBarcodeCreator>();
            _filePathInquirer = new Mock<IFilePathInquirer>();
            _imageSourceDownloader = new Mock<IImageSourceDownloader>();

            _target = new MainWindowViewModel(
                _barcodeCreator.Object,
                _filePathInquirer.Object,
                _imageSourceDownloader.Object);
        }

        [Fact]
        public void InitializeTest()
        {
            Assert.Equal("QR ƒR[ƒhì‚é‚­‚ñ", _target.Title);
            Assert.Empty(_target.PlainValue);
            Assert.Null(_target.BarcodeImage);
            Assert.NotNull(_target.MakeQrCodeCommand);
            Assert.NotNull(_target.DownloadQrCodeCommand);
        }

        [Fact]
        public void MakeQrCodeTest()
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

            _target.PlainValue = "foo_bar";
            _target.MakeQrCodeCommand.Execute();

            Assert.Equal(bitmap, _target.BarcodeImage);
            _barcodeCreator.Verify(
                x => x.Create("foo_bar", new BarcodeCreatorOption
                {
                    Format = BarcodeFormat.QR_CODE,
                    Height = 240,
                    Width = 240,
                    Margin = 5
                }),
                Times.Once);
        }

        [Fact]
        public void DownloadQrCodeTest()
        {
            _filePathInquirer
                .Setup(x => x.Inquery())
                .Returns("foo_bar");

            _target.DownloadQrCodeCommand.Execute();

            _imageSourceDownloader.Verify(
                x => x.Download(null, "foo_bar"), Times.Once);
        }
    }
}
