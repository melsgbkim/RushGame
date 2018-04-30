using UnityEngine;
using System.Collections;
using System.Xml;

public class MainLogic : MonoBehaviour
{
    public GameObject EnemyPrefab;
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
        int Min = (_RandomCountMin == "" ? 1 : int.Parse(_RandomCountMin));
        int Max = (_RandomCountMax == "" ? 1 : int.Parse(_RandomCountMax));

        XmlElement ItemInfo = XmlFile.Load("ItemInfo").GetNodeByID(id, "Item");

        print("id:"+id+" count:"+ Random.RandomRange(Min, Max));

        //DropItemPos(new ItemCube(id, Random.RandomRange(Min, Max) * 1f), pos, vel);

    }
}
