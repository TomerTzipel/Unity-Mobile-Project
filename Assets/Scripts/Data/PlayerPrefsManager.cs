using UnityEngine;

public static class PlayerPrefsManager
{
    private const string controlModeKey = "ControlMode";
    private const string difficultyModeKey = "DifficulityMode";
    private const string bestScoreKey = "BestScore";
    public const string lastLoginTimeKey = "LastLoginTime";
    public const string currentDayKey = "CurrentDay";

    public const string coinsKey = "Coins";

    private const string claimedKeyPrefix = "DayClaimed_";


    public const string controlModeTouchValue = "Touch";
    public const string controlModeButtonsValue = "Buttons";
    public const string difficultyModeEasyValue = "Easy";
    public const string difficultyModeHardValue = "Hard";

    public static void InitializePlayerPrefs()
    {
        if (!PlayerPrefs.HasKey(controlModeKey))
        {
            PlayerPrefs.SetString(controlModeKey, controlModeTouchValue);
            PlayerPrefs.SetString(difficultyModeKey, difficultyModeEasyValue);
            PlayerPrefs.SetInt(bestScoreKey, 0);
            PlayerPrefs.SetInt(coinsKey, 0);
            PlayerPrefs.Save();
        }
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
    public static string GetDifficultyMode()
    {
        return PlayerPrefs.GetString(difficultyModeKey, difficultyModeEasyValue);
    }
    public static void SetDifficultyMode(string mode)
    {
        PlayerPrefs.SetString(difficultyModeKey, mode);
        PlayerPrefs.Save();
    }
    public static int GetPlayerCoins()
    {
        return PlayerPrefs.GetInt(coinsKey, 0);
    }

    public static void AddToCoins(int amount)
    {
        int currentPlayerCoins = GetPlayerCoins();
        Debug.Log(currentPlayerCoins);
        PlayerPrefs.SetInt(coinsKey, currentPlayerCoins + amount);
        Debug.Log(GetPlayerCoins());
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

    public static void SetLastLoginTime(System.DateTime time)
    {
        PlayerPrefs.SetString(lastLoginTimeKey, time.ToString());
        PlayerPrefs.Save();
    }

    public static System.DateTime GetLastLoginTime()
    {
        string timeString = PlayerPrefs.GetString(lastLoginTimeKey, System.DateTime.MinValue.ToString());
        return System.DateTime.Parse(timeString);
    }

    public static void SetCurrentDay(int day)
    {
        PlayerPrefs.SetInt(currentDayKey, day);
        PlayerPrefs.Save();
    }

    public static int GetCurrentDay()
    {
        return PlayerPrefs.GetInt(currentDayKey, 1);
    }

    public static bool GetClaimedStatus(int day)
    {
        return PlayerPrefs.GetInt(claimedKeyPrefix + day, 0) == 1;
    }

    public static void SetClaimedStatus(int day, bool claimed)
    {
        PlayerPrefs.SetInt(claimedKeyPrefix + day, claimed ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static void WipeAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All PlayerPrefs have been wiped.");
    }
}
