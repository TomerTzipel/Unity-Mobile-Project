using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Screen
{
    Game, Pause, GameOver, MainMenu, LevelSelect, Settings, Dailies, Profile 
}

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonsPanels;
    [SerializeField] private GameObject GameScreenPanels;
    [SerializeField] private GameObject PauseScreenPanels;
    [SerializeField] private GameObject GameOverScreenPanels;

    [SerializeField] protected GameObject[] landscapePanels;
    [SerializeField] protected GameObject[] portraitPanels;

    protected Screen _currentScreen;

    protected DeviceOrientation _orientation;


    protected virtual void Awake()
    {
        _currentScreen = Screen.Game;
        OnOrientationChange();
    }

    private void Update()
    {
        if (_orientation != Input.deviceOrientation) OnOrientationChange();
    }

    public void LoadPauseScreen()
    {
        ChangeScreen(Screen.Pause);
    }
    public void LoadGameScreen()
    {
        ChangeScreen(Screen.Game);
    }
    public void LoadGameOverScreen()
    {
        ChangeScreen(Screen.GameOver);
    }

    protected virtual void ChangeScreen(Screen screen)
    {
        switch (_currentScreen)
        {
            case Screen.Game:
                GameScreenPanels.SetActive(false);
                break;
            case Screen.Pause:
                PauseScreenPanels.SetActive(false);
                break;
            case Screen.GameOver:
                GameOverScreenPanels.SetActive(false);
                break;
        }

        switch (screen)
        {
            case Screen.Game:
                GameScreenPanels.SetActive(true);
                break;
            case Screen.Pause:
                PauseScreenPanels.SetActive(true);
                break;
            case Screen.GameOver:
                GameOverScreenPanels.SetActive(true);
                break;
        }

        _currentScreen = screen;
    }

    public void TurnButtonsOn()
    {
        buttonsPanels.SetActive(true);
    }
    public void TurnButtonsOff()
    {
        buttonsPanels.SetActive(false);
    }

    protected void OnOrientationChange()
    {
        _orientation = Input.deviceOrientation;

        if(_orientation == DeviceOrientation.Portrait || _orientation == DeviceOrientation.PortraitUpsideDown)
        {
            foreach (GameObject panel in landscapePanels) 
            {
                panel.SetActive(false);
            }
            foreach (GameObject panel in portraitPanels)
            {
                panel.SetActive(true);
            }
            return;
        }
        foreach (GameObject panel in landscapePanels)
        {
            panel.SetActive(true);
        }
        foreach (GameObject panel in portraitPanels)
        {
            panel.SetActive(false);
        }
    }
}
