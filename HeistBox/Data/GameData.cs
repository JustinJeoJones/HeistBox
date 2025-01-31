namespace HeistBox.Data
{
    public class GameData
    {
        public string id;
        public List<PlayerData> players;
        public string heistLocation;
        public static List<GameData> activeGames = new List<GameData>();

        public GameData(string roomid)
        {
            id = roomid;
            players = new List<PlayerData>();
            heistLocation = HeistData.GetRandomLocation();
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
