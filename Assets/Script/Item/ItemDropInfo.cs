using UnityEngine;
using System.Collections;
using System.Xml;

public class ItemDropInfo : MonoBehaviour
{
    public int Count = 0;
    public GameObject Player = null;
    ItemData data = null;
    public string id
    {
        get
        {
            return data.ID;
        }
        set
        {
            data = new ItemData(value);
            if (data != null)
            {
                ChildInfoParser.Get.SetChildInfo(transform, data.DataNode);
                MainItemPositionUpdater.Get.AddList(new ItemPositionData(this, transform.localPosition,
                    transform.localPosition + Quaternion.Euler(0, 0, Random.RandomRange(0, 360)) * new Vector3(Random.RandomRange(0.3f, 0.75f), 0, 0),
                    "ItemDrop", 1, 1f, 0.6f + Random.Range(-0.2f, 0)));
            }
        }
    }

    public void EndItemMove(string type)
    {
        if (type == "ItemDrop" && Player != null)
        {
            MainEffManager.Get.NewEff("Eff_ItemHitTheFloor", transform.localPosition);
            ItemGetByPlayer(Player);
        }
        else if (type == "ItemGet")
        {
            Destroy(gameObject);
        }
    }

    public void ItemGetByPlayer(GameObject player)
    {
        MainItemPositionUpdater.Get.AddList(new ItemPositionData(this, transform.localPosition, player.transform, "ItemGet", 0, 3f, 0.2f));
        player.GetComponent<PlayerInventory>().GetItem(id, Count);
    }

    public void Set(string i, int c, GameObject p)
    {
        id = i;
        Count = c;
        Player = p;
    }
}
