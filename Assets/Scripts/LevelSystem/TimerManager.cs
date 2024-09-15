
using System.Collections;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour,ISaveable
{
    [SerializeField] ScoreManager scoreManager;

    [SerializeField] TMP_Text[] TimerTexts;

    private int _seconds;
    private int _minutes;

    public void StartTimer()
    {
        ResetTimer();
    }

    private IEnumerator Timer()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            _seconds++;

            if ((_seconds + _minutes * 60) % scoreManager.TimeRewardInterval == 0) scoreManager.RewardTime();

            if (_seconds == 60)
            {
                _seconds = 0;
                _minutes++;
            }
            UpdateTexts();
        }
    }

    public void ResetTimer()
    {
        StopAllCoroutines();
        ResetCounters();
        UpdateTexts();
        StartCoroutine(Timer());
    }

    private void ResetCounters()
    {
        _seconds = 0;
        _minutes = 0;
    }

    public void UpdateTexts()
    {
        string text = "Time - ";

        if(_minutes < 10)
        {
            text += $"0{_minutes}";
        }
        else
        {
            text += $"{_minutes}";
        }

        text += ":"; 

        if (_seconds < 10)
        {
            text += $"0{_seconds}";
        }
        else
        {
            text += $"{_seconds}";
        }

        foreach (var timerText in TimerTexts) 
        {
            timerText.text = text;
        }
    }

    public void LoadData(GameData data)
    {
        _seconds = data.PlayerData.Seconds;
        _minutes = data.PlayerData.Minutes;
        UpdateTexts();
    }

    public void SaveData(ref GameData data)
    {
        data.PlayerData.Seconds = _seconds;
        data.PlayerData.Minutes = _minutes;
    }

    public void RecordTime()
    {
        AnalyticsManager.RecordRunTimeAnaytic(_minutes, _seconds);
    }
}
