
using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text landscapeTimerText;
    [SerializeField] TMP_Text portraitTimerText;

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

            if (_seconds == 60)
            {
                _seconds = 0;
                _minutes++;
            }

            if (_minutes == 60)
            {
                ResetCounters();
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
        string text = "Survival Time - ";

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

        landscapeTimerText.text = text;
        portraitTimerText.text = text;
    }
}
