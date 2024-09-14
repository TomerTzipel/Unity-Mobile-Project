using System;
using System.Numerics;


[Serializable]
public class PlayerData
{
    //Z is constant,
    //loading while in air makes no sense as to how to load proper animation state from such positon
    //(which is part of the reasons why such games usually don't allow to save and load)
    public float PositionX;

    public int Score;
    public int Hp;
    public int Minutes;
    public int Seconds;
    public int Level;

    public PlayerData()
    {
        PositionX = 0f;
        Score = 0;
        Hp = 0;
        Minutes = 0;
        Seconds = 0;
        Level = 0;
    }
}
