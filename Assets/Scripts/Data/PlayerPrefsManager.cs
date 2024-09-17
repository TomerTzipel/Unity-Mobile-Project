using System;
using UnityEngine;

public static class PlayerPrefsManager
{
    //Data
    private const string playerPrefsIntializedKey = "FirstExecution";
    private const string bestScoreKey = "BestScore";
    public const string isSaveAvailable = "SaveState";

    //Dailies
    public const string coinsKey = "Coins";
    private const string claimedKeyPrefix = "DayClaimed_"; 
    public const string firstLoginTimeKey = "LastLoginTime";
    public const string currentDayKey = "CurrentDay";

    //Settings
    private const string controlModeKey = "ControlMode";
    private const string soundModeKey = "SoundMode";
    public const string controlModeTouchValue = "Touch";
    public const string controlModeButtonsValue = "Buttons";
    public const string soundOnValue = "On";
    public const string soundOffValue = "Off";

    public static void InitializePlayerPrefs()
    {
        //Will execute only on the first execution of the game
        if (!PlayerPrefs.HasKey(playerPrefsIntializedKey))
        {
            PlayerPrefs.SetInt(playerPrefsIntializedKey, 1);

            PlayerPrefs.SetString(firstLoginTimeKey, DateTime.Now.ToString());

            PlayerPrefs.SetString(controlModeKey, controlModeButtonsValue);
            PlayerPrefs.SetString(soundModeKey, soundOnValue);

            PlayerPrefs.SetInt(bestScoreKey, 0);
            PlayerPrefs.SetInt(coinsKey, 0);
            PlayerPrefs.SetInt(isSaveAvailable, 0);
            PlayerPrefs.Save();
        }
    }
    public static bool GetSaveState()
    {
        return PlayerPrefs.GetInt(isSaveAvailable, 0) == 1;
    }

    public static void SetSaveState(bool value)
    {
        if (value)
        {
            PlayerPrefs.SetInt(isSaveAvailable, 1);
        }
        else
        {
            PlayerPrefs.SetInt(isSaveAvailable, 0);
        }
        
        PlayerPrefs.Save();
    }
    public static string GetControlMode()
    {
        return PlayerPrefs.GetString(controlModeKey, controlModeTouchValue);
    }

    public static void SetControlMode(string mode)
    {
        PlayerPrefs.SetString(controlModeKey, mode);
        PlayerPrefs.Save();
    }
    public static string GetSoundMode()
    {
        return PlayerPrefs.GetString(soundModeKey, soundOnValue);
    }
    public static void SetSoundMode(string mode)
    {
        PlayerPrefs.SetString(soundModeKey, mode);
        PlayerPrefs.Save();
    }
    public static int GetPlayerCoins()
    {
        return PlayerPrefs.GetInt(coinsKey, 0);
    }

    public static void AddToCoins(int amount)
    {
        int currentPlayerCoins = GetPlayerCoins();
        PlayerPrefs.SetInt(coinsKey, currentPlayerCoins + amount);
    }

    public static int GetPlayerBestScore()
    {
        return PlayerPrefs.GetInt(bestScoreKey,0);
    }

    public static void SetPlayerBestScore(int score)
    {
        PlayerPrefs.SetInt(bestScoreKey, score);
        PlayerPrefs.Save();
    }

    public static DateTime GetFirstLoginTime()
    {
        string timeString = PlayerPrefs.GetString(firstLoginTimeKey, DateTime.MinValue.ToString());
        return DateTime.Parse(timeString);
    }

    public static int GetCurrentDay()
    {
        return PlayerPrefs.GetInt(currentDayKey, 1);
    }
    public static void SetCurrentDay(int day)
    {
        PlayerPrefs.SetInt(currentDayKey, day);
        PlayerPrefs.Save();
    }

    public static bool IsDayClaimed(int day)
    {
        return PlayerPrefs.GetInt(claimedKeyPrefix + day, 0) == 1;
    }

    public static void ClaimDay(int day)
    {
        PlayerPrefs.SetInt(claimedKeyPrefix + day,1);
        PlayerPrefs.Save();
    }

    public static void WipeAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All PlayerPrefs have been wiped.");
    }
}
