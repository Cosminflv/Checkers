using Checkers.Models;
using Checkers.Services;
using Checkers.Commands;
using System.Windows.Input;

namespace Checkers.ViewModels
{
    class CellVM
    {
        GameBusinessLogic bl;
        public CellVM(int x, int y, string hidden, string displayed, GameBusinessLogic bl)
        {
            SimpleCell = new Cell(x, y, hidden, displayed);
            this.bl = bl;
        }
        //am adus celula din Model in VM
        public Cell SimpleCell { get; set; }

        private ICommand clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                if (clickCommand == null)
                {
                    clickCommand = new RelayCommand<Cell>(bl.ClickAction);
                }
                return clickCommand;
            }
        }
    }
}
