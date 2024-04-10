using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Checkers.Models
{
    enum ECellState
    {
        red,
        white,
        none
    }

    enum EPlayerType
    {
        white,
        red
    }

    class Cell : BaseNotification
    {
        private int x;
        private int y;
        private string displayedImage;
        private string hiddenImage;
        private ECellState cellState;

        public Cell(int x, int y, string hidden, string displayed, ECellState cellstate)
        {
            this.X = x;
            this.Y = y;
            this.HiddenImage = hidden;
            this.DisplayedImage = displayed;
            this.CellState = cellstate;
        }

        public ECellState CellState
        {
            get { return cellState; }
            set { cellState = value; NotifyPropertyChanged("CellState"); }
        }

        public int X
        {
            get { return x; }
            set { x = value; NotifyPropertyChanged("X"); }
        }

        public int Y
        {
            get { return y; }
            set { y = value; NotifyPropertyChanged("Y"); }
        }

        public string DisplayedImage
        {
            get { return displayedImage; }
            set { displayedImage = value; NotifyPropertyChanged("DisplayedImage"); }
        }

        public string HiddenImage
        {
            get { return hiddenImage; }
            set { hiddenImage = value; NotifyPropertyChanged("HiddenImage");}
        }

        public bool isOccupied()
        {
            return cellState != ECellState.none;
        }
    }
}
