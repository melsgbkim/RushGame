using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class HashByNumber
{
    Hashtable hash = new Hashtable();
    public void Delete(ItemWithUIData data)
    {
        Delete(data.item);
    }
    public void Delete(Item item)
    {
        if (hash.ContainsKey(item.ItemNumber))
            hash.Remove(item.ItemNumber);
    }

    public void Add(ItemWithUIData data)
    {
        hash.Add(data.item.ItemNumber, data);
    }

    public ItemWithUIData GetData(int num)
    {
        if (hash.ContainsKey(num) == false) return null;
        return hash[num] as ItemWithUIData;
    }

    public Item GetItem(int num)
    {
        if (hash.ContainsKey(num) == false) return null;
        return (hash[num] as ItemWithUIData).item;
    }

    public GameObject GetObj(int num)
    {
        if (hash.ContainsKey(num) == false) return null;
        return (hash[num] as ItemWithUIData).UI;
    }

    public ICollection GetAllData()
    {
        return hash.Values;
    }
    public int Count { get { return hash.Count; } }

    public List<int> GetKeys()
    {
        List<int> list = hash.Keys.Cast<int>().ToList();
        list.Sort();
        return list;
    }
}