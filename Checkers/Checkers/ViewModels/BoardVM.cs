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

        private GameData gameData;

        private GameVM gameVM;

        private ObservableCollection<ObservableCollection<CellVM>> gameBoard;

        public delegate void GameDataUpdatedEventHandler(GameData updatedGameData);
        public event GameDataUpdatedEventHandler GameDataUpdated;

        public BoardVM(GameVM gameViewModel)
        {
            ObservableCollection<ObservableCollection<Cell>> board = Helper.InitGameBoard();
            bl = new GameBusinessLogic(board, gameViewModel.AllowMultipleJump);
            bl.RedrawBoardRequested += OnRedrawBoardRequested;

            GameData = gameViewModel.GameData;
            this.gameVM = gameViewModel;

            whiteRemainingPieces = bl.WhiteRemainingPieces;
            redRemainingPieces = bl.RedRemainingPieces;
            GameBoard = CellBoardToCellVMBoard(board);
            currentTurn = EPlayerType.red;
            playerWon = ECellState.none;

            gameVM.GameData.RedRemainingPieces = redRemainingPieces;
            gameVM.GameData.WhiteRemainingPieces = whiteRemainingPieces;
            gameVM.GameData.GameBoard = bl.Squares;
            gameVM.GameData.CurrentTurn = currentTurn;
            gameVM.GameData.PlayerWon = playerWon;
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

            GameDataUpdated?.Invoke(GameData);
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
                    CellVM cellVM = new CellVM(c.X, c.Y, c.HiddenImage, c.DisplayedImage, c.CellState, c.IsKing, bl);
                    line.Add(cellVM);
                }
                result.Add(line);
            }
            return result;
        }
    }
}
