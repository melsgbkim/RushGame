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

    public void RandomDropItem(XmlElement node,Vector2 pos)
    {
        string id = node.InnerText;
        string _minPercent = node.GetAttribute("percent");
        string _RandomCountMin = node.GetAttribute("min");
        string _RandomCountMax = node.GetAttribute("max");

        float minPercent = (_minPercent == "" ? 1 : float.Parse(_minPercent));
        if (Random.RandomRange(0, 1) > minPercent) return;

        float Min = (_RandomCountMin == "" ? 1 : float.Parse(_RandomCountMin));
        float Max = (_RandomCountMax == "" ? 1 : float.Parse(_RandomCountMax));
        int count = Mathf.FloorToInt(Random.Range(0f, 1f) * (Max - Min + 1) + Min);

        XmlElement ItemInfo = XmlFile.Load("ItemInfo").GetNodeByID(id, "Item");

        GameObject Item = Instantiate(ItemPrefab, pos, ItemPrefab.transform.rotation) as GameObject;
        Item.GetComponent<ItemInfo>().id = id;
        Item.GetComponent<ItemInfo>().Count = count;
        //DropItemPos(new ItemCube(id, Random.RandomRange(Min, Max) * 1f), pos, vel);

    }
}
