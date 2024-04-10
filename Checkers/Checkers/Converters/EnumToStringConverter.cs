using Checkers.Models;
using System.Globalization;
using System.Windows.Data;

namespace Checkers.Converters
{
    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EPlayerTurn turn)
            {
                switch (turn)
                {
                    case EPlayerTurn.white:
                        return "White to move";
                    case EPlayerTurn.red:
                        return "Red to move";
                    default:
                        return "";
                }
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}