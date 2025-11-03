using UnityEngine;
using UnityEngine.UI;

public class SoundToggleButton : MonoBehaviour
{
    public bool isBGMusic; 
    public Image iconImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private bool isOn;

    private void Start()
    {
        if (isBGMusic)
            isOn = PlayerPrefs.GetInt("BGMusicOn", 1) == 1;
        else
            isOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;

        UpdateIcon();
    }

    public void OnButtonClick()
    {
        isOn = !isOn;
        UpdateIcon();

        if (isBGMusic)
            AudioManager.Instance.ToggleBGMusic(isOn);
        else
            AudioManager.Instance.ToggleSFX(isOn);
    }

    private void UpdateIcon()
    {
        iconImage.sprite = isOn ? soundOnSprite : soundOffSprite;
    }
}

