using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopupEquipmentList : MonoBehaviour
{
    public static PopupEquipmentList Get = null;
    public PopupEquipmentList()
    {
        if (Get == null)
            Get = this;
    }

    public UIItemList List;

    public void SetActive()
    {
        List.actionNewItemUI = AddUIItemClickableComponent;
        gameObject.SetActive(true);
    }
    public void AddUIItemClickableComponent(GameObject obj, UIItemList parent)
    {
        UIItemClickable ui = obj.AddComponent<UIItemClickable>();
        ui.InitWithEvent(ClickedItem, parent);
    }
    public void ClickedItem(GameObject obj, UIItemList parent)
    {
        Item i = obj.GetComponent<UIItemInfoUpdater>().item;
        UIItemEquipmentSlotManager.Get.SelectItem(i);
        gameObject.SetActive(false);
    }

    public void InitItemList(List<Item> list)
    {
        List.InitItemList(list);
    }

    public void SetClose()
    {
        gameObject.SetActive(false);
        UIItemEquipmentSlotManager.Get.InitAllSlot();
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
