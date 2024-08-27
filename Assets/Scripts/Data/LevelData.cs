using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static readonly Dictionary<string, int> levelThresholds = new Dictionary<string, int>()
    {
        { "Level1", 0 },
        { "Level2", 5000 },
        { "Level3", 15000 },
        { "Level4", 30000 },
        { "Level5", 60000 },
        { "Level6", 85000 },
        { "Level7", 100000 },
        { "Level8", 200000 },
        { "Level9", 500000 },
    };
}
