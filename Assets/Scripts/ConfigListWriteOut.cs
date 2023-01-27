using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigListWriteOut : MonoBehaviour , IDataPersistence
{
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject configInfoPrefab;

    public void AddConfig()
    {
        DataManager.Instance.SaveGame();
    }
    public void SaveData(ConfigData data)
    {
        data.pcData.Add("bioz", new());
    }
    public void LoadData(ConfigData data)
    {
        for (int i = 0; i < data.pcData.Count; i++)
        {
            SummonRecord(i);
        }
    }
    private GameObject SummonRecord(int index)
    {
        GameObject newRecord = Instantiate(configInfoPrefab, content.transform);
        Transform recordT = newRecord.transform;
        int pos = -index * 130;
        recordT.localPosition = new(recordT.localPosition.x, pos - 80, recordT.localPosition.z);
        return newRecord;
    }
}
