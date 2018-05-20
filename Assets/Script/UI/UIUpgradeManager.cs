using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIUpgradeManager : MonoBehaviour
{
    public UIPositionUpdater Button;
    public UIPositionUpdater Button2;
    public UIPositionUpdater EquipListUI;
    public UIPositionUpdater OtherListUI;
    public UIItemList EquipList;
    public UIItemList OtherList;

    public PlayerInventory player;

    public List<UIPositionUpdater> list = new List<UIPositionUpdater>();
    public List<UIPositionUpdater> UIUpdateList = new List<UIPositionUpdater>();


    void Start()
    {
        list.Add(Button);
        list.Add(Button2);
        list.Add(EquipListUI);
        list.Add(OtherListUI);
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

    public void CheckIcon()
    {
        ICollection listEtc = player.GetCategoryTable("etc").GetAllData();
        ICollection listEquip = player.GetCategoryTable("equipment").GetAllData();
    }
}
