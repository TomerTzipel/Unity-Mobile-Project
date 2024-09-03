using UnityEngine;

public static class PlayerPrefsManager
{
    private const string controlModeKey = "PlayerControlMode";
    private const string difficultyModeKey = "EasyMode";
    private const string PlayerBestScoreKey = "PlayerBestScore";
    public const string LastLoginTimeKey = "LastLoginTime";
    public const string CurrentDayKey = "CurrentDay";

    public const string CoinsKey = "Coins";

    private const string ClaimedKeyPrefix = "DayClaimed_";


    private const string controlsModeDefault = "Touch";
    private const string difficultyModeDefault = "Easy";
    private const int playerBestScoreDefault = 0;

    public static void InitializePlayerPrefs()
    {
        if (!PlayerPrefs.HasKey(controlModeKey))
        {
            PlayerPrefs.SetString(controlModeKey, controlsModeDefault);
            PlayerPrefs.SetString(difficultyModeKey, difficultyModeDefault);
            PlayerPrefs.SetInt(PlayerBestScoreKey, playerBestScoreDefault);
            PlayerPrefs.SetInt(CoinsKey, 0);
            PlayerPrefs.Save();
        }
    }

    public static string GetControlMode()
    {
        return PlayerPrefs.GetString(controlModeKey, controlsModeDefault);
    }

    public static void SetControlMode(string mode)
    {
        PlayerPrefs.SetString(controlModeKey, mode);
        PlayerPrefs.Save();
    }

    public static string GetDifficultyMode()
    {
        return PlayerPrefs.GetString(difficultyModeKey, difficultyModeDefault);
    }

    public static int GetPlayerCoins()
    {
        return PlayerPrefs.GetInt(CoinsKey, 0);
    }

    public static void AddToCoins(int amount)
    {
        int currentPlayerCoins = GetPlayerCoins();
        Debug.Log(currentPlayerCoins);
        PlayerPrefs.SetInt(CoinsKey, currentPlayerCoins + amount);
        Debug.Log(GetPlayerCoins());
    }

    public static void SetDifficultyMode(string mode)
    {
        PlayerPrefs.SetString(difficultyModeKey, mode);
        PlayerPrefs.Save();
    }

    public static int GetPlayerBestScore()
    {
        return PlayerPrefs.GetInt(PlayerBestScoreKey, playerBestScoreDefault);
    }

    public static void SetPlayerBestScore(int score)
    {
        PlayerPrefs.SetInt(PlayerBestScoreKey, score);
        PlayerPrefs.Save();
    }

    public static void SetLastLoginTime(System.DateTime time)
    {
        PlayerPrefs.SetString(LastLoginTimeKey, time.ToString());
        PlayerPrefs.Save();
    }

    public static System.DateTime GetLastLoginTime()
    {
        string timeString = PlayerPrefs.GetString(LastLoginTimeKey, System.DateTime.MinValue.ToString());
        return System.DateTime.Parse(timeString);
    }

    public static void SetCurrentDay(int day)
    {
        PlayerPrefs.SetInt(CurrentDayKey, day);
        PlayerPrefs.Save();
    }

    public static int GetCurrentDay()
    {
        return PlayerPrefs.GetInt(CurrentDayKey, 1);
    }

    public static bool GetClaimedStatus(int day)
    {
        return PlayerPrefs.GetInt(ClaimedKeyPrefix + day, 0) == 1;
    }

    public static void SetClaimedStatus(int day, bool claimed)
    {
        PlayerPrefs.SetInt(ClaimedKeyPrefix + day, claimed ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static void WipeAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All PlayerPrefs have been wiped.");
    }
}
