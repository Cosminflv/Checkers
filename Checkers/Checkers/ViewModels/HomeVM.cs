    using System.Windows.Input;

    namespace Checkers.ViewModels
    {
         class HomeVM : BaseVM
        {
            private ICommand switchToGameCommand;

            public ICommand SwitchToGameCommand
            {
                get
                {
                    if(switchToGameCommand == null)
                    {
                        switchToGameCommand = new RelayCommand(o => true, o => { OnSwitchToGame(); });
                    }

                    return switchToGameCommand;
                }
            }

            private ICommand switchToAboutCommand;

            public ICommand SwitchToAboutCommand
            {
                get
                {
                    if(switchToAboutCommand == null)
                    {
                        switchToAboutCommand = new RelayCommand(o => true, o => { OnSwitchToAbout(); });
                    }

                    return switchToAboutCommand;
                }
            }

            //DELEGATES

            public delegate void SwitchToGame();
            public SwitchToGame OnSwitchToGame { get; set; }

            public delegate void SwitchToAbout();
            public SwitchToAbout OnSwitchToAbout { get; set; }
        }
    }
