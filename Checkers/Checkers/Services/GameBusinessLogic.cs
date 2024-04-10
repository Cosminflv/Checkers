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
        private EPlayerType playerTurn;

        private ECellState playerWon;

        private ObservableCollection<ObservableCollection<Cell>> squares;

        private int whiteRemainingPieces;

        private int redRemainingPieces;

        private Tuple<Cell, ObservableCollection<Cell>>? selectedSquare;
        public GameBusinessLogic(ObservableCollection<ObservableCollection<Cell>> squares)
        {
            this.squares = squares;
            whiteRemainingPieces = 12;
            redRemainingPieces = 12;
            playerTurn = EPlayerType.red;
            playerWon = ECellState.none;
        }

        public ECellState PlayerWon
        {
            get { return playerWon; }
            set { playerWon = value; }
        }

        public EPlayerType PlayerTurn
        {
            get { return playerTurn; }
            set { playerTurn = value; }
        }

        public int WhiteRemainingPieces
        {
            get { return whiteRemainingPieces; }
            set { whiteRemainingPieces = value; }
        }

        public int RedRemainingPieces
        {
            get { return redRemainingPieces; }
            set { redRemainingPieces = value; }
        }

        public ObservableCollection<ObservableCollection<Cell>> Squares
        {
            get { return squares; }
            set { squares = value; }
        }

        public event EventHandler RedrawBoardRequested;

        public void ClickAction(Cell obj)
        {
            if (obj.CellState != ECellState.none && IsCorrectPlayerTurn(obj.CellState, playerTurn) && !isGameOver())
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
                    SwitchTurn();
                    OnRedrawBoardRequested(EventArgs.Empty);

                }
            }
        }

        protected virtual void OnRedrawBoardRequested(EventArgs e)
        {
            RedrawBoardRequested?.Invoke(this, e);
        }

        // PRIVATE METHODS
        private void Move(Cell cellToMoveOn)
        {
            Tuple<int, int>? redCapturedPosition = redCaptured(cellToMoveOn);
            Tuple<int, int>? whiteCapturedPosition = whiteCaptured(cellToMoveOn);

            squares[cellToMoveOn.X][cellToMoveOn.Y].CellState = selectedSquare!.Item1.CellState;
            squares[cellToMoveOn.X][cellToMoveOn.Y].DisplayedImage = selectedSquare!.Item1.DisplayedImage;

            squares[selectedSquare.Item1.X][selectedSquare.Item1.Y].DisplayedImage = squares[selectedSquare.Item1.X][selectedSquare.Item1.Y].HiddenImage;

            squares[selectedSquare.Item1.X][selectedSquare.Item1.Y].CellState = ECellState.none;
          

            selectedSquare = null;

            if (redCapturedPosition != null)
            {
                squares[redCapturedPosition.Item1][redCapturedPosition.Item2].CellState = ECellState.none;
                squares[redCapturedPosition.Item1][redCapturedPosition.Item2].DisplayedImage = squares[redCapturedPosition.Item1][redCapturedPosition.Item2].HiddenImage;
                whiteRemainingPieces -= 1;
             
            }

            if(whiteCapturedPosition != null)
            {
                squares[whiteCapturedPosition.Item1][whiteCapturedPosition.Item2].CellState = ECellState.none;
                squares[whiteCapturedPosition.Item1][whiteCapturedPosition.Item2].DisplayedImage = squares[whiteCapturedPosition.Item1][whiteCapturedPosition.Item2].HiddenImage;
                redRemainingPieces -= 1;
        
            }
            if(whiteWon()) playerWon = ECellState.white;
            if(redWon()) playerWon = ECellState.red;
        }

        private Tuple<int, int>? redCaptured(Cell cellToMoveOn)
        {
            if(selectedSquare!.Item1.X - cellToMoveOn.X == 2) // Piece advanced two rows
            {
                if (squares[cellToMoveOn.X + 1][selectedSquare!.Item1.Y - 1].CellState == ECellState.white)
                    return new Tuple<int, int>(cellToMoveOn.X + 1, selectedSquare!.Item1.Y - 1);
                if (squares[cellToMoveOn.X + 1][selectedSquare!.Item1.Y + 1].CellState == ECellState.white)
                    return new Tuple<int, int>(cellToMoveOn.X + 1, selectedSquare!.Item1.Y + 1);
            }
            return null;
        }

        private Tuple<int, int>? whiteCaptured(Cell cellToMoveOn)
        {
            if (cellToMoveOn.X - selectedSquare!.Item1.X == 2) // Piece advanced two rows
            {
                if (squares[cellToMoveOn.X - 1][selectedSquare!.Item1.Y + 1].CellState == ECellState.red)
                    return new Tuple<int, int>(cellToMoveOn.X - 1, selectedSquare!.Item1.Y + 1);
                if (squares[cellToMoveOn.X - 1][selectedSquare!.Item1.Y - 1].CellState == ECellState.red)
                    return new Tuple<int, int>(cellToMoveOn.X - 1, selectedSquare!.Item1.Y - 1);
            }
            return null;
        }

        private ObservableCollection<Cell> GetPossibleMoves(Cell obj)
        {
            ObservableCollection<Cell> possibleMoves = new ObservableCollection<Cell>();

            if (obj.CellState == ECellState.none)
            {
                return possibleMoves;
            }

            if (obj.CellState == ECellState.red)
            {
                AddPossibleMovesForRed(obj, possibleMoves);
            }
            else if (obj.CellState == ECellState.white)
            {
                AddPossibleMovesForWhite(obj, possibleMoves);
            }

            return possibleMoves;
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

        private void AddPossibleMovesForRed(Cell obj, ObservableCollection<Cell> possibleMoves)
        {
            AddNormalMove(obj.X - 1, obj.Y - 1, possibleMoves);
            AddCaptureMove(obj.X - 1, obj.Y - 1, obj.X - 2, obj.Y - 2, ECellState.white, possibleMoves);

            AddNormalMove(obj.X - 1, obj.Y + 1, possibleMoves);
            AddCaptureMove(obj.X - 1, obj.Y + 1, obj.X - 2, obj.Y + 2, ECellState.white, possibleMoves);
        }

        private void AddPossibleMovesForWhite(Cell obj, ObservableCollection<Cell> possibleMoves)
        {
            AddNormalMove(obj.X + 1, obj.Y - 1, possibleMoves);
            AddCaptureMove(obj.X + 1, obj.Y - 1, obj.X + 2, obj.Y - 2, ECellState.red, possibleMoves);

            AddNormalMove(obj.X + 1, obj.Y + 1, possibleMoves);
            AddCaptureMove(obj.X + 1, obj.Y + 1, obj.X + 2, obj.Y + 2, ECellState.red, possibleMoves);
        }

        private void AddNormalMove(int x, int y, ObservableCollection<Cell> possibleMoves)
        {
            if (isInBoard(x, y) && squares[x][y].CellState == ECellState.none)
            {
                possibleMoves.Add(squares[x][y]);
            }
        }

        private void AddCaptureMove(int x, int y, int captureX, int captureY, ECellState targetState, ObservableCollection<Cell> possibleMoves)
        {
            if (isInBoard(x, y) && isInBoard(captureX, captureY) && squares[x][y].CellState == targetState && squares[captureX][captureY].CellState == ECellState.none)
            {
                possibleMoves.Add(squares[captureX][captureY]);
            }
        }

        private bool IsCorrectPlayerTurn(ECellState cellState, EPlayerType turn)
        {
            if (cellState == ECellState.red && turn == EPlayerType.red) return true;
            if (cellState == ECellState.white && turn == EPlayerType.white) return true;
            return false;
        }

        private void SwitchTurn()
        {
            if (playerTurn == EPlayerType.white)
            {
                playerTurn = EPlayerType.red;
                return;
            }
            if (playerTurn == EPlayerType.red)
            {
                playerTurn = EPlayerType.white;
                return;
            }
        }

        private bool isGameOver()
        {
            return whiteWon() || redWon();
        }

        private bool whiteWon()
        {
            return redRemainingPieces == 0;
        }

        private bool redWon()
        {
            return whiteRemainingPieces == 0;
        }
    }
}
