public class GameStateManager 
{
    private static GameStateManager instance;

    public static GameStateManager Instance
    {
        get
        {
            if(instance == null)
                instance = new GameStateManager();
            
            return instance;
        }
    }

    public GameState CurrentState { get; private set; }
    public delegate void GameStateChangeHandler(GameState newGmeState);
    public event GameStateChangeHandler onGameStateChanged;

    private GameStateManager()
    {

    }

    public void SetState(GameState newGameState)
    {
        if(newGameState == CurrentState)
            return;
        
        CurrentState = newGameState;
        onGameStateChanged?.Invoke(newGameState);
    }
}

/*
{
    GameState currentState = GameState.Instance.CurrentState;
    GameState newState = currentGameState == GameState.Gameplay
        ? GameState.Paused
        : GameState.GamePlay;
}


*/