using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveSystemManager : MonoBehaviour
{

    [SerializeField] private string fileName;
    [SerializeField] private LoadSettings loadSettings;

    private GameData _gameData;

    private List<ISaveable> _saveables;
    private FileDataHandler _dataHandler;

    private void Awake()
    {
        _saveables = FindAllSaveables();
        _dataHandler = new FileDataHandler(Application.persistentDataPath,fileName);

        _gameData = new GameData();

        if (loadSettings.LoadFromSave) 
        {
            LoadGame();
            loadSettings.LoadFromSave = false;
        } 
    }

    private void LoadGame() 
    {
        _gameData = _dataHandler.Load();

        if (_gameData == null) return;

        foreach (var saveable in _saveables) 
        {
            saveable.LoadData(_gameData);
        }
    }
    public void SaveGame()
    {
        foreach (var saveable in _saveables)
        {
            saveable.SaveData(ref _gameData);
        }

        _dataHandler.Save(_gameData);

        PlayerPrefsManager.SetSaveState(true);
    }


    private List<ISaveable> FindAllSaveables()
    {
        IEnumerable<ISaveable> saveables = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();

        return new List<ISaveable>(saveables);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

}
