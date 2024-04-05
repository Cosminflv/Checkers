﻿using System.Windows.Input;

namespace Checkers.ViewModels
{
    class GameVM : BaseVM
    {
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
                    switchToHomeCommand = new RelayCommand(o => true, o => { OnSwitchToHome(); });
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
                    switchToStatisticsCommand = new RelayCommand(o => true, o => { OnSwitchToStatistics(); });
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
                    switchToBoardCommand = new RelayCommand(o => true, o => { OnSwitchToBoard(); });
                }
                return switchToBoardCommand;
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

        // METHODS

        private void OnSaveGame()
        {
            // TO DO: IMPLEMENT
            return;
        }
    }
}