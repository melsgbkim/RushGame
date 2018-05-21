using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class UIItemList : MonoBehaviour
{
    public List<string> ItemTypeAllowedList = new List<string>();
    public Hashtable ItemTypeAllowedTable = new Hashtable();
    public GameObject ItemPrefab;
    public Transform root;
    public RectTransform Contents;
    public float InventoryHeightMax;
    public int WidthCount = 5;

    Hashtable IconTable = new Hashtable();
    Hashtable CategoryTable = new Hashtable();
    bool needCheckPos = false;

    void AddCategory(string val)
    {
        ItemCategory tmp = new ItemCategory(val, WidthCount);
        CategoryTable.Add(val, tmp);
    }
    ItemCategory GetCategoryTable(string key)
    {
        if (CategoryTable.ContainsKey(key) == false)    AddCategory(key);
        return CategoryTable[key] as ItemCategory;
    }

    void AllCategoryClean()
    {
        foreach (ItemCategory cate in CategoryTable.Values)
            cate.ItemReset();
    }

    public void AddItem(Item i)
    {
        if (isAllowed(i.data.Category) == false) return;
        GameObject ui = NewItemUI();
        AddItem(i, ui);
        ui.GetComponent<UIItemInfoUpdater>().SetData(i);
    }

    public void AddItem(Item i, GameObject ui)
    {
        if (isAllowed(i.data.Category) == false) return;
        ItemWithUIData data = new ItemWithUIData(i, ui);
        IconTable.Add(i, data.UI);

        GetCategoryTable(i.data.Category).NewData(data);
    }




    public void InitItemList(List<Item> list)
    {
        DeleteAllObj();
        AllCategoryClean();
        for (int i=0;i< list.Count; i++)
        {
            AddItem(list[i]);
        }
    }

    public void DeleteAllObj()
    {
        foreach(Item i in IconTable.Keys)
        {
            GameObject ui = (IconTable[i] as GameObject);
            Destroy(ui);
        }
        IconTable = new Hashtable();
    }




    public void Init()
    {
        if (root == null) root = this.transform;
        if (ItemTypeAllowedTable.Count == 0) AllowDataInit();
    }

    public void AllowDataInit()
    {
        foreach (string key in ItemTypeAllowedList)
            ItemTypeAllowedTable.Add(key, true);
    }


    bool isAllowed(string key)
    {
        if (ItemTypeAllowedTable.Count == 0) AllowDataInit();
        if (ItemTypeAllowedTable.ContainsKey(key))
            return (ItemTypeAllowedTable[key] as bool?).Value;
        return false;
    }

    GameObject NewItemUI()
    {
        GameObject result = Instantiate(ItemPrefab);
        UIItemInfoUpdater ui = result.GetComponent<UIItemInfoUpdater>();
        if(ui == null)
        {
            Destroy(result);
            return null;
        }

        result.transform.SetParent(root);
        result.transform.localPosition = Vector3.zero;
        needCheckPos = true;
        return result;
    }

    void SetContentsHeight(float h)
    {
        if (h < InventoryHeightMax) h = InventoryHeightMax;
        Contents.sizeDelta = new Vector2(Contents.rect.width, h);
    }

    // Use this for initialization
    void Start()
    {
        Init();
    }

    public void CheckPositionAll()
    {
        int index = 0;
        needCheckPos = false;
        foreach (ItemCategory node in CategoryTable.Values)
        {
            node.UpdateNewPos(index);
            index += node.Height;
        }
        SetContentsHeight(index * 100f);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ItemCategory node in CategoryTable.Values)
            node.UpdatePos();

        if (needCheckPos)
            CheckPositionAll();

        if (Input.GetKeyDown(KeyCode.B))
            AddItem(new Item("Item_E0001"));
    }
}
