using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoadSettings", menuName = "ScriptableObjects/LoadSettings")]
public class LoadSettings : ScriptableObject 
{
    private bool _loadFromSave = false;

    public bool LoadFromSave { get { return _loadFromSave; } set { _loadFromSave = value; } }
}
