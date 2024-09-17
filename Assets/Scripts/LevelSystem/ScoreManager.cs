using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour,ISaveable
{

    [SerializeField] private LevelSettings levelSettings;
    [SerializeField] private SpawningManager spawningManager;
    [SerializeField] private int scorePerTimeReward;
    [SerializeField] private int timeRewardInterval;
    [SerializeField] private int scorePerPowerUp;

    [SerializeField] private TMP_Text[] scoreTexts;
    [SerializeField] private TMP_Text[] levelTexts;
    [SerializeField] private TMP_Text[] bestScoreTexts;
    private int _score;

    public int TimeRewardInterval
    {
        get { return timeRewardInterval; }
    }

    private void Awake()
    {
        _score = 0;

        UpdateBestScoreUI();
        UpdateLevelUI();
        UpdateUI();
    }

    public void RewardPowerUp()
    {
        AddScore(scorePerPowerUp);
    }
    public void RewardTime()
    {
        AddScore(scorePerTimeReward);
    }

    private void AddScore(int value)
    {
        _score += (int)(value * levelSettings.CurrentLevel.ScoreMultiplier);
        UpdateUI();

        if (_score >= levelSettings.CurrentLevel.ScoreToFinish)
        {
            ChangeLevel();
        }
    }

    public void UpdateBestScore()
    {
        if(_score > PlayerPrefsManager.GetPlayerBestScore())
        {
            PlayerPrefsManager.SetPlayerBestScore( _score);
        }
        UpdateBestScoreUI();
    }

    private void UpdateUI()
    {
        foreach (var text in scoreTexts)
        {
            text.text = $"Score - {_score}";
        }
    }
    private void UpdateLevelUI()
    {
        foreach (var text in levelTexts)
        {
            text.text = $"Level {levelSettings.CurrentLevel.Index + 1}";
        }
    }
    private void UpdateBestScoreUI()
    {
        foreach (var text in bestScoreTexts)
        {
            text.text = $"Best Score - {PlayerPrefsManager.GetPlayerBestScore()}";
        }
    }

    private void ChangeLevel()
    {
        levelSettings.NextLevel();
        spawningManager.ResetCounters();
        UpdateLevelUI();
    }

    public void LoadData(GameData data)
    {
        _score = data.PlayerData.Score;
        UpdateUI();

        levelSettings.SetCurrentLevel(data.PlayerData.Level);
        UpdateLevelUI();
    }

    public void SaveData(ref GameData data)
    {
        data.PlayerData.Score = _score;
        data.PlayerData.Level = levelSettings.CurrentLevel.Index;
    }
}
