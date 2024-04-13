using Checkers.Services;
using System.Windows.Input;

    namespace Checkers.ViewModels
    {
         class HomeVM : BaseVM
        {

            // COMMANDS

            private ICommand switchToGameCommand;

            public ICommand SwitchToGameCommand
            {
                get
                {
                    if(switchToGameCommand == null)
                    {
                        switchToGameCommand = new RelayPagesCommand(o => true, o => { OnSwitchToGame(null); });
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
                        switchToAboutCommand = new RelayPagesCommand(o => true, o => { OnSwitchToAbout(); });
                    }

                    return switchToAboutCommand;
                }
            }

            //DELEGATES

            public delegate void SwitchToGame(GameStatistics? s);
            public SwitchToGame OnSwitchToGame { get; set; }

            public delegate void SwitchToAbout();
            public SwitchToAbout OnSwitchToAbout { get; set; }
        }
    }
