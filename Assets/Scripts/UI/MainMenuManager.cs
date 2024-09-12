using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Button controlModeButtonPortrait;
    [SerializeField] Button muteButtonPortrait; 
    [SerializeField] Button controlModeButtonLandscape;
    [SerializeField] Button muteButtonLandscape;

    [SerializeField] Sprite offMode;
    [SerializeField] Sprite onMode;

    [SerializeField] AudioSource sfxClick;
    [SerializeField] AudioSource sfxSuccess;
    [SerializeField] AudioSource sfxFailure;

    [SerializeField] TransitionUI TransitionUi;

    [SerializeField] AudioListener audioListener;

    public float sceneLoadTime;

    void Awake()
    {
        PlayerPrefsManager.InitializePlayerPrefs();
        UpdateUI();
    }

    void UpdateUI()
    {
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
            audioListener.enabled = true;
        }
        else
        {
            audioListener.enabled = false;
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
            audioListener.enabled = false;
            PlayerPrefsManager.SetSoundMode(PlayerPrefsManager.soundOffValue);
        }
        else
        {
            audioListener.enabled = true;
            PlayerPrefsManager.SetSoundMode(PlayerPrefsManager.soundOnValue);
        }

        UpdateUI();
    }

    public void StartGame(GameObject buttonPressed)
    {
        string levelName = buttonPressed.name;

        if (LevelData.levelThresholds.ContainsKey(levelName))
        {
            int levelScoreThreshold = LevelData.levelThresholds[levelName];

            if (PlayerPrefsManager.GetPlayerBestScore() >= levelScoreThreshold)
            {
                StaticData.checkPointScoreValueToKeep = levelScoreThreshold;

                TransitionUi.TransitionStart();

                StartCoroutine(LoadSceneAfterDelay("GameScene", sceneLoadTime));

                sfxSuccess.Play();
            }
            else
            {
                sfxFailure.Play();
            }
        }
        else
        {
            sfxFailure.Play();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
