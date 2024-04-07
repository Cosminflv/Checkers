using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Checkers.ViewModels
{
    class BoardVM : BaseVM
    {
        private GameBusinessLogic bl;

        public BoardVM()
        {
            ObservableCollection<ObservableCollection<Cell>> board = Helper.InitGameBoard();
            bl = new GameBusinessLogic(board);
            bl.RedrawBoardRequested += OnRedrawBoardRequested;
            whiteRemainingPieces = bl.WhiteRemainingPieces;
            redRemainingPieces = bl.RedRemainingPieces;
            GameBoard = CellBoardToCellVMBoard(board);
        }

        private ObservableCollection<ObservableCollection<CellVM>> gameBoard;

        public ObservableCollection<ObservableCollection<CellVM>> GameBoard
        {
            get { return gameBoard; }
            set { gameBoard = value; OnPropertyChanged("GameBoard"); }
        }

        private int whiteRemainingPieces;

        private int redRemainingPieces;

        public int WhiteRemainingPieces
        {
            get { return whiteRemainingPieces; }
            set { whiteRemainingPieces = value; OnPropertyChanged("WhiteRemainingPieces"); }
        }

        public int RedRemainingPieces
        {
            get { return redRemainingPieces; }
            set { redRemainingPieces = value; OnPropertyChanged("RedRemainingPieces"); }
        }

        private void OnRedrawBoardRequested(object sender, EventArgs e)
        {
            GameBoard.Clear();
            GameBoard = CellBoardToCellVMBoard(bl.Squares);
            WhiteRemainingPieces = bl.WhiteRemainingPieces;
            RedRemainingPieces = bl.RedRemainingPieces;
        }

        private void RedrawBoard()
        {

        }

        // DELEGATES

        public delegate void SwitchToGame();
        public SwitchToGame OnSwitchToGame { get; set; }

        // COMMANDS

        private ICommand switchToGameCommand;

        public ICommand SwitchToGameCommand
        {
            get
            {
                if (switchToGameCommand == null)
                {
                    switchToGameCommand = new RelayPagesCommand(o => true, o => { OnSwitchToGame(); });
                }

                return switchToGameCommand;
            }
        }

        // METHODS
        private ObservableCollection<ObservableCollection<CellVM>> CellBoardToCellVMBoard(ObservableCollection<ObservableCollection<Cell>> board)
        {
            ObservableCollection<ObservableCollection<CellVM>> result = new ObservableCollection<ObservableCollection<CellVM>>();
            for (int i = 0; i < board.Count; i++)
            {
                ObservableCollection<CellVM> line = new ObservableCollection<CellVM>();
                for (int j = 0; j < board[i].Count; j++)
                {
                    Cell c = board[i][j];
                    CellVM cellVM = new CellVM(c.X, c.Y, c.HiddenImage, c.DisplayedImage, c.CellState, bl);
                    line.Add(cellVM);
                }
                result.Add(line);
            }
            return result;
        }
    }
}
