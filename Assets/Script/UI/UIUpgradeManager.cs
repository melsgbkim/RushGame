﻿using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class UIUpgradeManager : MonoBehaviour
{
    public UIUpgradeItemPreview itemInfo;
    public UIPositionUpdater Button;
    public UIPositionUpdater Button2;
    public UIPositionUpdater EquipListUI;
    public UIPositionUpdater OtherListUI;
    public UIItemList ExpItemList;
    public UIItemList EquipList;
    public UIItemList OtherList;    

    public PlayerInventory player;

    List<UIPositionUpdater> list = new List<UIPositionUpdater>();
    List<UIPositionUpdater> UIUpdateList = new List<UIPositionUpdater>();
    bool Initialized = false;

    public void AddUIItemClickableComponent(GameObject obj,UIItemList parent)
    {
        UIItemClickable ui = obj.AddComponent<UIItemClickable>();
        ui.InitWithEvent(ClickedItem, parent);
    }

    public void ClickedItem(GameObject obj, UIItemList parent)
    {
        print(obj.name + " is Clicked ");
        if (parent == EquipList)
        {
            SelectEquipItem();
            Item i = obj.GetComponent<UIItemInfoUpdater>().item;
            itemInfo.SetActive(i, i);
        }
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        if (Initialized == true) return;
        list.Add(Button);
        list.Add(Button2);
        list.Add(EquipListUI);
        list.Add(OtherListUI);

        ExpItemList.actionNewItemUI = AddUIItemClickableComponent;
        EquipList.actionNewItemUI = AddUIItemClickableComponent;
        OtherList.actionNewItemUI = AddUIItemClickableComponent;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < UIUpdateList.Count; i++)
            UIUpdateList[i].UpdatePos();
        for (int i = 0; i < UIUpdateList.Count;)
        {
            if (UIUpdateList[i].end)
            {
                UIUpdateList.Remove(UIUpdateList[i]);
            }
            else
                i++;
        }
    }

    public void OpenEquipList()
    {
        Button.toggle = UIPositionUpdater.DefaultToggle;
        Button2.toggle = UIPositionUpdater.DefaultToggle;
        EquipListUI.toggle = "Opened";
        OtherListUI.toggle = UIPositionUpdater.DefaultToggle;

        UIUpdateList.AddRange(list);
    }

    public void SelectEquipItem()
    {
        Button.toggle = "Opened";
        Button2.toggle = "Opened";
        EquipListUI.toggle = UIPositionUpdater.DefaultToggle;
        OtherListUI.toggle = "Opened";
        UIUpdateList.AddRange(list);
    }

    public void ResetIcon()
    {
        Init();
        List<ItemWithUIData> listDataEtc = player.GetCategoryTable("etc").GetAllData().Cast<ItemWithUIData>().ToList();
        List<ItemWithUIData> listDataEquip = player.GetCategoryTable("equipment").GetAllData().Cast<ItemWithUIData>().ToList();

        List<Item> listEquip = new List<Item>();
        List<Item> listSum   = new List<Item>();
        foreach (ItemWithUIData data in listDataEtc) listSum.Add(data.item);
        foreach (ItemWithUIData data in listDataEquip) listEquip.Add(data.item);
        listSum.AddRange(listEquip);

        ExpItemList.InitItemList(new List<Item>());
        EquipList.InitItemList(listEquip);
        OtherList.InitItemList(listSum);
    }

    void OnEnable()
    {
        ResetIcon();


    }
}
