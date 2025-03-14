using System.Text.Json.Serialization;

namespace HeistBox.Data
{
    [Serializable]
    public class GameData
    {
        [JsonPropertyName("id")]
        public string id { get; set; }
        [JsonPropertyName("players")]
        public List<PlayerData> players { get; set; } = new List<PlayerData>();
        [JsonPropertyName("heistLocation")]
        public string heistLocation { get; set; } = HeistData.GetRandomLocation();
        [JsonPropertyName("gameState")]
        public GameState gameState { get; set; } = GameState.Lobby;


        public static List<GameData> activeGames = new List<GameData>();

        public GameData() { } // Default constructor needed for serialization

        public GameData(string roomid)
        {
            id = roomid;
        }

        public static void RemoveGame(GameData game)
        {
            activeGames.Remove(game);
        }

        public static void AddGame(GameData game)
        {
            activeGames.Add(game);
        }

        public static string GenerateId()
        {
            Random random = new Random();
            string result = "";
            while (activeGames.Any(g => g.id == result) || result == "")
            {
                char[] letters = new char[4];

                for (int i = 0; i < 4; i++)
                {
                    letters[i] = (char)random.Next('A', 'Z' + 1);
                }
                result = new string(letters);
            }

            return result;
        }

        public static GameData GetGameById(string id)
        {
            return activeGames.FirstOrDefault(g => g.id.ToLower().Trim() == id.ToLower().Trim());
        }
    }
}
