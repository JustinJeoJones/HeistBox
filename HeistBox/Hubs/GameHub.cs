﻿using HeistBox.Data;
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
            newGame.gameState = GameState.AnswerSubmission;
            newGame.responses = GameData.SetupPlayerRoles(newGame.players);
            await Clients.All.SendAsync("GameStarted", newGame);
        }
        public async Task SendAnswer(string roomId, Response submittedResponse)
        {
            GameData newGame = GameData.GetGameById(roomId);
            newGame.responses.FirstOrDefault(r => r.player.Name == submittedResponse.player.Name).answer = submittedResponse.answer;

            await Clients.All.SendAsync("GotAnswer", newGame);
        }

        public async Task EndAnswerSubmission(string roomId)
        {
            GameData newGame = GameData.GetGameById(roomId);
            newGame.gameState = GameState.Voting;
            await Clients.All.SendAsync("StartVoting", newGame);
        }
    }
}
