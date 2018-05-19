using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HashByID
{
    Hashtable hash = new Hashtable();
    public List<ItemWithUIData> GetItemInfoList(string id)
    {
        List<ItemWithUIData> result = null;
        if (hash.ContainsKey(id))
            result = hash[id] as List<ItemWithUIData>;
        else
        {
            result = new List<ItemWithUIData>();
            hash.Add(id, result);
        }
        return result;
    }
    public void Delete(ItemWithUIData data)
    {
        List<ItemWithUIData> IDlist = GetItemInfoList(data.item.id);
        if (IDlist != null && IDlist.Count > 0)
        {
            for (int i = 0; i < IDlist.Count; i++)
            {
                if (IDlist[i].item.ItemNumber == data.item.ItemNumber)
                {
                    IDlist.RemoveAt(i);
                    return;
                }
            }
        }
    }
    public void Delete(Item item)
    {
        List<ItemWithUIData> IDlist = GetItemInfoList(item.id);
        if (IDlist != null && IDlist.Count > 0)
        {
            for (int i = 0; i < IDlist.Count; i++)
            {
                if (IDlist[i].item.ItemNumber == item.ItemNumber)
                {
                    IDlist.RemoveAt(i);
                    return;
                }
            }
        }
    }

    public void Add(ItemWithUIData data)
    {
        List<ItemWithUIData> IDlist = GetItemInfoList(data.item.id);
        if (IDlist != null)
            IDlist.Add(data);
    }
}