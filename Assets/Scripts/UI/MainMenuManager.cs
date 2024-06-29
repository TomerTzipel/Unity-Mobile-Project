using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI controlModeUiText;

    private const string controlsModeDefault = "Touch";
    private const string difficultyModeDefault = "Easy";

    private string controlModeKey = "PlayerControlMode";
    private string difficultyModeKey = "HardMode";

    void Start()
    {
        CheckPlayerData();
        UpdateUI();
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

    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
