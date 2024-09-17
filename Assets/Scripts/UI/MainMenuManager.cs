using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] TMP_Text bestScorePortrait;
    [SerializeField] TMP_Text bestScoreLandscape;

    [SerializeField] Button controlModeButtonPortrait;
    [SerializeField] Button muteButtonPortrait; 
    [SerializeField] Button controlModeButtonLandscape;
    [SerializeField] Button muteButtonLandscape;

    [SerializeField] Sprite offMode;
    [SerializeField] Sprite onMode;

    void Awake()
    {
        PlayerPrefsManager.InitializePlayerPrefs();
        UpdateUI();
    }

    void UpdateUI()
    {
        string text = $"Best Score: {PlayerPrefsManager.GetPlayerBestScore()}";
        bestScorePortrait.text = text ;
        bestScoreLandscape.text = text ;

        if (PlayerPrefsManager.GetControlMode() == PlayerPrefsManager.controlModeTouchValue)
        {
            controlModeButtonPortrait.image.sprite = onMode;
            controlModeButtonLandscape.image.sprite = onMode;
        }
        else
        {
            controlModeButtonPortrait.image.sprite = offMode;
            controlModeButtonLandscape.image.sprite = offMode;
        }

        if (PlayerPrefsManager.GetSoundMode() == PlayerPrefsManager.soundOnValue)
        {
            muteButtonPortrait.image.sprite = offMode;
            muteButtonLandscape.image.sprite = offMode;
            AudioListener.volume = 1.0f;
        }
        else
        {
            AudioListener.volume = 0f;
            muteButtonPortrait.image.sprite = onMode;
            muteButtonLandscape.image.sprite = onMode;
        }
    }

    public void ControlModePressed()
    {
        if (PlayerPrefsManager.GetControlMode() == PlayerPrefsManager.controlModeTouchValue)
        {
            PlayerPrefsManager.SetControlMode(PlayerPrefsManager.controlModeButtonsValue);
        }
        else
        {
            PlayerPrefsManager.SetControlMode(PlayerPrefsManager.controlModeTouchValue);
        }

        UpdateUI();
    }

    public void MutePressed()
    {
        if (PlayerPrefsManager.GetSoundMode() == PlayerPrefsManager.soundOnValue)
        {
            AudioListener.volume = 0f;
            PlayerPrefsManager.SetSoundMode(PlayerPrefsManager.soundOffValue);
        }
        else
        {
            AudioListener.volume = 1.0f;
            PlayerPrefsManager.SetSoundMode(PlayerPrefsManager.soundOnValue);
        }

        UpdateUI();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
