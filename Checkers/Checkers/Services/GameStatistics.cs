using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Checkers.Services
{
    public class GameStatistics
    {
        private int whiteWins;
        private int redWins;
        private int maxPiecesLeft;

        public GameStatistics() 
        {
            whiteWins = 0;
            redWins = 0; 
            maxPiecesLeft = 0;
        }

        public GameStatistics(int whiteWins, int redWins, int maxPiecesLeft)
        {
            this.whiteWins = whiteWins;
            this.redWins = redWins;
            this.maxPiecesLeft = maxPiecesLeft;
        }

        public int WhiteWins
        {
            get { return whiteWins; }
            set { whiteWins = value;}
        }

        public int RedWins
        {
            get { return redWins; }
            set { redWins = value; }
        }

        public int MaxPiecesLeft
        {
            get { return maxPiecesLeft; }
            set { maxPiecesLeft = value; }
        }

        // Method to serialize the GameData object to JSON string
        public string SerializeToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        // Method to deserialize JSON string to GameData object
        public static GameStatistics DeserializeFromJson(string jsonString)
        {
            return JsonSerializer.Deserialize<GameStatistics>(jsonString);
        }
    }
}
