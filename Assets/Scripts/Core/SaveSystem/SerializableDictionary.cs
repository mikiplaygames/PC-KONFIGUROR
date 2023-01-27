using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{

    [SerializeField] private List<TKey> lvlIndex = new();
    [SerializeField] private List<TValue> lvlData = new();

    public void OnBeforeSerialize()
    {
        lvlIndex.Clear();
        lvlData.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            lvlIndex.Add(pair.Key);
            lvlData.Add(pair.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        this.Clear();

        if (lvlIndex.Count != lvlData.Count)
        {
            Debug.LogError("Tried to deserialize a SerializableDictionary, but the amount of keys ("
                + lvlIndex.Count + ") does not match the number of values (" + lvlData.Count
                + ") which indicates that something went wrong");
        }

        for (int i = 0; i < lvlIndex.Count; i++)
        {
            this.Add(lvlIndex[i], lvlData[i]);
        }
    }
}