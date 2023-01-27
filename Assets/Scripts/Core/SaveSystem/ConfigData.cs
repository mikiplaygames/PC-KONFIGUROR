[System.Serializable]
public class ConfigData
{
    public string currentConfig;
    public SerializableDictionary<int, PcData> mainLvlData;

    public ConfigData()
    {
        currentConfig = "";
        mainLvlData = new SerializableDictionary<int, PcData>();
    }
}