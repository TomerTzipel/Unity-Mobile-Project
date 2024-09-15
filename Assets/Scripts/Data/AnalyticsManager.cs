using System;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using Unity.Services.Core.Environments;

public class AnalyticsManager : MonoBehaviour
{
    private static AnalyticsManager _instance;
    private static bool _isInitialized = false;
    public static AnalyticsManager Instance
    {
        get { return _instance; }
    }

    async void Awake()
    {
        if(_isInitialized)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        _isInitialized = true;

        try
        {
            var options = new InitializationOptions();
            options.SetEnvironmentName("production");
            await UnityServices.InitializeAsync(options);
            Debug.Log("Analytics initialized successfully.");
            AnalyticsService.Instance.StartDataCollection();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to initialize analytics: {ex.Message}");
        }

        DontDestroyOnLoad(gameObject);
    }

    public static void RecordPowerUpAnalytic(PowerUpType type)
    {
        if (UnityServices.State == ServicesInitializationState.Initialized)
        {
            try
            {
                CustomEvent e = new("Power_Up_Collected")
                {
                    { "Power_Up", type.ToString() }
                };
                AnalyticsService.Instance.RecordEvent(e);
                AnalyticsService.Instance.Flush();
                Debug.Log("Event Recorded: Power_Up," + type.ToString());
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to send analytics event: {ex.Message}");
            }
        }
        else
        {
            Debug.Log("error initialized!");
        }
    }
    public static void RecordLevelEntryAnalytic(int levelNumber)
    {
        if (UnityServices.State == ServicesInitializationState.Initialized)
        {
            try
            {
                CustomEvent e = new("Level_Entry")
                {
                    { "Level", levelNumber }
                };
                AnalyticsService.Instance.RecordEvent(e);
                AnalyticsService.Instance.Flush();
                Debug.Log("Event Recorded: Level," + levelNumber);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to send analytics event: {ex.Message}");
            }
        }
        else
        {
            Debug.Log("error initialized!");
        }
    }

    public static void RecordRunTimeAnaytic(int minutes, int seconds)
    {
        if (UnityServices.State == ServicesInitializationState.Initialized)
        {
            try
            {
                CustomEvent e = new("Run_Time")
                {
                    { "Minutes", minutes },
                    { "Seconds", seconds }
                };
                AnalyticsService.Instance.RecordEvent(e);
                AnalyticsService.Instance.Flush();
                Debug.Log("Event Recorded: Time," + $"{minutes}:{seconds}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to send analytics event: {ex.Message}");
            }
        }
        else
        {
            Debug.Log("error initialized!");
        }
    }
}
