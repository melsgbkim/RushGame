using UnityEngine;
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
        Item i = obj.GetComponent<UIItemInfoUpdater>().item;
        if (parent == EquipList)
        {
            SelectEquipItem();
            itemInfo.SetActive(i, i);
            OtherList.GetItem(i).UI.GetComponent<UIItemInfoUpdater>().SetSelect(true);
        }
        if (parent == OtherList)
        {
            if (itemInfo.item == i) return;//Error This Item is Selected
            if (i.data.isAble("Count"))
            {
                ItemWithUIData itemInExpItemList = ExpItemList.GetItem(i);
                if (itemInExpItemList != null && itemInExpItemList.UI.GetComponent<PopupItemInfo>() != null && itemInExpItemList.UI.GetComponent<PopupItemInfo>().CustomCount != null)
                    PopupItemSelectCount.Get.SetActive(i, AddItemToExpItemList, itemInExpItemList.UI.GetComponent<PopupItemInfo>().CustomCount.Value);
                else
                    PopupItemSelectCount.Get.SetActive(i, AddItemToExpItemList);
            }
            else
            {
                AddItemToExpItemList(i, 1);
            }
        }
        if (parent == ExpItemList)
        {
            ResetItemInfoState(i);
        }

    }

    public void AddItemToExpItemList(Item i,int count)
    {
        if (isActiveAndEnabled == false) return;
        ItemWithUIData itemInOtherList      = OtherList.GetItem(i);
        ItemWithUIData itemInExpItemList    = ExpItemList.GetItem(i);

        if (itemInOtherList == null) return;//Error List Not Contains Item
        if (itemInExpItemList == null)
        {
            ExpItemList.AddItem(i);
            itemInExpItemList = ExpItemList.GetItem(i);
        }

        itemInExpItemList.UI.GetComponent<UIItemInfoUpdater>().SetCount(count);//Will use this Item
        itemInOtherList.UI.GetComponent<UIItemInfoUpdater>().SetCount(i.count - count);//remained item count
        itemInOtherList.UI.GetComponent<UIItemInfoUpdater>().SetSelect(true);
    }

    public void ResetItemInfoState(Item i)
    {
        ExpItemList.DeleteObj(i);

        ItemWithUIData itemInOtherList = OtherList.GetItem(i);
        if (itemInOtherList != null)
        {
            UIItemInfoUpdater InfoInOtherList = itemInOtherList.UI.GetComponent<UIItemInfoUpdater>();
            InfoInOtherList.SetData(i);
        }

        ExpItemList.needCheckPos = true;
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
