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

        public GameVM()
        {
            gameData = new GameData(Helper.InitGameBoard(), 12, 12, EPlayerType.red, ECellState.none);
            BoardVM boardVMInstance = new BoardVM(this); // Pass reference to GameVM
            boardVMInstance.GameDataUpdated += OnBoardVMGameDataUpdated;
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

        public delegate void SwitchToBoard();
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
                    switchToBoardCommand = new RelayPagesCommand(o => true, o => { OnSwitchToBoard(); });
                }
                return switchToBoardCommand;
            }
        }

        private ICommand buttonSaveGameCommand;

        public ICommand SaveGameCommand
        {
            get
            {
                if(buttonSaveGameCommand == null)
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
                if(buttonOpenGameCommand == null)
                {
                    buttonOpenGameCommand = new RelayPagesCommand(o => true, o => { OnOpenGame(); });
                }
                return buttonOpenGameCommand;
            }
        }

        // METHODS

        private void OnBoardVMGameDataUpdated(GameData updatedGameData)
        {
            // Update GameData property in GameVM with the new data from BoardVM
            GameData = updatedGameData;
        }

        private void OnSaveGame()
        {
            string jsonData = GameData.SerializeToJson();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";
            if (saveFileDialog.ShowDialog() == true)
            {
                // Get the selected file path
                string filePath = saveFileDialog.FileName;

                try
                {
                    // Write JSON data to the selected file
                    File.WriteAllText(filePath, jsonData);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during file write
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
                // Get the selected file path
                string filePath = openFileDialog.FileName;

                try
                {
                    // Read JSON data from the selected file
                    string jsonData = File.ReadAllText(filePath);

                    // Deserialize JSON data into GameData object
                    GameData openedGameData = JsonSerializer.Deserialize<GameData>(jsonData);

                    // Update GameData property in GameVM with openedGameData
                    GameData = openedGameData;
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
