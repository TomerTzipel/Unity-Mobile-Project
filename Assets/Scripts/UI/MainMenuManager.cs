using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Button controlModeButton;
    [SerializeField] Button difficultyButton;

    [SerializeField] Sprite offMode;
    [SerializeField] Sprite onMode;

    [SerializeField] AudioSource sfxClick;
    [SerializeField] AudioSource sfxSuccess;
    [SerializeField] AudioSource sfxFailure;

    private const string controlsModeDefault = "Touch";
    private const string difficultyModeDefault = "Easy";
    private const int PlayerBestScore = 0;

    public const string controlModeKey = "PlayerControlMode";
    public const string difficultyModeKey = "EasyMode";
    public const string PlayerBestScoreKey = "PlayerBestScore";

    void Start()
    {
        CheckPlayerData();
        UpdateUI();

        Debug.Log(PlayerPrefs.GetString(controlModeKey)); 
        Debug.Log(PlayerPrefs.GetString(difficultyModeKey));
    }

    void CheckPlayerData()
    {
        if (!PlayerPrefs.HasKey(controlModeKey))
        {
            PlayerPrefs.SetString(controlModeKey, controlsModeDefault);
            PlayerPrefs.SetString(difficultyModeKey, difficultyModeDefault);
            PlayerPrefs.SetInt(PlayerBestScoreKey, PlayerBestScore);

            PlayerPrefs.Save();
            Debug.Log("No existing PlayerPrefs for Controls. Initialized with default value.");
        }
        else
        {
            Debug.Log("PlayerPrefs for player Controls exists. Loaded existing value.");
        }
    }

    void UpdateUI()
    {
       if (PlayerPrefs.GetString(controlModeKey) == "Touch")
        {
            controlModeButton.image.sprite = onMode;
        }
       else
        {
            controlModeButton.image.sprite = offMode;
        }

       if (PlayerPrefs.GetString(difficultyModeKey) == "Easy")
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
        if (PlayerPrefs.GetString(controlModeKey) == "Touch")
        {
            PlayerPrefs.SetString(controlModeKey, "Buttons");
        }
        else
        {
            PlayerPrefs.SetString(controlModeKey, "Touch");
        }

        PlayerPrefs.Save();

        UpdateUI();
    }

    public void DifficultyModePressed()
    {
        if (PlayerPrefs.GetString(difficultyModeKey) == "Easy")
        {
            PlayerPrefs.SetString(difficultyModeKey, "Hard");
        }
        else
        {
            PlayerPrefs.SetString(difficultyModeKey, "Easy");
        }

        PlayerPrefs.Save();

        UpdateUI();
    }

    public void StartGame(GameObject buttonPressed)
    {
        string levelName = buttonPressed.name;


        if (LevelData.levelThresholds.ContainsKey(levelName))
        {
            int levelScoreThreshold = LevelData.levelThresholds[levelName];

            if (PlayerPrefs.GetInt("PlayerBestScore") >= levelScoreThreshold)
            {
                StaticData.checkPointScoreValueToKeep = levelScoreThreshold;
                
                SceneManager.LoadScene("GameScene");

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
}
