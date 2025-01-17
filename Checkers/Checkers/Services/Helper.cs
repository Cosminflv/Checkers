﻿using Checkers.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Security.Policy;
using System.Windows.Data;


namespace Checkers.Services
{
    class Helper
    {
        public static Cell CurrentCell { get; set; }
        public static Cell PreviousCell { get; set; }
        public static ObservableCollection<ObservableCollection<Cell>> InitGameBoard()
        {
            return new ObservableCollection<ObservableCollection<Cell>>()
            {
                new ObservableCollection<Cell>()
                {
                    new Cell(0, 0, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(0, 1, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/white_piece.png", ECellState.white, false),
                    new Cell(0, 2, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(0, 3, "/Checkers;component/Resources/black_square.png","/Checkers;component/Resources/white_piece.png", ECellState.white, false),
                    new Cell(0, 4, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(0, 5, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/white_piece.png", ECellState.white, false),
                    new Cell(0, 6, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(0, 7, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/white_piece.png", ECellState.white, false)
                },
                new ObservableCollection<Cell>()
                {
                    new Cell(1, 0, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/white_piece.png", ECellState.white, false),
                    new Cell(1, 1, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(1, 2, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/white_piece.png", ECellState.white, false),  // Flipped for piece on right
                    new Cell(1, 3, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(1, 4, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/white_piece.png", ECellState.white, false),  // Flipped for piece on right
                    new Cell(1, 5, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(1, 6, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/white_piece.png", ECellState.white, false),  // Flipped for piece on right
                    new Cell(1, 7, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false)
                },
                new ObservableCollection<Cell>()
                {
                    new Cell(2, 0, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(2, 1, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/white_piece.png", ECellState.white, false),  // Flipped for piece on right
                    new Cell(2, 2, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(2, 3, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/white_piece.png", ECellState.white, false),  // Flipped for piece on right
                    new Cell(2, 4, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(2, 5, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/white_piece.png", ECellState.white, false),  // Flipped for piece on right
                    new Cell(2, 6, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(2, 7, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/white_piece.png", ECellState.white, false),  // Flipped for piece on right
                },
                new ObservableCollection<Cell>()
                {
                    new Cell(3, 0, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/black_square.png", ECellState.none, false),
                    new Cell(3, 1, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(3, 2, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/black_square.png", ECellState.none, false),
                    new Cell(3, 3, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(3, 4, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/black_square.png", ECellState.none, false),
                    new Cell(3, 5, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(3, 6, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/black_square.png", ECellState.none, false),
                    new Cell(3, 7, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                },
                new ObservableCollection<Cell>()
                {
                    new Cell(4, 0, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(4, 1, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/black_square.png", ECellState.none, false),
                    new Cell(4, 2, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(4, 3, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/black_square.png", ECellState.none, false),
                    new Cell(4, 4, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(4, 5, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/black_square.png", ECellState.none, false),
                    new Cell(4, 6, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(4, 7, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/black_square.png", ECellState.none, false),
                },
                new ObservableCollection<Cell>()
                {
                    new Cell(5, 0, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/red_piece.png", ECellState.red, false),  // Flipped for piece on right
                    new Cell(5, 1, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(5, 2, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/red_piece.png", ECellState.red, false),  // Flipped for piece on right
                    new Cell(5, 3, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(5, 4, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/red_piece.png", ECellState.red, false),  // Flipped for piece on right
                    new Cell(5, 5, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(5, 6, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/red_piece.png", ECellState.red, false),  // Flipped for piece on right
                    new Cell(5, 7, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),

                },
                new ObservableCollection<Cell>()
                {
                    new Cell(6, 0, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(6, 1, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/red_piece.png", ECellState.red, false),  // Flipped for piece on right
                    new Cell(6, 2, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(6, 3, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/red_piece.png", ECellState.red, false),  // Flipped for piece on right
                    new Cell(6, 4, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(6, 5, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/red_piece.png", ECellState.red, false),  // Flipped for piece on right
                    new Cell(6, 6, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(6, 7, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/red_piece.png", ECellState.red, false),  // No piece, flipped order

                },
                new ObservableCollection<Cell>()
                {
                    new Cell(7, 0, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/red_piece.png", ECellState.red, false),  // Flipped for piece on right
                    new Cell(7, 1, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(7, 2, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/red_piece.png", ECellState.red, false),  // Flipped for piece on right
                    new Cell(7, 3, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(7, 4, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/red_piece.png", ECellState.red, false),  // No change
                    new Cell(7, 5, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),
                    new Cell(7, 6, "/Checkers;component/Resources/black_square.png", "/Checkers;component/Resources/red_piece.png", ECellState.red, false),  // Flipped for piece on right
                    new Cell(7, 7, "/Checkers;component/Resources/white_square.png", "/Checkers;component/Resources/white_square.png", ECellState.none, false),

                }
            };
        }
    }
}


public class EnumToStringConverter : IValueConverter
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
