﻿using UnityEngine;
using System.Collections;
using System.Xml;

public class EnemyInfo : MonoBehaviour
{
    string _id = "";
    public GameObject Player = null;
    XmlElement EnemyNode = null;
    public string id
    {
        get
        {
            return _id;
        }
        set
        {
            XmlFile EnemyInfoFile = XmlFile.Load("EnemyInfo");
            EnemyNode = EnemyInfoFile.GetNodeByID(value, "Enemy");
            if(EnemyNode != null)
            {
                _id = value;

                GetComponent<Rigidbody2D>().mass = float.Parse(XMLUtil.FindOneByTag(EnemyNode, "RigidbodyMass").InnerText);
                GetComponent<Rigidbody2D>().drag = float.Parse(XMLUtil.FindOneByTag(EnemyNode, "RigidbodyDrag").InnerText);
                gameObject.AddComponent<EnemyLogic>();
                gameObject.AddComponent<EnemyCollisionHandler>();

                ChildInfoParser.Get.SetChildInfo(transform,EnemyNode);
            }
        }
    }

    public void OnDead()
    {
        XmlElement DestroyNode = XMLUtil.FindOneByTag(EnemyNode, "Destroy");
        foreach (XmlElement node in DestroyNode.ChildNodes)
        {
            switch(node.Name)
            {
                case "RandomDropItem":
                    MainLogic.Get.RandomDropItem(node, transform.position,Player);
                    break;
            }
        }
    }
}
