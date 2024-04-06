using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Checkers.Services
{
    class GameBusinessLogic
    {
        private ObservableCollection<ObservableCollection<Cell>> squares;

        private Tuple<Cell, ObservableCollection<Cell>>? selectedSquare;
        public GameBusinessLogic(ObservableCollection<ObservableCollection<Cell>> squares)
        {
            this.squares = squares;
        }

        private ObservableCollection<Cell> GetPossibleMoves(Cell obj)
        {
            ObservableCollection<Cell> possibleMoves = new ObservableCollection<Cell>();
            if (obj.CellState == ECellState.none)
            {
                return possibleMoves;
            }

            if(obj.CellState == ECellState.red)
            {
                if (isInBoard(obj.X - 1, obj.Y - 1))
                {
                    if (squares[obj.X - 1][obj.Y - 1].CellState == ECellState.none)
                    {
                        possibleMoves.Add(squares[obj.X - 1][obj.Y - 1]);
                    }
                }
                if(isInBoard(obj.X - 1, obj.Y + 1))
                {
                    if (squares[obj.X - 1][obj.Y + 1].CellState == ECellState.none)
                    {
                        possibleMoves.Add(squares[obj.X - 1][obj.Y + 1]);
                    }
                }
            }

            if (obj.CellState == ECellState.white)
            {
                if (isInBoard(obj.X + 1, obj.Y - 1))
                {
                    if (squares[obj.X + 1][obj.Y - 1].CellState == ECellState.none)
                    {
                        possibleMoves.Add(squares[obj.X + 1][obj.Y - 1]);
                    }
                }
                if (isInBoard(obj.X + 1, obj.Y + 1))
                {
                    if (squares[obj.X + 1][obj.Y + 1].CellState == ECellState.none)
                    {
                        possibleMoves.Add(squares[obj.X + 1][obj.Y + 1]);
                    }
                }
            }
            return possibleMoves;
        }

        public void ClickAction(Cell obj)
        {
            if (obj.CellState != ECellState.none)
            {
                ObservableCollection<Cell> possibleMoves = GetPossibleMoves(obj);
                selectedSquare = new Tuple<Cell, ObservableCollection<Cell>> (obj, possibleMoves);
            }

            if(obj.CellState == ECellState.none && selectedSquare != null)
            {
                if (isMovePossible(obj))
                {
                    //EXECUTE MOVE
                    Move(obj);
                }
            }
        }

        private void Move(Cell cellToMoveOn)
        {
            cellToMoveOn.CellState = selectedSquare!.Item1.CellState;
            cellToMoveOn.DisplayedImage = selectedSquare.Item1.DisplayedImage;
            selectedSquare.Item1.DisplayedImage = selectedSquare.Item1.HiddenImage;
            selectedSquare.Item1.CellState = ECellState.none;

            Cell cellToUpdate = squares[selectedSquare.Item1.X][selectedSquare.Item1.Y];
            cellToUpdate.CellState = ECellState.none;
        }

        private bool isMovePossible(Cell cellToMoveOn)
        {
            foreach(Cell possibleMove in selectedSquare!.Item2)
            {
                if (cellToMoveOn.X == possibleMove.X && cellToMoveOn.Y == possibleMove.Y)
                    return true;
            }
            return false;
        }


        private bool isInBoard(int x, int y)
        {
            return x >= 0 && x <= 7 && y >= 0 && y <= 7;
        }
    }
}
