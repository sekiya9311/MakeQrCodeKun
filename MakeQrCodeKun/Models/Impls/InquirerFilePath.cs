using MakeQrCodeKun.Models.Interfaces;
using Microsoft.Win32;

namespace MakeQrCodeKun.Models.Impls
{
    public class InquirerFilePath : IInquirerFilePath
    {
        public string Inquery()
        {
            var dialog = new SaveFileDialog
            {
                Filter = "jpeg|*.jpg|bmp|*.bmp|png|*.png",
            };

            return dialog.ShowDialog().GetValueOrDefault()
                ? dialog.FileName
                : "";
        }
    }
}
