using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    class Cell : BaseNotification
    {
        private int x;
        private int y;
        private string displayedImage;
        private string hiddenImage;

        public Cell(int x, int y, string hidden, string displayed)
        {
            this.X = x;
            this.Y = y;
            this.HiddenImage = hidden;
            this.DisplayedImage = displayed;
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
    }
}
