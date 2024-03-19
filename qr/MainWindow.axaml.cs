using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Channels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using Color = Avalonia.Media.Color;
using Image = Avalonia.Controls.Image;



namespace qr;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Create_OnClick(object? sender, RoutedEventArgs e)
    {
        string path = $@"QRs\{QRText.Text}.png";
        QRCodeEncoder qr = new QRCodeEncoder();
        Bitmap qrimage = qr.Encode(QRText.Text);
        
        qrimage.Save(path, ImageFormat.Png);
        var result = new Avalonia.Media.Imaging.Bitmap(path);
        QRImage.Source = result;
    }

    private void ReadQR_OnClick(object? sender, RoutedEventArgs e)
    {
        var file = StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            
        });
        try
        {
            var path = file.Result[0].Path.AbsolutePath;
            Console.WriteLine(path);
            QRCodeDecoder decoder = new QRCodeDecoder();
            Bitmap img = new Bitmap(path);
            var result = decoder.Decode(new QRCodeBitmapImage(new Bitmap(img)));
            ReadedQRText.Text = result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}