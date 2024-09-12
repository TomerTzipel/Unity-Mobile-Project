using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "ScriptableObjects/LevelSystem/LevelSettings")]
public class LevelSettings : ScriptableObject
{
    [SerializeField] private Level[] levels;
    public Level CurrentLevel { get; private set; } 

    public int NumberOfLevels
    {
        get { return levels.Length; }
    }

    public void SetCurrentLevel(int index)
    {
        if (levels == null)
        {
            Debug.LogError("No Levels");
            return;
        }
        if (index >= levels.Length) { return; }

        CurrentLevel = levels[index];
    }

    public void NextLevel()
    {
        SetCurrentLevel(CurrentLevel.Index+1);
    }
}
