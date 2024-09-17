using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Sprite unlockedSprite;
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Image image;

    [SerializeField] private Button button;
    [SerializeField] private LevelSettings levelSettings;
    [SerializeField] private Level level;

    [SerializeField] private AudioSource clickSFX;
    [SerializeField] private AudioSource errorSFX;

    private bool IsLocked
    {
        get { return PlayerPrefsManager.GetPlayerBestScore() < level.ScoreToUnlock; }
    }

    private void Awake()
    {

        if (IsLocked)
        {
            image.sprite = lockedSprite;
        }
        else
        {
            image.sprite = unlockedSprite;
        }
    }

    public void SetUpLevel()
    {
        if (IsLocked)
        {
            errorSFX.Play();
            return;
        }

        clickSFX.Play();
        AnalyticsManager.RecordLevelEntryAnalytic(level.Index + 1);
        PlayerPrefsManager.SetSaveState(false);
        levelSettings.SetCurrentLevel(level.Index);
        SceneManager.LoadScene("GameScene");
    }
}
