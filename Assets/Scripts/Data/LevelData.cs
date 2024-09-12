using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static readonly Dictionary<string, int> levelThresholds = new Dictionary<string, int>()
    {
        { "Level1", 400 },
        { "Level2", 1000 },
        { "Level3", 1800 },
        { "Level4", 3000 },
        { "Level5", 4500 },
        { "Level6", 6250 },
        { "Level7", 8250 },
        { "Level8", 10500 },
        { "Level9", 999999 },
    };
}
