using UnityEngine;
using System.Collections;
using System.Xml;

public class EnemyInfo : MonoBehaviour
{
    string _id = "";
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

                GetComponent<Animator>().runtimeAnimatorController = Resources.Load(XMLUtil.FindOneByTag(EnemyNode, "Animation").InnerText) as RuntimeAnimatorController;
                GetComponent<Rigidbody2D>().mass = float.Parse(XMLUtil.FindOneByTag(EnemyNode, "RigidbodyMass").InnerText);
                GetComponent<Rigidbody2D>().drag = float.Parse(XMLUtil.FindOneByTag(EnemyNode, "RigidbodyDrag").InnerText);
                gameObject.AddComponent<EnemyLogic>();
                gameObject.AddComponent<EnemyCollisionHandler>();
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
                    MainLogic.Get.RandomDropItem(node, transform.position);
                    break;
            }
        }
    }
}
