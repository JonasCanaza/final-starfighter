using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    private enum GameState
    {
        MainMenu,
        Playing,
        Paused,
    }

    private GameState currentGameState;

    protected override void Awake()
    {
        base.Awake();

        currentGameState = GameState.Playing;
    }

    private void OnEnable() => EventBus.Subscribe<PauseRequestedEvent>(OnPauseRequested);

    private void OnDisable() => EventBus.Unsubscribe<PauseRequestedEvent>(OnPauseRequested);

    private void OnPauseRequested(PauseRequestedEvent pauseRequestedEvent)
    {
        GameState newState = currentGameState == GameState.Paused ? GameState.Playing : GameState.Paused;
        SetGameState(newState);
        Time.timeScale = newState == GameState.Paused ? 0.0f : 1.0f;
    }

    private void SetGameState(GameState newGameState)
    {
        if (currentGameState == newGameState)
        {
            return;
        }

        currentGameState = newGameState;

        if (newGameState == GameState.Playing || newGameState == GameState.Paused)
        {
            EventBus.Publish(new GamePausedEvent
            {
                IsPaused = newGameState == GameState.Paused
            });
        }
    }
}