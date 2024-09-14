using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MapSettings SO_MapSettings;
    [SerializeField] private TileHandler firstLastTile;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerSwipeMovement playerSwipe;

    [SerializeField] private TimerManager TimerManager;
    [SerializeField] private AudioListener audioListener;

    public TileHandler FirstLastTile
    {
        get { return firstLastTile; }
    }
    public TileHandler LastTile
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

        if (PlayerPrefs.GetString(PlayerPrefsManager.GetSoundMode()) == PlayerPrefsManager.soundOnValue)
        {
            audioListener.enabled = true;
        }
        else
        {
            audioListener.enabled = false;
        }
    }

    public void GameOver()
    {
        PauseGame();
        scoreManager.UpdateBestScore();
        uiManager.LoadGameOverScreen();
    }

    public void GoToMainMenu()
    {
        ResumeGame();
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
