using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public MainWindowVM()
        {
            switchToHome();
        }

        public void switchToAbout()
        {
            AboutViewModel = new AboutVM();
            AboutViewModel.OnSwitchToHome = switchToHome;
            SelectedVM = AboutViewModel;
        }

        public void switchToGame()
        {
            GameViewModel = new GameVM();
            GameViewModel.OnSwitchToHome = switchToHome;
            SelectedVM = GameViewModel;
        }

        private void switchToHome()
        {
            HomeViewModel = new HomeVM();
            HomeViewModel.OnSwitchToAbout = switchToAbout;
            HomeViewModel.OnSwitchToGame = switchToGame;
            SelectedVM = HomeViewModel;
        }
    }
}
