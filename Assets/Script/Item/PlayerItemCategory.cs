using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class PlayerItemCategory : ItemCategory
{
    public PlayerInventory Parent;
    public PlayerItemCategory(string type) : base( type : type)
    {
        CategoryUI = UIInventoryManager.Get.GetCategoryUI(type);
    }

    public override void NewData(ItemWithUIData i)
    {
        base.NewData(i);
        i.UI.AddComponent<UIItemClickable>().InitWithEvent(ClickEvent, null);
    }
    public void ClickEvent(GameObject obj, UIItemList parent)
    {
        UIItemInfoUpdater info = obj.GetComponent<UIItemInfoUpdater>();
        PopupItemInfoManager.Get.SetActive(info.item);
    }

    public override void DeleteItem(Item i)
    {
        MonoBehaviour.Destroy(i.UI);
        base.DeleteItem(i);
    }

    public override GameObject GetInventoryIcon()
    {
        return UIInventoryManager.Get.NewItemUI();
    }
}
