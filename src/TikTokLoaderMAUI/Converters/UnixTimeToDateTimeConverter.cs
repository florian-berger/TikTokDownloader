using System.Globalization;

namespace TikTokLoaderMAUI.Converters
{
    public class UnixTimeToDateTimeConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long unixValue)
            {
                var offset = DateTimeOffset.FromUnixTimeSeconds(unixValue);
                return new DateTime(offset.Ticks, DateTimeKind.Utc);
            }

            throw new ArgumentOutOfRangeException(nameof(value), "Value is not of type long.");
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTimeValue)
            {
                var offset = new DateTimeOffset(dateTimeValue, TimeSpan.Zero);
                return offset.ToUnixTimeMilliseconds();
            }

            throw new ArgumentOutOfRangeException(nameof(value), "Value is not of type DateTime.");
        }
    }
}
