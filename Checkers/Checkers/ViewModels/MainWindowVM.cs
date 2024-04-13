using Checkers.Services;
using Checkers.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Checkers.ViewModels
{
    class MainWindowVM : BaseVM
    {
        private BaseVM selectedVM;

        public BaseVM SelectedVM
        {
            get { return selectedVM; }
            set { selectedVM = value; OnPropertyChanged(); }
        }

        public HomeVM HomeViewModel { get; set; }

        public AboutVM AboutViewModel { get; set; }

        public GameVM GameViewModel { get; set; }

        public BoardVM BoardViewModel { get; set; }

        public StatisticsVM StatisticsViewModel { get; set; }

        public GameStatistics GameStatistics { get; set; }

        public MainWindowVM()
        {
            GameViewModel = new GameVM();
            GameStatistics = new GameStatistics();
            OnLoadStatistics();
            BoardViewModel = new BoardVM(GameViewModel, GameStatistics);
            switchToHome();
        }

        public void switchToAbout()
        {
            AboutViewModel = new AboutVM();
            AboutViewModel.OnSwitchToHome = switchToHome;
            SelectedVM = AboutViewModel;
        }

        public void switchToGame(GameStatistics? statistics)
        {
            if (statistics != null)
            {
                GameViewModel.Statistics = statistics;
            }

            GameViewModel.OnSwitchToHome = switchToHome;
            GameViewModel.OnSwitchToBoard = switchToBoard;
            GameViewModel.OnSwitchToStatistics = switchToStatistics;
            SelectedVM = GameViewModel;
        }

        public void switchToHome()
        {
            HomeViewModel = new HomeVM();
            HomeViewModel.OnSwitchToAbout = switchToAbout;
            HomeViewModel.OnSwitchToGame = switchToGame;
            SelectedVM = HomeViewModel;
        }

        public void switchToBoard(GameData? loadedGameData)
        {
            GameViewModel.GameData = new GameData();
            if (loadedGameData != null)
            {
                GameViewModel.GameData = loadedGameData;
             
            }
            BoardViewModel = new BoardVM(GameViewModel, GameStatistics);
            BoardViewModel.OnSwitchToGame = switchToGame;
            SelectedVM = BoardViewModel;
        }

        public void switchToStatistics()
        {
            StatisticsViewModel = new StatisticsVM(GameStatistics);
            StatisticsViewModel.OnSwitchToGame = switchToGame;
            SelectedVM = StatisticsViewModel;
        }

        private void OnLoadStatistics()
        {

            string filePath = "../../../Resources/Statistics.json";

            try
            {
                // Read JSON data from the selected file
                string jsonData = File.ReadAllText(filePath);

                // Deserialize JSON data into GameData object
                GameStatistics loadedGameStatistics = JsonSerializer.Deserialize<GameStatistics>(jsonData);

                GameStatistics = loadedGameStatistics;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening file: {ex.Message}");
                // You can add more sophisticated error handling as needed
            }

        }
    }
}
