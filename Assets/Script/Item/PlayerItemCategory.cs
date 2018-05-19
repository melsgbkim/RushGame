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

    public override void DeleteItem(Item i)
    {
        MonoBehaviour.Destroy(i.UI);
        base.DeleteItem(i);
    }
}
