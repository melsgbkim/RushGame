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
    
    string[] IDLIST = new string[19];

    
    void Start()
    {
        AddCategory("equipment");
        AddCategory("etc");
        AddCategory("quest");
        AddCategory("potion");

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
        UIInventoryManager.Get.NeedCheckUIPos();
    }
    void AddCategory(string val)
    {
        PlayerItemCategory tmp = new PlayerItemCategory(val);
        UIInventoryManager.Get.AddCategory(tmp);
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

        List<ItemWithUIData> itemList = GetCategoryTable(category).GetItemInfoList(id);
        bool NeedCallNewItem = true;
        if (itemList != null && itemList.Count > 0)
        {
            foreach(ItemWithUIData i in itemList)
            {
                if (i.item.AddCount(count) == true)
                {
                    NeedCallNewItem = false;
                    break;//succes
                }
            }
        }

        if(NeedCallNewItem)//Add NewItem
            GetCategoryTable(category).NewItem(id, count);
        UIInventoryManager.Get.NeedCheckUIPos();
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
        
    }

    
}