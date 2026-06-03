using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPanelController : MonoBehaviour
{
    [Header("Button References")]
    [SerializeField] private Button playButton;

    private void Awake()
    {
        playButton.onClick.AddListener(HandlePlayClicked);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(HandlePlayClicked);
    }

    private void HandlePlayClicked()
    {
        SceneManager.LoadScene(SceneNames.Gameplay);
    }
}