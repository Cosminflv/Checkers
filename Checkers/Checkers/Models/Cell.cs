using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        // Properties
        public int X { get; set; }
        public int Y { get; set; }
        public string DisplayedImage { get; set; }
        public string HiddenImage { get; set; }
        public ECellState CellState { get; set; }
        public bool IsKing { get; set; }

        // Deserialization constructor
        [JsonConstructor]
        public Cell(int x, int y, string hiddenImage, string displayedImage, ECellState cellState, bool isKing)
        {
            X = x;
            Y = y;
            HiddenImage = hiddenImage;
            DisplayedImage = displayedImage;
            CellState = cellState;
            IsKing = isKing;
        }

    }
}
