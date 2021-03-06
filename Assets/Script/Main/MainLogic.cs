﻿using UnityEngine;
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
        for(int x=3;x<200;x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                EnemyCreate("Enemy_opossum", new Vector2(x+0.5f, y));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        PopupItemInfoManager tmp = PopupItemInfoManager.Get;
        if (Input.GetKeyDown(KeyCode.T))
        {
            tmp.SetActive(new Item("Item_E0001"));
        }
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
        Item.GetComponent<ItemDropInfo>().Set(id, count, player);
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
