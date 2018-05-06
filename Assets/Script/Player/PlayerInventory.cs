using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    int i = 1;
    Hashtable CategoryTable = new Hashtable();
    List<PlayerItemCategory> CategoryList = new List<PlayerItemCategory>();

    bool needCheckPos = false;
    void Start()
    {
        AddCategory("equipment");
        AddCategory("etc");
        AddCategory("quest");
        AddCategory("potion");
        


    }
    void AddCategory(string val)
    {
        PlayerItemCategory tmp = new PlayerItemCategory(val, this);
        CategoryList.Add(tmp);
        CategoryTable.Add(val, tmp);
    }
    PlayerItemCategory GetCategoryTable(string key)
    {
        if (CategoryTable.ContainsKey(key) == false)
            AddCategory(key);
        return CategoryTable[key] as PlayerItemCategory;
    }
    public void GetItem(string id,int count)
    {
        string category = XMLUtil.FindOneByTag(XmlFile.Load("ItemInfo").GetNodeByID(id, "Item"), "Category").InnerText;
        GetCategoryTable(category).AddItem(id, count);

        needCheckPos = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetItem("Item_00" + (i < 10 ? "0" : "") + i, 1);
            i++;
            if (i > 21) i = 1;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            GetItem("Item_00" + (i < 10 ? "0" : "") + i, -100);
            i++;
            if (i > 21) i = 1;
        }
        foreach (PlayerItemCategory node in CategoryList)
            node.UpdatePos();

        if(needCheckPos)
            CheckPositionAll();
    }

    public void CheckPositionAll()
    {
        int index = 0;
        needCheckPos = false;
        foreach (PlayerItemCategory node in CategoryList)
        {
            node.UpdateNewPos(index);
            index += node.Height;
        }
    }
}


public class PlayerItemCategory
{
    public PlayerInventory Parent;
    public GameObject CategoryUI;
    public string type;
    public int Height
    {
        get
        {
            return 1 + Mathf.CeilToInt(ItemTable.Keys.Count / 9f);
        }
    }
    Hashtable ItemTable = new Hashtable();
    Hashtable ItemUITable = new Hashtable();
    public PlayerItemCategory(string type, PlayerInventory p)
    {
        this.type = type;
        this.Parent = p;
        CategoryUI = UIInventoryManager.Get.GetCategoryUI(type);
    }

    void CheckItem(string key)
    {
        Item tmpItem = null;
        if (ItemTable.ContainsKey(key) == false)
            ItemTable.Add(key, tmpItem = new Item(key));
        if (ItemUITable.ContainsKey(key) == false)
        {
            GameObject tmp = UIInventoryManager.Get.NewItemUI();
            ItemUITable.Add(key, tmp);
            (ItemTable[key] as Item).UI = tmp;
            tmp.GetComponent<Image>().sprite = tmpItem.sprite;
        }
    }
    public void AddItem(string id,int count)
    {
        CheckItem(id);
        (ItemTable[id] as Item).AddCount(count);
        if ((ItemTable[id] as Item).count == 0)
        {
            GameObject tmp = ItemUITable[id] as GameObject;
            ItemUITable.Remove(id);
            ItemTable.Remove(id);
            MonoBehaviour.Destroy(tmp);
        }
    }
    public void UpdatePos()
    {
        if(CategoryUI.GetComponent<UIItemPositionUpdater>().end == false)
            CategoryUI.GetComponent<UIItemPositionUpdater>().UpdatePos();
        foreach (string key in ItemUITable.Keys)
        {
            UIItemPositionUpdater tmp = (ItemUITable[key] as GameObject).GetComponent<UIItemPositionUpdater>();
            if(tmp.end == false)
                tmp.UpdatePos();
        }
    }

    public void UpdateNewPos(int StartIndex)
    {
        Vector3 next = new Vector3(0, StartIndex * -100f, 0);
        CategoryUI.GetComponent<UIItemPositionUpdater>().SetNextPos(next);
        int x = 0;
        StartIndex += 1;
        List<string> list = ItemUITable.Keys.Cast<string>().ToList();
        list.Sort();
        foreach (string key in list)
        {
            next = new Vector3((x-4) * 100f, StartIndex * -100f, 0);
            (ItemUITable[key] as GameObject).GetComponent<UIItemPositionUpdater>().SetNextPos(next);
            x += 1;
            if (x >= 9)
            {
                x = 0;
                StartIndex += 1;
            }
        }
    }
}
