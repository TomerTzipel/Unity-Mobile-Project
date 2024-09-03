using System;

[Serializable]
public class PlayerProfile
{
    public string name;
    public int age;
    public int BestScore;
    public string photoPath;

    public PlayerProfile(string name, int age, int bestScore, string photoPath)
    {
        this.name = name;
        this.age = age;
        this.BestScore = bestScore;
        this.photoPath = photoPath;
    }
}
