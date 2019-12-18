using MakeQrCodeKun.Models.Impls;
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
        private readonly Mock<IBarcodeModel> _model;

        private readonly MainWindowViewModel _target;

        public MainWindowViewModelTest()
        {
            _model = new Mock<IBarcodeModel>();

            _target = new MainWindowViewModel(_model.Object);
        }

        [Fact]
        public void InitializeTest()
        {
            Assert.Equal("QR ƒR[ƒhì‚é‚­‚ñ", _target.Title);
            Assert.Empty(_target.PlainValue);
            Assert.Null(_target.Barcode);
            Assert.NotNull(_target.CreateBarcodeCommand);
            Assert.NotNull(_target.DownloadBarodeCommand);
        }

        [Fact]
        public void CreateBarcodeTest()
        {
            var pf = PixelFormats.Pbgra32;
            int rawStride = (200 * pf.BitsPerPixel + 7) / 8;
            byte[] rawImage = new byte[rawStride * 200];
            var bitmap = BitmapSource.Create(
                200, 200,
                96, 96,
                pf, null,
                rawImage, rawStride);
            _model
                .SetupGet(x => x.Barcode)
                .Returns(bitmap);

            _target.PlainValue = "foo_bar";
            _target.CreateBarcodeCommand.Execute();

            Assert.Equal(bitmap, _target.Barcode);
            _model.Verify(
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
        public void DownloadBarcodeTest()
        {
            _target.DownloadBarodeCommand.Execute();

            _model.Verify(x => x.Download(), Times.Once);
        }
    }
}
