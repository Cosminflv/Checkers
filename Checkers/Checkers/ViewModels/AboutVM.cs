using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Checkers.ViewModels
{
    class AboutVM : BaseVM
    {

        // DELEGATES
        public delegate void SwitchToHome();

        public SwitchToHome OnSwitchToHome { get; set; }

        // COMMANDS

        private ICommand switchToHomeCommand;

        public ICommand SwitchToHomeCommand
        {
            get
            {
                if(switchToHomeCommand == null)
                {
                    switchToHomeCommand = new RelayCommand(o => true, o => { OnSwitchToHome(); });
                }
                return switchToHomeCommand;
            }
        }
    }
}
