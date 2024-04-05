using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Checkers.ViewModels
{
    class GameVM : BaseVM
    {
        // COMMANDS

        private ICommand switchToHomeCommand;
        public ICommand SwitchToHomeCommand
        {
            get
            {
                if (switchToHomeCommand == null)
                {
                    switchToHomeCommand = new RelayCommand(o => true, o => { OnSwitchToHome(); });
                }

                return switchToHomeCommand;
            }
        }

        private ICommand buttonSaveGameCommand;

        public ICommand ButtonSaveGameCommand
        {
            get
            {
                if(buttonSaveGameCommand == null)
                {
                    buttonSaveGameCommand = new RelayCommand(o => true, o => { OnSaveGame(); });
                }
            return buttonSaveGameCommand;
            }
        }

        // DELEGATES

        public delegate void SwitchToHome();
        public SwitchToHome OnSwitchToHome {  get; set; }

        // METHODS

        private void OnSaveGame()
        {
            // TO DO: IMPLEMENT
            return;
        }

    }
}
