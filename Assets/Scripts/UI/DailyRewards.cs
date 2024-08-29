using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewards : MonoBehaviour
{
    [SerializeField] List<Button> rewardButtons;
    [SerializeField] Sprite canClaimIcon;        
    [SerializeField] Sprite claimedIcon;         
    [SerializeField] Sprite unAvailable;         
    [SerializeField] Sprite claimed;

    [SerializeField] AudioSource awardCoinsSound;

    void Start()
    {
        PlayerPrefsManager.WipeAllPlayerPrefs();
        PlayerPrefsManager.SetCurrentDay(5);
        PlayerPrefsManager.SetLastLoginTime(DateTime.Now.AddDays(-5));

        CheckDailyReward();
        UpdateRewardUI();
    }

    void CheckDailyReward()
    {
        DateTime lastLoginTime = PlayerPrefsManager.GetLastLoginTime();
        DateTime currentTime = DateTime.Now;
        int currentDay = PlayerPrefsManager.GetCurrentDay();

        if ((currentTime - lastLoginTime).TotalHours >= 24)
        {
            currentDay = Mathf.Min(currentDay + 1, rewardButtons.Count);
            PlayerPrefsManager.SetCurrentDay(currentDay);
        }

        PlayerPrefsManager.SetLastLoginTime(currentTime);
    }

    void UpdateRewardUI()
    {
        int currentDay = PlayerPrefsManager.GetCurrentDay();

        for (int i = 0; i < rewardButtons.Count; i++)
        {
            Image canClaimImage = rewardButtons[i].transform.Find("CanClaim").GetComponent<Image>();

            if (i < currentDay)
            {
                if (PlayerPrefsManager.GetClaimedStatus(i + 1))
                {
                    
                    rewardButtons[i].image.sprite = claimed;
                    canClaimImage.sprite = claimedIcon;
                }
                else
                {
                    
                    rewardButtons[i].image.sprite = claimed;
                    canClaimImage.sprite = canClaimIcon;
                    canClaimImage.gameObject.SetActive(true);
                }
            }
            else
            {
                
                rewardButtons[i].image.sprite = unAvailable;
                canClaimImage.gameObject.SetActive(false);
            }
        }
    }

    public void ClaimReward(int day)
    {
        if (day <= PlayerPrefsManager.GetCurrentDay())
        {
            if (!PlayerPrefsManager.GetClaimedStatus(day))
            {
                PlayerPrefsManager.SetClaimedStatus(day, true);

                float baseAmount = 100f;
                float rewardAmount = baseAmount * Mathf.Pow(1.5f, day - 1);
                rewardAmount = Mathf.Round(rewardAmount);
                PlayerPrefsManager.AddToCoins((int)rewardAmount);

                UpdateRewardUI();
                awardCoinsSound.Play();
                Debug.Log($"Reward for Day {day} claimed! Added {rewardAmount} coins.");
            }
            else
            {
                Debug.Log("Reward already claimed.");
            }
        }
        else
        {
            Debug.Log("Reward not available yet.");
        }
    }

    public void OnRewardButtonPressed(Button button)
    {
        string buttonName = button.name;
        if (buttonName.StartsWith("Day"))
        {
            int day = int.Parse(buttonName.Substring(3));

            if (day <= PlayerPrefsManager.GetCurrentDay())
            {
                ClaimReward(day);
                UpdateRewardUI();
            }
            else
            {
                Debug.Log("Cannot claim this reward yet.");
            }
        }
        else
        {
            Debug.Log("Invalid button name.");
        }
    }


}
