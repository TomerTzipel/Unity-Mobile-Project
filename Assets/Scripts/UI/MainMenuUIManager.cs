using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : UIManager
{
    [SerializeField] private GameObject mainMenuPanels;
    [SerializeField] private GameObject levelSelectPanels;
    [SerializeField] private GameObject settingsPanels;
    [SerializeField] private GameObject dailyRewardsPanels;
    [SerializeField] private GameObject playerProfilePanels;

    protected override void Awake()
    {
        _currentScreen = Screen.MainMenu;
        OnOrientationChange();
    }

    protected override void ChangeScreen(Screen screen)
    {
        switch (_currentScreen)
        {
            case Screen.MainMenu:
                mainMenuPanels.SetActive(false);
                break;
            case Screen.LevelSelect:
                levelSelectPanels.SetActive(false);
                break;
            case Screen.Settings:
                settingsPanels.SetActive(false);
                break;
            case Screen.Dailies:
                dailyRewardsPanels.SetActive(false);
                break;
            case Screen.Profile:
                playerProfilePanels.SetActive(false);
                break;
        }

        switch (screen)
        {
            case Screen.MainMenu:
                mainMenuPanels.SetActive(true);
                break;
            case Screen.LevelSelect:
                levelSelectPanels.SetActive(true);
                break;
            case Screen.Settings:
                settingsPanels.SetActive(true);
                break;
            case Screen.Dailies:
                dailyRewardsPanels.SetActive(true);
                break;
            case Screen.Profile:
                playerProfilePanels.SetActive(true);
                break;
        }

        _currentScreen = screen;
    }
    public void LoadMainMenuScreen()
    {
        ChangeScreen(Screen.MainMenu);
    }
    public void LoadLevelSelectScreen()
    {
        ChangeScreen(Screen.LevelSelect);
    }
    public void LoadSettingsScreen()
    {
        ChangeScreen(Screen.Settings);
    }
    public void LoadDailiesScreen()
    {
        ChangeScreen(Screen.Dailies);
    }
    public void LoadProfileScreen()
    {
        ChangeScreen(Screen.Profile);
    }

}
