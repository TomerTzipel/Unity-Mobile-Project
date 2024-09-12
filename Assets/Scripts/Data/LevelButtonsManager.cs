using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonsManager : MonoBehaviour
{
    [SerializeField] GameObject buttonsParent;

    private Color lockedColor = new Color32(255, 113, 107, 255);
    private Color unlockedColor = new Color32(107, 255, 226, 255);

    private List<string> accessibleLevels = new List<string>();

    void Start()
    {
        PlayerPrefs.SetInt("PlayerBestScore", 84000);
        PlayerPrefs.Save();

        int playerBestScore = PlayerPrefs.GetInt("PlayerBestScore");

        foreach (var level in LevelData.levelThresholds)
        {
            if (playerBestScore >= level.Value)
            {
                accessibleLevels.Add(level.Key);
            }
        }

        foreach (Transform buttonTransform in buttonsParent.transform)
        {
            Button button = buttonTransform.GetComponent<Button>();

            if (button != null)
            {
                ColorBlock cb = button.colors;
                if (accessibleLevels.Contains(button.name))
                {
                    cb.normalColor = unlockedColor;
                }
                else
                {
                    cb.normalColor = lockedColor;
                }
                button.colors = cb;
            }
        }
    }
}
