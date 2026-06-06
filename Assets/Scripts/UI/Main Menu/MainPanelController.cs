using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPanelController : MonoBehaviour
{
    [Header("Button References")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(HandlePlayClicked);
        exitButton.onClick.AddListener(HandleExitClicked);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(HandlePlayClicked);
        exitButton.onClick.RemoveListener(HandleExitClicked);
    }

    private void HandlePlayClicked()
    {
        SceneManager.LoadScene(SceneNames.Gameplay);
    }

    private void HandleExitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}