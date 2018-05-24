using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class UIItemEquipmentSlotManager : MonoBehaviour
{
    public static UIItemEquipmentSlotManager Get = null;
    public UIItemEquipmentSlotManager()
    {
        if (Get == null)
            Get = this;
    }


    public PlayerInventory player;
    PlayerEquipment _playerEquipManager;
    PlayerEquipment playerEquipManager
    {
        get
        {
            if(_playerEquipManager == null)
                _playerEquipManager = player.gameObject.GetComponent<PlayerEquipment>();
            return _playerEquipManager;
        }
    }
    public List<UIItemEquipmentSlot> SlotList;

    int SelectedSlotIndex = 0;

    public void Click(UIItemEquipmentSlot from)
    {
        PopupEquipmentList.Get.SetActive();
        
        List<ItemWithUIData> listDataEquip = player.GetCategoryTable("equipment").GetAllData().Cast<ItemWithUIData>().ToList();
        List<Item> list = new List<Item>();
        foreach (ItemWithUIData data in listDataEquip)
        {
            if (from.type == UIItemEquipmentSlot.GetEquipType(data.item.data.EquipType) && playerEquipManager.GetItemIndex(data.item) == -1)
                list.Add(data.item);
        }
        PopupEquipmentList.Get.InitItemList(list);

        SlotList[SelectedSlotIndex].Select(false);
        for (int i=0;i< SlotList.Count;i++)
        {
            if(SlotList[i] == from)
            {
                SelectedSlotIndex = i;
                break;
            }
        }
    }

    public void SelectItem(Item i)
    {
        playerEquipManager.EquipItem(i, SelectedSlotIndex);
        InitAllSlot();
    }

    public void InitAllSlot()
    {
        Item[] itemArr = playerEquipManager.ItemArr;
        for(int i=0;i< itemArr.Length;i++)
        {
            SlotList[i].SetData(itemArr[i]);
            SlotList[i].Select(false);
        }
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
