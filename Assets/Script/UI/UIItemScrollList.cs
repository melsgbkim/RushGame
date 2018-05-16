using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIItemScrollList : MonoBehaviour
{
    public GameObject ItemPrefab;
    public List<GameObject> childItemList = new List<GameObject>();
    public Transform root;
    public RectTransform Contents;
    RectTransform thisRectTransform = null;
    public float MaxWidth = 550f;

    public void NewItemIconList(List<UIItemDataForScroll> list)
    {
        foreach (UIItemDataForScroll data in list)
            NewItemIcon(data);
        updateChildPos();
    }

    void NewItemIcon(UIItemDataForScroll uiData)
    {
        GameObject result = Instantiate(ItemPrefab);
        UIItemInfoUpdater ui = result.GetComponent<UIItemInfoUpdater>();
        if (ui == null) return;

        result.transform.SetParent(root);
        ui.SetData(uiData.data, uiData.ValueTable);
        childItemList.Add(result);
    }

    public void updateChildPos()
    {
        if(thisRectTransform==null)thisRectTransform = GetComponent<RectTransform>();
        if (childItemList.Count * 100f < MaxWidth)  thisRectTransform.sizeDelta = new Vector2(childItemList.Count * 100f, thisRectTransform.rect.height);
        else                                        thisRectTransform.sizeDelta = new Vector2(MaxWidth, thisRectTransform.rect.height);
        float w = thisRectTransform.rect.width;
        if (w < childItemList.Count * 100f) w = childItemList.Count * 100f;
        SetContentsWidth(w);

        float x = 50f;
        for(int i=0;i< childItemList.Count;i++)
        {
            childItemList[i].transform.localPosition = new Vector3(x + i * 100f, 0, 0);
        }
        
    }

    public void SetContentsWidth(float w)
    {
        Contents.sizeDelta = new Vector2(w, Contents.rect.height);
    }
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}