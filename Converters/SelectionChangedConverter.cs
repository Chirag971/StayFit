using System.Globalization;

namespace StayFitz.Converters
{
    public class SelectionChangedConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType,
            object? parameter, CultureInfo culture)
        {
            var args = value as SelectionChangedEventArgs;
            return args?.CurrentSelection?.FirstOrDefault();
        }

        public object? ConvertBack(object? value,
            Type targetType, object? parameter,
            CultureInfo culture)
            => throw new NotImplementedException();
    }
}
