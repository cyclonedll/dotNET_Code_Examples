using Emgu.CV;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Windows;

namespace WpfInteractiveWithOtherGraphicFramework;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {

        var imageFile = @"H:\My Pictures\082ce3fcde18045d08244da6.bmp";

        using var image = SixLabors.ImageSharp.Image.Load<Bgra32>(imageFile);
        int width = image.Width / 5;
        int height = image.Height / 5;
        image.Mutate(x => x.Resize(width, height));
        ImageBox1.Source = image.ToWriteableBitmap();




        imageFile = @"H:\My Pictures\6.jpg";

        using var image2 = CvInvoke.Imread(imageFile);
        CvInvoke.Resize(image2, image2, new System.Drawing.Size(image2.Width / 5, image2.Height / 5));
        ImageBox2.Source = image2.ToWriteableBitmap_2();
    }
}