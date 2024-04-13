using System;

namespace Checkers.Services
{
    public class GameStatistics
    {
        private int whiteWins;
        private int redWins;
        private int maxPiecesLeft;
        private JsonHandler jsonHandler;

        public GameStatistics()
        {
            whiteWins = 0;
            redWins = 0;
            maxPiecesLeft = 0;
            jsonHandler = new JsonHandler();
        }
        public int WhiteWins
        {
            get { return whiteWins; }
            set { whiteWins = value; }
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

        public void OnLoadStatistics()
        {
            string filePath = "../../../Resources/Statistics.json";

            try
            {
                // Load statistics from JSON file using JsonHandler
                GameStatistics loadedGameStatistics = jsonHandler.LoadFromJson<GameStatistics>(filePath);

                this.whiteWins = loadedGameStatistics.whiteWins;
                this.redWins = loadedGameStatistics.redWins;
                this.maxPiecesLeft = loadedGameStatistics.maxPiecesLeft;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file: {ex.Message}");
                // You can add more sophisticated error handling as needed
            }
        }

        public void OnSaveStatistics()
        {
            string filePath = "../../../Resources/Statistics.json";

            try
            {
                // Save statistics to JSON file using JsonHandler
                jsonHandler.SaveToJson<GameStatistics>(filePath, this);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
                // You can add more sophisticated error handling as needed
            }
        }
    }
}
