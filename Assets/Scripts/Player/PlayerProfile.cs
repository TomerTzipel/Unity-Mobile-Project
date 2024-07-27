using System;

[Serializable]
public class PlayerProfile
{
    public string name;
    public int age;
    public string photoPath;

    public PlayerProfile(string name, int age, string photoPath)
    {
        this.name = name;
        this.age = age;
        this.photoPath = photoPath;
    }
}