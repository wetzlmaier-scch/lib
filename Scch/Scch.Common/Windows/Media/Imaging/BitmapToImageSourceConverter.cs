using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Scch.Common.Windows.Media.Imaging;

/// <summary>
/// Converts <see cref="Bitmap"/> to <see cref="ImageSource"/>.
/// </summary>
[ValueConversion(typeof(Bitmap), typeof(ImageSource))]
public class BitmapToImageSourceConverter : IValueConverter
{
    /// <summary>
    /// <see cref="IValueConverter.Convert"/>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Bitmap bitmap)
            return null;

        return BitmapSourceHelper.LoadBitmap(bitmap);
    }

    /// <summary>
    /// <see cref="IValueConverter.ConvertBack"/>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
