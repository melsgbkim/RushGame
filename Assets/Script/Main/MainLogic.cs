using UnityEngine;
using System.Collections;
using System.Xml;

public class MainLogic : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject ItemPrefab;
    public static MainLogic Get = null;
    public MainLogic()
    {
        if(Get == null)
            Get = this;
    }
    // Use this for initialization
    void Start()
    {
        for(int x=0;x<10;x++)
        {
            for (int y = -5; y < 5; y++)
            {
                EnemyCreate("Enemy_opossum", new Vector2(x, y+0.5f));
            }
        }

        /*DropItemWithCount("Item_1", new Vector2(1, 0));
        DropItemWithCount("Item_2", new Vector2(2, 0));
        DropItemWithCount("Item_3", new Vector2(3, 0));
        DropItemWithCount("Item_4", new Vector2(4, 0));
        DropItemWithCount("Item_5", new Vector2(5, 0));
        DropItemWithCount("Item_6", new Vector2(6, 0));
        DropItemWithCount("Item_7", new Vector2(7, 0));*/
        DropItemWithCount("Item_8", new Vector2(1, 0));
        DropItemWithCount("Item_9", new Vector2(2, 0));
        DropItemWithCount("Item_10", new Vector2(3, 0));

        DropItemWithCount("Item_Q1", new Vector2(1, 1));
        DropItemWithCount("Item_Q2", new Vector2(2, 1));
        DropItemWithCount("Item_Q3", new Vector2(3, 1));

        DropItemWithCount("Item_E1", new Vector2(1, 2));
        DropItemWithCount("Item_E2", new Vector2(2, 2));
        DropItemWithCount("Item_E3", new Vector2(3, 2));
        DropItemWithCount("Item_E4", new Vector2(4, 2));
        DropItemWithCount("Item_E5", new Vector2(5, 2));
        DropItemWithCount("Item_E6", new Vector2(6, 2));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyCreate(string id,Vector2 pos)
    {
        GameObject enemy = Instantiate(EnemyPrefab, pos,EnemyPrefab.transform.rotation) as GameObject;
        enemy.GetComponent<EnemyInfo>().id = id;
        //enemy.transform.localPosition = pos;
    }

    public void DropItemWithCount(string id, Vector2 pos, GameObject player=null,int count = 1)
    {
        XmlElement ItemInfo = XmlFile.Load("ItemInfo").GetNodeByID(id, "Item");

        GameObject Item = Instantiate(ItemPrefab, pos, ItemPrefab.transform.rotation) as GameObject;
        Item.GetComponent<ItemInfo>().id = id;
        Item.GetComponent<ItemInfo>().Count = count;
        Item.GetComponent<ItemInfo>().Player = player;
    }

    public void RandomDropItem(XmlElement node,Vector2 pos,GameObject player)
    {
        string id = node.InnerText;
        string _minPercent = node.GetAttribute("percent");
        string _RandomCountMin = node.GetAttribute("min");
        string _RandomCountMax = node.GetAttribute("max");

        float minPercent = (_minPercent == "" ? 1 : float.Parse(_minPercent));
        if (Random.Range(0f, 1f) > minPercent) return;

        float Min = (_RandomCountMin == "" ? 1 : float.Parse(_RandomCountMin));
        float Max = (_RandomCountMax == "" ? 1 : float.Parse(_RandomCountMax));
        int count = Mathf.FloorToInt(Random.Range(0f, 1f) * (Max - Min + 1) + Min);

        DropItemWithCount(id, pos, player,count);
    }
}
