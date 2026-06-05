public struct GamePausedEvent
{
    public bool IsPaused;
}

public struct PauseRequestedEvent { }

public struct SceneLoadRequestedEvent
{
    public string SceneName;
}