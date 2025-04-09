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
        [JsonPropertyName("responses")]
        public List<Response> responses { get; set; } = new List<Response>();


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

        public static List<Response> SetupPlayerRoles(List<PlayerData> users)
        {
            var result = new List<Response>();
            var random = new Random();

            // Shuffle the user list
            var shuffledUsers = users.OrderBy(u => random.Next()).ToList();

            int i = 0;
            while (i < shuffledUsers.Count)
            {
                int remaining = shuffledUsers.Count - i;
                int groupSize = (remaining == 3) ? 3 : Math.Min(2, remaining);

                // Assign a random role to the group
                Role role = Role.GetRandomRole();

                for (int j = 0; j < groupSize; j++)
                {
                    result.Add(new Response
                    {
                        answer = string.Empty,
                        player = shuffledUsers[i + j],
                        role = role,
                        wasChosen = false
                    });
                }

                i += groupSize;
            }

            return result;
        }

        public static Dictionary<string, List<Response>> GroupResponsesByRoleName(List<Response> responses)
        {
            return responses
                .GroupBy(r => r.role.Name)
                .ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}
