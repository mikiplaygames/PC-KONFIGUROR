[System.Serializable]
public class ConfigData
{
    public SerializableDictionary<string, PcData> pcData;
    public ConfigData()
    {
        pcData = new SerializableDictionary<string, PcData>();
    }
}