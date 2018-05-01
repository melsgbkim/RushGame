using UnityEngine;
using System.Collections;
using System.Xml;

public class ItemInfo : MonoBehaviour
{
    string _id = "";
    public int Count = 0;
    public GameObject Player = null;
    XmlElement ItemNode = null;
    public string id
    {
        get
        {
            return _id;
        }
        set
        {
            XmlFile ItemInfoFile = XmlFile.Load("ItemInfo");
            ItemNode = ItemInfoFile.GetNodeByID(value, "Item");
            if (ItemNode != null)
            {
                _id = value;

                ChildInfoParser.Get.SetChildInfo(transform, ItemNode);

                MainItemPositionUpdater.Get.AddList(new ItemPositionData(this, transform.localPosition, 
                    transform.localPosition + Quaternion.Euler(0, 0, Random.RandomRange(0, 360)) * new Vector3(Random.RandomRange(0.3f, 0.75f), 0, 0), "ItemDrop", 1,1f));
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
