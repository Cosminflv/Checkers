using Checkers.Models;
using System.Globalization;
using System.Windows.Data;

namespace Checkers.Converters
{
    public class PlayerTurnToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EPlayerType turn)
            {
                switch (turn)
                {
                    case EPlayerType.white:
                        return "White to move";
                    case EPlayerType.red:
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

    public class PlayerWonToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is ECellState player)
            {
                switch(player)
                {
                    case ECellState.white:
                        return "White Won!";
                    case ECellState.red:
                        return "Red Won!";

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