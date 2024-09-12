using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    private static Color lockedColor = new Color32(255, 113, 107, 255);
    private static Color unlockedColor = new Color32(107, 255, 226, 255);

    [SerializeField] private Button button;
    [SerializeField] private LevelSettings levelSettings;
    [SerializeField] private Level level;

    private bool IsLocked
    {
        get { return PlayerPrefsManager.GetPlayerBestScore() < level.ScoreToUnlock; }
    }

    private void Awake()
    {
        ColorBlock cb = button.colors;
        if (IsLocked)
        {
            cb.normalColor = lockedColor;
        }
        else
        {
            cb.normalColor = unlockedColor;
        }
        button.colors = cb;
    }

    public void SetUpLevel()
    {
        Debug.Log(PlayerPrefsManager.GetPlayerBestScore());
        Debug.Log(level.ScoreToUnlock);
        if (IsLocked) return;

        levelSettings.SetCurrentLevel(level.Index);
        SceneManager.LoadScene("GameScene");
    }
}
