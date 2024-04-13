using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        private GameData gameData;

        private GameVM gameVM;

        private ObservableCollection<ObservableCollection<CellVM>> gameBoard;

        private GameStatistics statistics;

        public BoardVM(GameVM gameViewModel, GameStatistics statistics)
        {
            gameData = gameViewModel.GameData;
            if (gameData != null)
            {
                WhiteRemainingPieces = gameData.WhiteRemainingPieces;
                RedRemainingPieces = gameData.RedRemainingPieces;
                bl = new GameBusinessLogic(gameData.GameBoard, gameData.AllowMultipleJump, gameData.WhiteRemainingPieces, gameData.RedRemainingPieces, gameData.CurrentTurn, gameData.PlayerWon);
                bl.RedrawBoardRequested += OnRedrawBoardRequested;
                GameBoard = CellBoardToCellVMBoard(gameData.GameBoard);
                CurrentTurn = gameData.CurrentTurn;
                PlayerWon = gameData.PlayerWon;
            }
            else
            {
                ObservableCollection<ObservableCollection<Cell>> board = Helper.InitGameBoard();
                bl = new GameBusinessLogic(board, gameViewModel.AllowMultipleJump, null, null, null, null);
                bl.RedrawBoardRequested += OnRedrawBoardRequested;

                whiteRemainingPieces = bl.WhiteRemainingPieces;
                redRemainingPieces = bl.RedRemainingPieces;
                GameBoard = CellBoardToCellVMBoard(board);
                currentTurn = EPlayerType.red;
                playerWon = ECellState.none;
            }
            this.gameVM = gameViewModel;
            GameStatistics = statistics;

            gameVM.GameData.RedRemainingPieces = redRemainingPieces;
            gameVM.GameData.WhiteRemainingPieces = whiteRemainingPieces;
            gameVM.GameData.GameBoard = bl.Squares;
            gameVM.GameData.CurrentTurn = currentTurn;
            gameVM.GameData.PlayerWon = playerWon;
        }

        public GameStatistics GameStatistics
        {
            get { return statistics; }
            set {  statistics = value; OnPropertyChanged("Statistics"); }
        }
        
        public GameData GameData
        {
            get { return gameData; }
            set { gameData = value; OnPropertyChanged("GameData"); }  
        }

        public ObservableCollection<ObservableCollection<CellVM>> GameBoard
        {
            get { return gameBoard; }
            set { gameBoard = value; OnPropertyChanged("GameBoard"); }
        }

        private int whiteRemainingPieces;

        private int redRemainingPieces;

        private EPlayerType currentTurn;

        private ECellState playerWon;

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

        public EPlayerType CurrentTurn
        {
            get { return currentTurn; }
            set
            {
                if (currentTurn != value)
                {
                    currentTurn = value;
                    OnPropertyChanged("CurrentTurn");
                }
            }
        }

        public ECellState PlayerWon
        {
            get { return playerWon; }
            set
            {
                if (playerWon != value)
                {
                    playerWon = value;
                    OnPropertyChanged("PlayerWon");
                }
            }
        }

        private void OnRedrawBoardRequested(object sender, EventArgs e)
        {
            GameBoard.Clear();
            GameBoard = CellBoardToCellVMBoard(bl.Squares);
            WhiteRemainingPieces = bl.WhiteRemainingPieces;
            RedRemainingPieces = bl.RedRemainingPieces;
            CurrentTurn = bl.PlayerTurn;
            PlayerWon = bl.PlayerWon;


            gameVM.GameData.RedRemainingPieces = redRemainingPieces;
            gameVM.GameData.WhiteRemainingPieces = whiteRemainingPieces;
            gameVM.GameData.GameBoard = bl.Squares;
            gameVM.GameData.CurrentTurn = currentTurn;
            gameVM.GameData.PlayerWon = playerWon;

            UpdateStatistics();
        }

        // DELEGATES

        public delegate void SwitchToGame(GameStatistics s);
        public SwitchToGame OnSwitchToGame { get; set; }

        // COMMANDS

        private ICommand switchToGameCommand;

        public ICommand SwitchToGameCommand
        {
            get
            {
                if (switchToGameCommand == null)
                {
                    switchToGameCommand = new RelayPagesCommand(o => true, o => { OnSwitchToGame(GameStatistics); });
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
                    CellVM cellVM = new CellVM(c.X, c.Y, c.HiddenImage, c.DisplayedImage, c.CellState, c.IsKing, bl);
                    line.Add(cellVM);
                }
                result.Add(line);
            }
            return result;
        }

        private void UpdateStatistics()
        {
            if (gameData.PlayerWon == ECellState.red)
            {
                GameStatistics.RedWins += 1;
                if (gameData.RedRemainingPieces > statistics.MaxPiecesLeft)
                    GameStatistics.MaxPiecesLeft = gameData.RedRemainingPieces;
                GameStatistics.OnSaveStatistics();
            }
            if (gameData.PlayerWon == ECellState.white)
            {
                GameStatistics.WhiteWins += 1;
                if (gameData.WhiteRemainingPieces > statistics.MaxPiecesLeft)
                    GameStatistics.MaxPiecesLeft = gameData.WhiteRemainingPieces;
                GameStatistics.OnSaveStatistics();
            }
        }
    }
}
