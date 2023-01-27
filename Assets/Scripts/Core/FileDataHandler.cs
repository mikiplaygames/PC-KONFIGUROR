using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string dataPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataPath, string dataFileName)
    {
        this.dataPath = dataPath;
        this.dataFileName = dataFileName;
    }

    public ConfigData Load()
    {
        string fullPath = Path.Combine(dataPath, dataFileName);

        ConfigData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<ConfigData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Could not load game data: " + e.Message);
            }
        }
        return loadedData;
    }
    
    public void Save(ConfigData data)
    {
        string fullPath = Path.Combine(dataPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to load data from file: " + fullPath + e.Message);
        }
    }
}
