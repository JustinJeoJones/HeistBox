using HeistBox.Data;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace HeistBox.Hubs
{
    public class GameHub: Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task CreateGame(string roomId)
        {
            
            GameData newGame = new GameData(roomId);
            GameData.AddGame(newGame);
            Console.WriteLine(newGame);
            await Clients.All.SendAsync("GameCreated", newGame);
        }

        public async Task JoinGame(string user, string roomId)
        {
            GameData result = GameData.GetGameById(roomId);
            result.players.Add(new PlayerData() { Name = user});
            await Clients.All.SendAsync("PlayerJoined", result);
        }

        public async Task StartGame(string roomId)
        { 
            GameData newGame = GameData.GetGameById(roomId);
            newGame.started = true;
            await Clients.All.SendAsync("GameStarted", newGame);
        }

        public async Task EndAnswerSubmission(string roomId)
        {
            GameData newGame = GameData.GetGameById(roomId);
            newGame.started = true;
            await Clients.All.SendAsync("GameStarted", newGame);
        }
    }
}
