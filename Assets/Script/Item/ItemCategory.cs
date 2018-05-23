using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ItemCategory
{
    public UIItemPositionUpdater CategoryUI = null;
    public string type;
    public int Width = 9;
    public int Height{get{return (CategoryUI != null ? 1 : 0) + Mathf.CeilToInt(ItemTableByItemNumber.Count / (float)(Width));}}

    HashByID ItemTableByID = new HashByID();
    //Hashtable ItemTableByItemNumber = new Hashtable();
    HashByNumber ItemTableByItemNumber = new HashByNumber();
    public ItemCategory(string type,int width = 9)
    {
        this.type = type;
        Width = width;
        //CategoryUI = UIInventoryManager.Get.GetCategoryUI(type);
    }

    public void ItemReset()
    {
        ItemTableByID = new HashByID();
        ItemTableByItemNumber = new HashByNumber();
    }

    public List<ItemWithUIData> GetItemInfoList(string id)
    {
        return ItemTableByID.GetItemInfoList(id);
    }

    public virtual GameObject GetInventoryIcon()
    {//UIInventoryManager.Get.NewItemUI();
        return null;
    }

    public Item NewItem(string id, int count = 1)
    {
        Item result = new Item(id);
        GameObject ui = GetInventoryIcon();
        result.UI = ui;
        NewData(new ItemWithUIData(result, ui));

        if (result.CanAddCount())
            result.AddCount(count);
        return result;
    }

    public void NewData(ItemWithUIData i)
    {
        ItemTableByID.Add(i);
        ItemTableByItemNumber.Add(i);
    }

    public bool AddCountItem(int num, int count)
    {
        bool result = false;
        Item i = ItemTableByItemNumber.GetItem(num);
        if (i == null)
            return false;

        result = i.AddCount(count);
        if (i.count == 0)
            DeleteItem(i);
        return result;
    }

    public virtual void DeleteItem(Item i)
    {
        ItemTableByID.Delete(i);
        ItemTableByItemNumber.Delete(i);
    }

    public void DeleteItem(int num)
    {
        Item i = ItemTableByItemNumber.GetItem(num);
        if (i == null)return;
        DeleteItem(i);
    }

    public ItemWithUIData GetData(int num)
    {
        return ItemTableByItemNumber.GetData(num);
    }

    public void UpdatePos()
    {
        if (CategoryUI != null && CategoryUI.GetComponent<UIItemPositionUpdater>().end == false)
            CategoryUI.GetComponent<UIItemPositionUpdater>().UpdatePos();
        ICollection list = GetAllData();
        if (list == null) return;
        foreach (ItemWithUIData obj in list)
        {
            UIItemPositionUpdater tmp = obj.UI.GetComponent<UIItemPositionUpdater>();
            if (tmp != null && tmp.end == false)
                tmp.UpdatePos();
        }
    }

    public void UpdateNewPos(int StartIndex)
    {
        Vector3 next = new Vector3(0, StartIndex * -100f, 0);
        if (CategoryUI != null)
        {
            CategoryUI.GetComponent<UIItemPositionUpdater>().SetNextPos(next);
            StartIndex += 1;
        }
        int x = 0;
        List<int> list = ItemTableByItemNumber.GetKeys();
        foreach (int key in list)
        {   
            next = new Vector3(x * 100f - (Width - 1) * 50f, StartIndex * -100f, 0);
            ItemTableByItemNumber.GetObj(key).GetComponent<UIItemPositionUpdater>().SetNextPos(next);
            x += 1;
            if (x >= Width)
            {
                x = 0;
                StartIndex += 1;
            }
        }
    }

    public ICollection GetAllData()
    {
        return ItemTableByItemNumber.GetAllData();
    }
}