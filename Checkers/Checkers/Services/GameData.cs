using Checkers.Models;
using Checkers.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Checkers.Services
{
    internal class GameData
    {
        public ObservableCollection<ObservableCollection<Cell>> GameBoard { get; set; }
        public int WhiteRemainingPieces { get; set; }
        public int RedRemainingPieces { get; set; }
        public EPlayerType CurrentTurn { get; set; }
        public ECellState PlayerWon { get; set; }
        public bool AllowMultipleJump {  get; set; }

        public GameData(ObservableCollection<ObservableCollection<Cell>> gameBoard, int whiteRemainingPieces, int redRemainingPieces, EPlayerType currentTurn, ECellState playerWon, bool allowMultipleJump)
        {
            GameBoard = gameBoard;
            WhiteRemainingPieces = whiteRemainingPieces;
            RedRemainingPieces = redRemainingPieces;
            CurrentTurn = currentTurn;
            PlayerWon = playerWon;
            AllowMultipleJump = allowMultipleJump;
        }

        public GameData() 
        {
            GameBoard = Helper.InitGameBoard();
            WhiteRemainingPieces = 12;
            RedRemainingPieces = 12;
            CurrentTurn = EPlayerType.red;
            PlayerWon = ECellState.none;
            AllowMultipleJump = false;
        }   

        // Method to serialize the GameData object to JSON string
        public string SerializeToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        // Method to deserialize JSON string to GameData object
        public static GameData DeserializeFromJson(string jsonString)
        {
            return JsonSerializer.Deserialize<GameData>(jsonString);
        }
    }
}
