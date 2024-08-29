using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using static UnityEditor.Progress;

public class AnalysticsManager : MonoBehaviour
{
    public void LogItemCollected(string itemType, int itemID)
    {
        Analytics.CustomEvent("ItemCollected", new Dictionary<string, object>
        {
            { "itemType", itemType },
            { "itemID", itemID }
        });
    }

    public void LogCheckpointPassed(int checkpointID)
    {
        Analytics.CustomEvent("CheckpointPassed", new Dictionary<string, object>
        {
            { "checkpointID", checkpointID }
        });
    }
}
