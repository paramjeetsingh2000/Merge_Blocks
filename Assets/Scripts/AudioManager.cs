
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgMusicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip bgMusicClip;
    public AudioClip mergeClip;

    private bool sfxEnabled = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Apply saved sound states
        bool bgOn = PlayerPrefs.GetInt("BGMusicOn", 1) == 1;
        bool sfxOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;

        ToggleBGMusic(bgOn);
        ToggleSFX(sfxOn);
    }

    private void Start()
    {
        if (bgMusicSource != null && bgMusicClip != null)
        {
            bgMusicSource.clip = bgMusicClip;
            bgMusicSource.loop = true;
            if (PlayerPrefs.GetInt("BGMusicOn", 1) == 1)
                bgMusicSource.Play();
        }
    }

    public void PlayMergeSound()
    {
        if (sfxEnabled && mergeClip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(mergeClip);
        }
    }

    public void ToggleBGMusic(bool isOn)
    {
        PlayerPrefs.SetInt("BGMusicOn", isOn ? 1 : 0);
        if (bgMusicSource == null) return;

        if (isOn)
        {
            if (!bgMusicSource.isPlaying)
                bgMusicSource.Play();
        }
        else
        {
            bgMusicSource.Pause();
        }
    }

    public void ToggleSFX(bool isOn)
    {
        PlayerPrefs.SetInt("SFXOn", isOn ? 1 : 0);
        sfxEnabled = isOn;
    }
}
