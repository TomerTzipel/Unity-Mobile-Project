using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MapSettings SO_MapSettings;
    [SerializeField] private GameObject firstLastTile;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerSwipeMovement playerSwipe;

    [SerializeField] private TimerManager TimerManager;

    public GameObject FirstLastTile
    {
        get { return firstLastTile; }
    }
    public GameObject LastTile
    {
        get { return SO_MapSettings.LastTile; }
    }

    private void Awake()
    {
        SO_MapSettings.LastTile = firstLastTile;
        CheckSettings();
        TimerManager.StartTimer();
    }

    private void CheckSettings()
    {
        if (PlayerPrefsManager.GetControlMode() == "Touch")
        {
            uiManager.TurnButtonsOff(); 
            playerSwipe.enabled = true;
        }
        else
        {
            uiManager.TurnButtonsOn();
            playerSwipe.enabled = false;
        }

        /*if (PlayerPrefs.GetString(PlayerPrefsManager.GetDifficultyMode()) == "Easy")
        {
            SO_MapSettings.speedMultiplier = 1f;
        }
        else
        {
            SO_MapSettings.speedMultiplier = 1.5f;
        }*/
    }

    public void GameOver()
    {
        PauseGame();
        scoreManager.UpdateBestScore();
        uiManager.LoadGameOverScreen();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

}
