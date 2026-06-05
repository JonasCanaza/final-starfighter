using UnityEngine;
using UnityEngine.UI;

public class PausePanelController : UIPanel
{
    [Header("Button References")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button exitButton;

    protected override void Awake()
    {
        base.Awake();

        resumeButton.onClick.AddListener(HandleResumeClicked);
        exitButton.onClick.AddListener(HandleExitClicked);
    }

    private void OnEnable() => EventBus.Subscribe<GamePausedEvent>(OnGamePaused);

    private void OnDisable() => EventBus.Unsubscribe<GamePausedEvent>(OnGamePaused);

    private void OnDestroy()
    {
        resumeButton.onClick.RemoveListener(HandleResumeClicked);
        exitButton.onClick.RemoveListener(HandleExitClicked);
    }

    private void HandleResumeClicked()
    {
        EventBus.Publish(new PauseRequestedEvent());
    }

    private void HandleExitClicked()
    {
        
    }

    private void OnGamePaused(GamePausedEvent gamePausedEvent)
    {
        if (gamePausedEvent.IsPaused)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}