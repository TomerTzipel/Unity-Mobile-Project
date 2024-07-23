using System.Collections.Generic;
using UnityEngine;





public class GameManager : MonoBehaviour
{
    [SerializeField] private MapSettings SO_MapSettings;
    [SerializeField] private GameObject firstLastTile;

    [SerializeField] private GameObject buttonsCanvas;
    [SerializeField] private PlayerSwipeMovement playerSwipe;

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
    }
  
    private void CheckSettings()
    {
        if (PlayerPrefs.GetString(MainMenuManager.controlModeKey) == "Touch")
        {
            buttonsCanvas.SetActive(false);
        }
        else
        {
            playerSwipe.enabled = false;
        }

        if (PlayerPrefs.GetString(MainMenuManager.difficultyModeKey) == "Easy")
        {
            SO_MapSettings.speedMultiplier = 1f;
        }
        else
        {
            SO_MapSettings.speedMultiplier = 1.5f;
        }
    }

    
}
