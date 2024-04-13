using Checkers.Services;
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
        private int whiteWins;
        private int redWins;
        private int maxPiecesLeft;

        public StatisticsVM(GameStatistics statistics)
        {
            this.whiteWins = statistics.WhiteWins;
            this.redWins = statistics.RedWins;
            this.maxPiecesLeft = statistics.MaxPiecesLeft;
        }

        public int WhiteWins
        {
            get { return whiteWins; }
            set { whiteWins = value; OnPropertyChanged("WhiteWins"); }
        }

        public int  RedWins
        {
            get { return redWins; }
            set { redWins = value; OnPropertyChanged("WhiteWins"); }
        }

        public int MaxPiecesLeft
        {
            get { return maxPiecesLeft; }
            set { maxPiecesLeft = value; OnPropertyChanged("MaxPiecesLeft"); }
        }

        // COMMANDS

        private ICommand switchToGameCommand;
        public ICommand SwitchToGameCommand
        {
            get
            {
                if (switchToGameCommand == null)
                {
                    switchToGameCommand = new RelayPagesCommand(o => true, o => { OnSwitchToGame(null); });
                }

                return switchToGameCommand;
            }
        }

        // DELEGATES

        public delegate void SwitchToGame(GameStatistics? s);
        public SwitchToGame OnSwitchToGame { get; set; }
    }
}
