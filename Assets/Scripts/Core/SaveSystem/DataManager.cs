using UnityEngine;
using HeadDevMikiPlayGames.Core.Singletons;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DataManager : Singleton<DataManager>
{
    private ConfigData gameData;
    private FileDataHandler dataHandler;

    [SerializeField] private List<IDataPersistence> dataPersistenceObjects;

    private void OnEnable()
    {
        if (this.dataHandler == null)
        {
            this.dataHandler = new FileDataHandler(Application.persistentDataPath, "save.save");
        }
        SceneManager.sceneLoaded += GETLIST;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= GETLIST;
    }

    private void GETLIST(Scene sc, LoadSceneMode loadSceneMode)
    {
        this.dataPersistenceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new ConfigData();
        dataHandler.Save(gameData);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            NewGame();
        }

        foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(gameData);
        }
    }
    
    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(gameData);
        }
        
        dataHandler.Save(gameData);
    }


    private List<IDataPersistence> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}