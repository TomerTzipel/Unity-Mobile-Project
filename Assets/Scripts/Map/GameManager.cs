using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour,ISaveable
{
    [SerializeField] private TileHandler[] tiles;

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

        if (PlayerPrefsManager.GetSoundMode() == PlayerPrefsManager.soundOnValue)
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
        TimerManager.RecordTime();
        PlayerPrefsManager.SetSaveState(false);
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

    public void LoadData(GameData data)
    {
        SO_MapSettings.LastTile = tiles[data.MapData.LastTileIndex];
    }

    public void SaveData(ref GameData data)
    {
        data.MapData.LastTileIndex = SO_MapSettings.LastTile.DataIndex;
    }
}
