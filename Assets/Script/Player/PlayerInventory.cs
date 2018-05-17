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
    string[] IDLIST = new string[19];

    bool needCheckPos = true;
    void Start()
    {
        AddCategory("equipment");
        AddCategory("etc");
        AddCategory("quest");
        AddCategory("potion");

        Hashtable Test = new Hashtable();
        Test.Add("test", 1);
        Test.Add(1, 2);
        Test.Add(2, 3);
        Test.Add(3.5f, 4);
        foreach(object o in Test.Keys)
        {
            print(o);
            print(o as string);
            int? tmp = (o as int?);
            print((tmp.HasValue ? tmp.Value.ToString() : "NO"));

            float? tmpf = (o as float?);
            print((tmpf.HasValue ? tmpf.Value.ToString() : "NO"));
        }
        print("end");


        IDLIST[0] = "Item_0001";
        IDLIST[1] = "Item_0002";
        IDLIST[2] = "Item_0003";
        IDLIST[3] = "Item_0004";
        IDLIST[4] = "Item_0005";
        IDLIST[5] = "Item_0006";
        IDLIST[6] = "Item_0007";
        IDLIST[7] = "Item_0008";
        IDLIST[8] = "Item_0009";
        IDLIST[9] = "Item_0010";
        IDLIST[10] = "Item_Q0001";
        IDLIST[11] = "Item_Q0002";
        IDLIST[12] = "Item_Q0003";
        IDLIST[13] = "Item_E0001";
        IDLIST[14] = "Item_E0002";
        IDLIST[15] = "Item_E0003";
        IDLIST[16] = "Item_E0004";
        IDLIST[17] = "Item_E0005";
        IDLIST[18] = "Item_E0006";

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
        XmlElement node = XmlFile.Load("ItemInfo").GetNodeByID(id, "Item");
        string category = XMLUtil.FindOneByTag(node, "Category").InnerText;
        bool CountAble = XMLUtil.FindOneByTag(XMLUtil.FindOneByTag(node, "AbleList"), "Count") != null;

        List<Item> itemList = GetCategoryTable(category).GetItemInfoList(id);
        bool NeedCallNewItem = true;
        if (itemList != null && itemList.Count > 0)
        {
            foreach(Item i in itemList)
            {
                if (i.AddCount(count) == true)
                    NeedCallNewItem = false;
                    break;//succes
            }
        }

        if(NeedCallNewItem)//Add NewItem
            GetCategoryTable(category).NewItem(id, count);
        needCheckPos = true;
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            GetItem(IDLIST[i], 1);
            i++;
            if (IDLIST.Length <= i) i = 0;
        }

        if (Input.GetKey(KeyCode.S))
        {
            GetItem(IDLIST[i], -100);
            i++;
            if (IDLIST.Length <= i) i = 0;
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
        UIInventoryManager.Get.SetContentsHeight(index * 100f);
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


    public Item NewItem(string id,int count = 1)
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

    public bool AddItem(int num,int count)
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
        if (ItemTableByItemNumber.ContainsKey(num) == false)return;
        DeleteItem(ItemTableByItemNumber[num] as Item);
    }
    public void UpdatePos()
    {
        if(CategoryUI.GetComponent<UIItemPositionUpdater>().end == false)
            CategoryUI.GetComponent<UIItemPositionUpdater>().UpdatePos();
        foreach (GameObject obj in ItemUITableByItemNumber.Values)
        {
            UIItemPositionUpdater tmp = obj.GetComponent<UIItemPositionUpdater>();
            if (tmp != null &&tmp.end == false)
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
            next = new Vector3((x-4) * 100f, StartIndex * -100f, 0);
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
