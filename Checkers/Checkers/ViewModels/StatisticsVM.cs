using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Checkers.ViewModels
{
    public class StatisticsVM : BaseVM
    {
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

        // DELEGATES

        public delegate void SwitchToGame();
        public SwitchToGame OnSwitchToGame { get; set; }
    }
}
