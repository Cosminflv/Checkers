using Checkers.Models;
using Checkers.Services;
using Microsoft.Win32;
using System.IO;
using System.Text.Json;
using System.Windows.Input;

namespace Checkers.ViewModels
{
    class GameVM : BaseVM
    {
        private GameData gameData;

        private GameStatistics statistics;

        private JsonHandler jsonHandler;

        public GameVM()
        {
            gameData = new GameData(Helper.InitGameBoard(), 12, 12, EPlayerType.red, ECellState.none, allowMultipleJump);
            jsonHandler = new JsonHandler();
        }

        public GameStatistics Statistics
        {
            get { return statistics; }
            set
            {
                statistics = value;
                OnPropertyChanged("Statistics");
            }
        }

        public GameData GameData
        {
            get { return gameData; }
            set
            {
                gameData = value;
                OnPropertyChanged("GameData");
            }
        }

        private bool allowMultipleJump;
        public bool AllowMultipleJump
        {
            get { return allowMultipleJump; }
            set
            {
                if (allowMultipleJump != value)
                {
                    allowMultipleJump = value;
                    OnPropertyChanged("AllowMultipleJump");
                }
            }
        }

        // DELEGATES

        public delegate void SwitchToHome();
        public SwitchToHome OnSwitchToHome { get; set; }

        public delegate void SwitchToBoard(GameData? loadedGameData);
        public SwitchToBoard OnSwitchToBoard { get; set; }

        public delegate void SwitchToStatistics();
        public SwitchToStatistics OnSwitchToStatistics { get; set; }

        // COMMANDS

        private ICommand switchToHomeCommand;
        public ICommand SwitchToHomeCommand
        {
            get
            {
                if (switchToHomeCommand == null)
                {
                    switchToHomeCommand = new RelayPagesCommand(o => true, o => { OnSwitchToHome(); });
                }

                return switchToHomeCommand;
            }
        }

        private ICommand switchToStatisticsCommand;
        public ICommand SwitchToStatisticsCommand
        {
            get
            {
                if (switchToStatisticsCommand == null)
                {
                    switchToStatisticsCommand = new RelayPagesCommand(o => true, o => { OnSwitchToStatistics(); });
                }

                return switchToStatisticsCommand;
            }
        }

        private ICommand switchToBoardCommand;

        public ICommand SwitchToBoardCommand
        {
            get
            {
                if (switchToBoardCommand == null)
                {
                    switchToBoardCommand = new RelayPagesCommand(o => true, o => { OnSwitchToBoard(null); });
                }
                return switchToBoardCommand;
            }
        }

        private ICommand buttonSaveGameCommand;

        public ICommand SaveGameCommand
        {
            get
            {
                if (buttonSaveGameCommand == null)
                {
                    buttonSaveGameCommand = new RelayPagesCommand(o => true, o => { OnSaveGame(); });
                }
                return buttonSaveGameCommand;
            }
        }

        private ICommand buttonOpenGameCommand;

        public ICommand OpenGameCommand
        {
            get
            {
                if (buttonOpenGameCommand == null)
                {
                    buttonOpenGameCommand = new RelayPagesCommand(o => true, o => { OnOpenGame(); });
                }
                return buttonOpenGameCommand;
            }
        }

        // METHODS

        private void OnSaveGame()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    jsonHandler.SaveToJson(filePath, gameData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving file: {ex.Message}");
                    // You can add more sophisticated error handling as needed
                }
            }
        }

        private void OnOpenGame()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    GameData openedGameData = jsonHandler.LoadFromJson<GameData>(filePath);
                    GameData = openedGameData;
                    OnSwitchToBoard(GameData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error opening file: {ex.Message}");
                    // You can add more sophisticated error handling as needed
                }
            }
        }
    }
}
