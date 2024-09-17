using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardsMenager : MonoBehaviour
{

    [SerializeField] TMP_Text coinsTextPortrait;
    [SerializeField] TMP_Text coinsTextLandscape;

    [SerializeField] float baseGoldAmount;
    [SerializeField] int numberOfDays;

    [SerializeField] AudioSource awardCoinsSound;
    private void Awake()
    {
        CheckDailyReward(); 
        UpdateUI();
    }

    private void CheckDailyReward()
    {
        int currentDay = PlayerPrefsManager.GetCurrentDay();
        
        if (currentDay > numberOfDays) return;

        DateTime firstLoginTime = PlayerPrefsManager.GetFirstLoginTime(); 
        DateTime currentTime = DateTime.Now;
     
        if ((currentTime - firstLoginTime).TotalHours > 24 * currentDay)
        {  
            PlayerPrefsManager.SetCurrentDay(currentDay+1);
        } 
    }

    public void ClaimReward(int day)
    {
        PlayerPrefsManager.ClaimDay(day);

        float rewardAmount = baseGoldAmount * Mathf.Pow(1.5f, day - 1);
        rewardAmount = Mathf.Round(rewardAmount);
        PlayerPrefsManager.AddToCoins((int)rewardAmount);
        awardCoinsSound.Play();
        UpdateUI();
    }

    private void UpdateUI()
    {
        string text = $"Coins: {PlayerPrefsManager.GetPlayerCoins()}";
        coinsTextPortrait.text = text;
        coinsTextLandscape.text = text;
    }
}
