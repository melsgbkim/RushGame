using UnityEngine;
using System.Collections;

[System.Serializable]
public class UIItemDataForScroll
{
    public ItemData data;
    public Hashtable ValueTable = new Hashtable();
    public UIItemDataForScroll(ItemData d)
    {
        data = d;
    }

    public void SetValue(string key, int value) { if (ValueTable.ContainsKey(key) == false) ValueTable.Add(key, value); }
}