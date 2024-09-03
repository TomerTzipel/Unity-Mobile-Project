using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Button controlModeButton;
    [SerializeField] Button difficultyButton;

    [SerializeField] Sprite offMode;
    [SerializeField] Sprite onMode;

    [SerializeField] AudioSource sfxClick;
    [SerializeField] AudioSource sfxSuccess;
    [SerializeField] AudioSource sfxFailure;

    [SerializeField] Image TransitionUi;

    public float sceneLoadTime;

    void Start()
    {
        PlayerPrefsManager.InitializePlayerPrefs();
        UpdateUI();
    }

    void UpdateUI()
    {
        if (PlayerPrefsManager.GetControlMode() == "Touch")
        {
            controlModeButton.image.sprite = onMode;
        }
        else
        {
            controlModeButton.image.sprite = offMode;
        }

        if (PlayerPrefsManager.GetDifficultyMode() == "Easy")
        {
            difficultyButton.image.sprite = offMode;
        }
        else
        {
            difficultyButton.image.sprite = onMode;
        }
    }

    public void ControlModePressed()
    {
        if (PlayerPrefsManager.GetControlMode() == "Touch")
        {
            PlayerPrefsManager.SetControlMode("Buttons");
        }
        else
        {
            PlayerPrefsManager.SetControlMode("Touch");
        }

        UpdateUI();
    }

    public void DifficultyModePressed()
    {
        if (PlayerPrefsManager.GetDifficultyMode() == "Easy")
        {
            PlayerPrefsManager.SetDifficultyMode("Hard");
        }
        else
        {
            PlayerPrefsManager.SetDifficultyMode("Easy");
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

                TransitionUi.GetComponent<TransitionUI>().TransitionStart();

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
