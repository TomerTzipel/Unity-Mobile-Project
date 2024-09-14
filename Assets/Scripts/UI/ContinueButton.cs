using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    private static Color lockedColor = new Color32(39, 33, 34, 255);


    [SerializeField] private LoadSettings loadSettings;
    [SerializeField] private Button button;
    void Awake()
    {
        if (!PlayerPrefsManager.GetSaveState())
        {
            ColorBlock cb = button.colors;
            cb.normalColor = lockedColor;
            cb.highlightedColor = lockedColor;
            cb.pressedColor = lockedColor;
            cb.selectedColor = lockedColor;
            cb.disabledColor = lockedColor;
            button.colors = cb;
        }  
    }

    public void OnClick()
    {
        if (!PlayerPrefsManager.GetSaveState()) return;

        loadSettings.LoadFromSave = true;
        SceneManager.LoadScene("GameScene");
    }
}
