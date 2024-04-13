using Checkers.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
        private string SerializeToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public void OnLoadStatistics()
        {
            string filePath = "../../../Resources/Statistics.json";

            try
            {
                // Read JSON data from the selected file
                string jsonData = File.ReadAllText(filePath);

                // Deserialize JSON data into GameData object
                GameStatistics loadedGameStatistics = JsonSerializer.Deserialize<GameStatistics>(jsonData);

                this.whiteWins = loadedGameStatistics.whiteWins;
                this.redWins = loadedGameStatistics.redWins;
                this.maxPiecesLeft = loadedGameStatistics.maxPiecesLeft;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening file: {ex.Message}");
                // You can add more sophisticated error handling as needed
            }
        }

        public void OnSaveStatistics()
        {
            string jsonData = this.SerializeToJson();

            // Get the selected file path
            string filePath = "../../../Resources/Statistics.json";

            try
            {
                // Write JSON data to the selected file
                File.WriteAllText(filePath, jsonData);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during file write
                Console.WriteLine($"Error saving file: {ex.Message}");
                // You can add more sophisticated error handling as needed
            }

        }
    }
}
