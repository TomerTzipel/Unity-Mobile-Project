
using System.IO;
using UnityEngine;

public class ProfileManager
{
    private string _filePath;

    public ProfileManager()
    {
        _filePath = Path.Combine(Application.persistentDataPath, "playerProfile.json");
    }

    public void SaveProfile(PlayerProfile profile)
    {
        string json = JsonUtility.ToJson(profile);
        File.WriteAllText(_filePath, json);
    }

    public PlayerProfile LoadProfile()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            return JsonUtility.FromJson<PlayerProfile>(json);
        }
        return null;
    }
}
