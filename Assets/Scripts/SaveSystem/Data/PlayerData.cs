using System;
using System.Numerics;


[Serializable]
public class PlayerData
{
    //Z is constant,
    //loading while in air makes no sense as to how to load proper animation state from such positon
    //(which is part of the reasons why such games usually don't allow to save and load)
    private float _positionX;

    private int _score;
    private int _hp;
    private int _time;
    private int _level;

    public PlayerData()
    {
        _positionX = 0f;
        _score = 0;
        _hp = 0;
        _time = 0;
        _level = 0;
    }

    public float PositionX
    {
        get { return _positionX; }
        set { _positionX = value; }
    }
    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }
    public int Hp
    {
        get { return _hp; }
        set { _hp = value; }
    }
    public int Time
    {
        get { return _time; }
        set { _time = value; }
    }
    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }
}
