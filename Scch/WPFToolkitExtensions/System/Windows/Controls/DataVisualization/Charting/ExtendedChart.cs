using System.IO;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFToolkitExtensions.System.Windows.Controls.DataVisualization.Charting;

public class ExtendedChart : Chart
{
    public void Save(string filename)
    {
        RenderTargetBitmap renderBitmap = new((int) ActualWidth, (int) ActualHeight, 96d, 96d,
            PixelFormats.Pbgra32);

        Size size = new(ActualWidth, ActualHeight);

        Rectangle rect = new() {Width = ActualWidth, Height = ActualHeight, Fill = new VisualBrush(this)};
        rect.Measure(size);
        rect.Arrange(new Rect(size));
        rect.UpdateLayout();

        renderBitmap.Render(rect);

        // Create a file stream for saving image
        using (FileStream fs = new(filename, FileMode.Create))
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
            encoder.Save(fs);
            fs.Flush();
            fs.Close();
        }
    }
}
