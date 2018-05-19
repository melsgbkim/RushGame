using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class PlayerItemCategory
{
    public PlayerInventory Parent;
    public GameObject CategoryUI;
    public string type;
    public int Height
    {
        get
        {
            return 1 + Mathf.CeilToInt(ItemTableByItemNumber.Keys.Count / 9f);
        }
    }
    Hashtable ItemTableByID = new Hashtable();
    Hashtable ItemTableByItemNumber = new Hashtable();
    Hashtable ItemUITableByItemNumber = new Hashtable();
    public PlayerItemCategory(string type, PlayerInventory p)
    {
        this.type = type;
        this.Parent = p;
        CategoryUI = UIInventoryManager.Get.GetCategoryUI(type);
    }

    public List<Item> GetItemInfoList(string id)
    {
        List<Item> result = null;
        if (ItemTableByID.ContainsKey(id))
            result = ItemTableByID[id] as List<Item>;
        else
        {
            result = new List<Item>();
            ItemTableByID.Add(id, result);
        }
        //sort

        return result;
    }


    public Item NewItem(string id, int count = 1)
    {
        Item result = new Item(id);
        List<Item> list = GetItemInfoList(id);
        list.Add(result);

        GameObject ui = UIInventoryManager.Get.NewItemUI();
        result.UI = ui;
        ItemUITableByItemNumber.Add(result.ItemNumber, ui);
        ItemTableByItemNumber.Add(result.ItemNumber, result);

        if (result.CanAddCount())
            result.AddCount(count);
        return result;
    }

    public bool AddItem(int num, int count)
    {
        bool result = false;
        if (ItemTableByItemNumber.ContainsKey(num) == false)
            return false;
        Item i = ItemTableByItemNumber[num] as Item;

        result = i.AddCount(count);
        if (i.count == 0)
            DeleteItem(i);
        return result;
    }

    public void DeleteItem(Item i)
    {
        ItemTableByItemNumber.Remove(i.ItemNumber);
        ItemUITableByItemNumber.Remove(i.ItemNumber);
        MonoBehaviour.Destroy(i.UI);
    }

    public void DeleteItem(int num)
    {
        if (ItemTableByItemNumber.ContainsKey(num) == false) return;
        DeleteItem(ItemTableByItemNumber[num] as Item);
    }
    public void UpdatePos()
    {
        if (CategoryUI.GetComponent<UIItemPositionUpdater>().end == false)
            CategoryUI.GetComponent<UIItemPositionUpdater>().UpdatePos();
        foreach (GameObject obj in ItemUITableByItemNumber.Values)
        {
            UIItemPositionUpdater tmp = obj.GetComponent<UIItemPositionUpdater>();
            if (tmp != null && tmp.end == false)
                tmp.UpdatePos();
        }
    }

    public void UpdateNewPos(int StartIndex)
    {
        Vector3 next = new Vector3(0, StartIndex * -100f, 0);
        CategoryUI.GetComponent<UIItemPositionUpdater>().SetNextPos(next);
        int x = 0;
        StartIndex += 1;
        List<int> list = ItemUITableByItemNumber.Keys.Cast<int>().ToList();
        list.Sort();
        foreach (int key in list)
        {
            next = new Vector3((x - 4) * 100f, StartIndex * -100f, 0);
            (ItemUITableByItemNumber[key] as GameObject).GetComponent<UIItemPositionUpdater>().SetNextPos(next);
            x += 1;
            if (x >= 9)
            {
                x = 0;
                StartIndex += 1;
            }
        }
    }
}
