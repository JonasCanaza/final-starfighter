using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    public bool IsMusicPaused { get; private set; }

    [Header("Sources Settings")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips Settings")]
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip gameplayMusic;

    protected override void Awake()
    {
        base.Awake();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip != clip)
        {
            musicSource.clip = clip;
        }

        musicSource.enabled = true;
        musicSource.loop = true;

        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }

        IsMusicPaused = false;
    }

    public void StopMusic()
    {
        musicSource.Stop();
        IsMusicPaused = false;
    }

    public void PauseMusic()
    {
        if (!IsMusicPaused)
        {
            musicSource.Pause();
            IsMusicPaused = true;
        }
    }

    public void ResumeMusic()
    {
        if (IsMusicPaused)
        {
            musicSource.UnPause();
            IsMusicPaused = false;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case SceneNames.MainMenu:

                PlayMusic(mainMenuMusic);

                break;
            case SceneNames.Gameplay:

                PlayMusic(gameplayMusic);

                break;
        }
    }
}