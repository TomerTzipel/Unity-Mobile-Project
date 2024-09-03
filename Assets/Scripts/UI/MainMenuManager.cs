using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI controlModeUiText;
    [SerializeField] TextMeshProUGUI difficultyButtonText;
    [SerializeField] Button difficultyButton;

    private const string controlsModeDefault = "Touch";
    private const string difficultyModeDefault = "Easy";

    public const string controlModeKey = "PlayerControlMode";
    public const string difficultyModeKey = "EasyMode";

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
            controlModeUiText.text = "Touch";
        }
       else
        {
            controlModeUiText.text = "Buttons";
        }

       if (PlayerPrefs.GetString(difficultyModeKey) == "Easy")
        {
            difficultyButtonText.text = "Easy";
        }
        else
        {
            difficultyButtonText.text = "HARD!";
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

    public void StartGame(string sceneName)
    {
        //SceneManager.LoadScene(sceneName);
    }

    
}
