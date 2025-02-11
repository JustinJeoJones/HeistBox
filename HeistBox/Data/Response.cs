namespace HeistBox.Data
{
    public class Response
    {
        public string answer {  get; set; }
        public PlayerData player { get; set; }
        public bool wasChosen { get; set; } = false;
    }
}
