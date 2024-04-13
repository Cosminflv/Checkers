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
            GameStatistics.OnLoadStatistics();
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
            if (loadedGameData != null)
            {
                GameViewModel.GameData = loadedGameData;
            }
            else
            {
                GameViewModel.GameData = new GameData(GameViewModel.AllowMultipleJump);
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
    }
}
