using Checkers.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
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

        private bool multipleJumpsAllowed;

        private ObservableCollection<Cell> possibleMultipleJumpMoves;
        public GameBusinessLogic(ObservableCollection<ObservableCollection<Cell>> squares, bool multipleJumps)
        {
            this.squares = squares;
            whiteRemainingPieces = 12;
            redRemainingPieces = 12;
            playerTurn = EPlayerType.red;
            playerWon = ECellState.none;
            multipleJumpsAllowed = multipleJumps;
            possibleMultipleJumpMoves = new ObservableCollection<Cell>();
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
                ObservableCollection<Cell> possibleMoves = possibleMultipleJumpMoves.Count > 0 ? possibleMultipleJumpMoves : GetPossibleMoves(obj);

                selectedSquare = new Tuple<Cell, ObservableCollection<Cell>>(obj, possibleMoves);
            }

            if (obj.CellState == ECellState.none && selectedSquare != null)
            {
                if (isMovePossible(obj))
                {
                    //EXECUTE MOVE
                    if (!Move(obj))
                    {
                        SwitchTurn();
                    }
                    else
                    {
                        //SCAN FOR JUMPING OPORTUNITIES
                        if (multipleJumpsAllowed)
                        {
                            ScanForJumpingOportunities(obj);

                        }
                        if (possibleMultipleJumpMoves.Count == 0)
                        {
                            SwitchTurn();
                        }
                    }

                    OnRedrawBoardRequested(EventArgs.Empty);
                }
            }
        }

        void ScanForJumpingOportunities(Cell obj)
        {
            possibleMultipleJumpMoves.Clear();
            if (squares[obj.X][obj.Y].CellState == ECellState.red)
            {
                if (isInBoard(obj.X - 2, obj.Y - 2))
                {
                    if (squares[obj.X - 1][obj.Y - 1].CellState == ECellState.white && squares[obj.X - 2][obj.Y - 2].CellState == ECellState.none)
                    {
                        possibleMultipleJumpMoves.Add(squares[obj.X - 2][obj.Y - 2]);
                    }
                }
                if (isInBoard(obj.X - 2, obj.Y + 2))
                {
                    if (squares[obj.X - 1][obj.Y + 1].CellState == ECellState.white && squares[obj.X - 2][obj.Y + 2].CellState == ECellState.none)
                    {
                        possibleMultipleJumpMoves.Add(squares[obj.X - 2][obj.Y + 2]);
                    }
                }
                return;
            }

            if (squares[obj.X][obj.Y].CellState == ECellState.white)
            {
                if (isInBoard(obj.X + 2, obj.Y - 2))
                {
                    if (squares[obj.X + 1][obj.Y - 1].CellState == ECellState.red && squares[obj.X + 2][obj.Y - 2].CellState == ECellState.none)
                    {
                        possibleMultipleJumpMoves.Add(squares[obj.X + 2][obj.Y - 2]);
                    }
                }
                if (isInBoard(obj.X + 2, obj.Y + 2))
                {
                    if (squares[obj.X + 1][obj.Y + 1].CellState == ECellState.red && squares[obj.X + 2][obj.Y + 2].CellState == ECellState.none)
                    {
                        possibleMultipleJumpMoves.Add(squares[obj.X + 2][obj.Y + 2]);
                    }
                }
                return;
            }
        }

        protected virtual void OnRedrawBoardRequested(EventArgs e)
        {
            RedrawBoardRequested?.Invoke(this, e);
        }

        // PRIVATE METHODS
        private bool Move(Cell cellToMoveOn)
        {
            bool hasCaptured = false;

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
                hasCaptured = true;
            }

            if (whiteCapturedPosition != null)
            {
                squares[whiteCapturedPosition.Item1][whiteCapturedPosition.Item2].CellState = ECellState.none;
                squares[whiteCapturedPosition.Item1][whiteCapturedPosition.Item2].DisplayedImage = squares[whiteCapturedPosition.Item1][whiteCapturedPosition.Item2].HiddenImage;
                redRemainingPieces -= 1;
                hasCaptured = true;
            }

            if (CanPromote(squares[cellToMoveOn.X][cellToMoveOn.Y].X, squares[cellToMoveOn.X][cellToMoveOn.Y].CellState))
            {
                Promote(squares[cellToMoveOn.X][cellToMoveOn.Y]);
            }

            if (whiteWon()) playerWon = ECellState.white;
            if (redWon()) playerWon = ECellState.red;
            return hasCaptured;
        }

        private Tuple<int, int>? redCaptured(Cell cellToMoveOn)
        {

            if (selectedSquare!.Item1.X - cellToMoveOn.X == 2) // Piece advanced two rows
            {

                if (isInBoard(cellToMoveOn.X + 1, cellToMoveOn.Y - 1))
                {
                    // TOP LEFT CAPTURE
                    if (squares[cellToMoveOn.X + 1][cellToMoveOn.Y - 1].CellState == ECellState.white && selectedSquare!.Item1.X - 1 == cellToMoveOn.X + 1 && selectedSquare!.Item1.Y + 1 == cellToMoveOn.Y - 1)
                        return new Tuple<int, int>(cellToMoveOn.X + 1, cellToMoveOn.Y - 1);
                }
                if (isInBoard(cellToMoveOn.X + 1, cellToMoveOn.Y + 1))
                {
                    // TOP RIGHT CAPTURE
                    if (squares[cellToMoveOn.X + 1][cellToMoveOn.Y + 1].CellState == ECellState.white && selectedSquare!.Item1.X - 1 == cellToMoveOn.X + 1 && selectedSquare!.Item1.Y - 1 == cellToMoveOn.Y + 1)
                        return new Tuple<int, int>(cellToMoveOn.X + 1, cellToMoveOn.Y + 1);
                }
            }
            return null;
        }

        private Tuple<int, int>? whiteCaptured(Cell cellToMoveOn)
        {
            if (cellToMoveOn.X - selectedSquare!.Item1.X == 2) // Piece advanced two rows
            {

                // BOTTOM LEFT CAPTURE
                if (isInBoard(cellToMoveOn.X - 1, cellToMoveOn.Y - 1))
                {
                    if (squares[cellToMoveOn.X - 1][cellToMoveOn.Y - 1].CellState == ECellState.red && selectedSquare!.Item1.X + 1 == cellToMoveOn.X - 1 && selectedSquare!.Item1.Y + 1 == cellToMoveOn.Y - 1)
                        return new Tuple<int, int>(cellToMoveOn.X - 1, cellToMoveOn.Y - 1);
                }
                // BOTTOM RIGHT CAPTURE
                if (isInBoard(cellToMoveOn.X - 1, cellToMoveOn.Y + 1))
                {
                    if (squares[cellToMoveOn.X - 1][cellToMoveOn.Y + 1].CellState == ECellState.red && selectedSquare!.Item1.X + 1 == cellToMoveOn.X - 1 && selectedSquare!.Item1.Y - 1 == cellToMoveOn.Y + 1)
                        return new Tuple<int, int>(cellToMoveOn.X - 1, cellToMoveOn.Y + 1);
                }

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
            foreach (Cell possibleMove in selectedSquare!.Item2)
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


            if (obj.IsKing)
            {
                AddNormalMove(obj.X + 1, obj.Y - 1, possibleMoves);
                AddCaptureMove(obj.X + 1, obj.Y - 1, obj.X + 2, obj.Y - 2, ECellState.white, possibleMoves);

                AddNormalMove(obj.X + 1, obj.Y + 1, possibleMoves);
                AddCaptureMove(obj.X + 1, obj.Y + 1, obj.X + 2, obj.Y + 2, ECellState.white, possibleMoves);
            }
        }

        private void AddPossibleMovesForWhite(Cell obj, ObservableCollection<Cell> possibleMoves)
        {
            AddNormalMove(obj.X + 1, obj.Y - 1, possibleMoves);
            AddCaptureMove(obj.X + 1, obj.Y - 1, obj.X + 2, obj.Y - 2, ECellState.red, possibleMoves);

            AddNormalMove(obj.X + 1, obj.Y + 1, possibleMoves);
            AddCaptureMove(obj.X + 1, obj.Y + 1, obj.X + 2, obj.Y + 2, ECellState.red, possibleMoves);

            if (obj.IsKing)
            {
                AddNormalMove(obj.X - 1, obj.Y - 1, possibleMoves);
                AddCaptureMove(obj.X - 1, obj.Y - 1, obj.X - 2, obj.Y - 2, ECellState.red, possibleMoves);

                AddNormalMove(obj.X - 1, obj.Y + 1, possibleMoves);
                AddCaptureMove(obj.X - 1, obj.Y + 1, obj.X - 2, obj.Y + 2, ECellState.red, possibleMoves);
            }
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

        private bool CanPromote(int x, ECellState state)
        {
            if (x == 0 && state == ECellState.red) return true;
            if (x == 7 && state == ECellState.white) return true;
            return false;
        }

        private void Promote(Cell c)
        {
            c.IsKing = true;
            c.DisplayedImage = "/Checkers;component/Resources/king_piece.png";
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
