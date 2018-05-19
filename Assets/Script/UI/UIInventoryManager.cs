using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIInventoryManager : MonoBehaviour
{
    public static UIInventoryManager Get = null;
    public UIInventoryManager()
    {
        if (Get == null)
            Get = this;
    }

    public GameObject ItemPrefab;

    public Transform root;
    public RectTransform Contents;
    public float InventoryHeightMax = 1020f;
    
    List<PlayerItemCategory> CategoryList = new List<PlayerItemCategory>();
    bool needCheckPos = false;

    public GameObject NewItemUI()
    {
        GameObject result = Instantiate(ItemPrefab);
        result.transform.SetParent(root);
        result.transform.localPosition = Vector3.zero;
        return result;
    }

    public GameObject GetCategoryUI(string name)
    {
        for(int i=0;i< root.childCount;i++)
        {
            if (root.GetChild(i).GetComponent<UIItemPositionUpdater>().name == name)
                return root.GetChild(i).gameObject;
        }
        return null;
    }

    public void SetContentsHeight(float h)
    {
        if (h < InventoryHeightMax) h = InventoryHeightMax;
        Contents.sizeDelta = new Vector2(Contents.rect.width,h);
    }

    public void NeedCheckUIPos()
    {
        needCheckPos = true;
    }
    public void AddCategory(PlayerItemCategory node)
    {
        CategoryList.Add(node);
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
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (PlayerItemCategory node in CategoryList)
            node.UpdatePos();

        if (needCheckPos)
            CheckPositionAll();
    }
}
