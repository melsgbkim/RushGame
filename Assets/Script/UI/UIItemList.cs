using UnityEngine;
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

    public void AddItem(Item i)
    {
        if (isAllowed(i.data.Category) == false) return;
        ItemWithUIData data = new ItemWithUIData(i, NewItemUI());
        
        GetCategoryTable(i.data.Category).NewData(data);
    }




    public void Init()
    {
        if (root == null) root = this.transform;
        foreach (string key in ItemTypeAllowedList)
            ItemTypeAllowedTable.Add(key, true);
    }

    bool isAllowed(string key)
    {
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
