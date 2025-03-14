namespace HeistBox.Data
{
    public enum GameState
    {
        Lobby = 0,
        PlayerJoin = 1, //Player only state
        AnswerSubmission = 2,
        Voting = 3,
        Results = 4
    }
}
