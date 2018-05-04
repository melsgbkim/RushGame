using UnityEngine;
using System.Collections;
using System.Xml;

public class ItemInfo : MonoBehaviour
{
    public int Count = 0;
    public GameObject Player = null;

    Item item;

    public string id
    {
        get
        {
            return item.id;
        }
        set
        {
            Item tmp = new Item(value);
            if(tmp.OK)
            {
                item = tmp;
                ChildInfoParser.Get.SetChildInfo(transform, item.ItemNode);
                MainItemPositionUpdater.Get.AddList(new ItemPositionData(this, transform.localPosition,
                    transform.localPosition + Quaternion.Euler(0, 0, Random.RandomRange(0, 360)) * new Vector3(Random.RandomRange(0.3f, 0.75f), 0, 0), "ItemDrop", 1, 1f));
            }
        }
    }

    public void EndItemMove(string type)
    {
        if (type == "ItemDrop")
        {
            ItemGetByPlayer(Player);
        }
        else if (type == "ItemGet")
        {
            Destroy(gameObject);
        }
    }

    public void ItemGetByPlayer(GameObject player)
    {
        MainItemPositionUpdater.Get.AddList(new ItemPositionData(this, transform.localPosition, player.transform, "ItemGet", 0, 3f,0.2f));
        player.GetComponent<PlayerInventory>().GetItem(id,Count);
    }
}
