using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    private string filePath;

    private void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "playerProfile.json");
    }

    public void SaveProfile(PlayerProfile profile)
    {
        string json = JsonUtility.ToJson(profile);
        File.WriteAllText(filePath, json);
    }

    public PlayerProfile LoadProfile()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<PlayerProfile>(json);
        }
        return null;
    }
}
