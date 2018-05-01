using UnityEngine;
using System.Collections;
using System.Xml;

public class ItemInfo : MonoBehaviour
{
    string _id = "";
    public int Count = 0;
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

                gameObject.AddComponent<EnemyLogic>();
                gameObject.AddComponent<EnemyCollisionHandler>();

                ChildInfoParser.Get.SetChildInfo(transform, ItemNode);

                MainItemPositionUpdater.Get.AddList(new ItemPositionData(transform, transform.localPosition, transform.localPosition + Quaternion.Euler(0, 0, Random.RandomRange(0, 360)) * new Vector3(Random.RandomRange(0.3f, 0.75f), 0, 0), 1));
            }
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
