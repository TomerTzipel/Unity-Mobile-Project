using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MapSettings SO_MapSettings;
    [SerializeField] private GameObject LastTile;

    private void Awake()
    {
        SO_MapSettings.LastTile = LastTile;
    }
}
