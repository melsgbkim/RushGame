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

                GetComponent<Rigidbody2D>().mass = float.Parse(XMLUtil.FindOneByTag(EnemyNode, "RigidbodyMass").InnerText);
                GetComponent<Rigidbody2D>().drag = float.Parse(XMLUtil.FindOneByTag(EnemyNode, "RigidbodyDrag").InnerText);
                gameObject.AddComponent<EnemyLogic>();
                gameObject.AddComponent<EnemyCollisionHandler>();

                SetChildInfo();
            }
        }
    }

    void SetChildInfo()
    {
        for (int i = 0; i < transform.GetChildCount(); i++)
        {
            Transform child = transform.GetChild(i);
            XmlNodeList list = EnemyNode.GetElementsByTagName(child.name);
            if (list.Count == 0) continue;
            foreach (XmlElement node in list[0].ChildNodes)
            {
                string val = node.InnerText;
                try
                {
                    switch (node.Name)
                    {
                        case "x": SetChildLocalPosX(child, val);break;
                        case "y": SetChildLocalPosY(child, val);break;
                        case "z": SetChildLocalPosZ(child, val);break;
                        case "scale": SetChildLocalScale(child,val); break;
                        case "Animation": SetChildSpriteAnimation(child,val); break;
                    }
                }
                catch (System.Exception e) { }
            }

        }
    }
    void SetChildLocalPosX(Transform c, string value) { c.localPosition = new Vector3(float.Parse(value), c.localPosition.y,  c.localPosition.z); }
    void SetChildLocalPosY(Transform c, string value) { c.localPosition = new Vector3(c.localPosition.x,  float.Parse(value), c.localPosition.z); }
    void SetChildLocalPosZ(Transform c, string value) { c.localPosition = new Vector3(c.localPosition.x, c.localPosition.y, float.Parse(value)); }
    void SetChildLocalScale(Transform c, string value) { c.localScale = new Vector3(float.Parse(value), float.Parse(value), 0); }
    void SetChildSpriteAnimation(Transform c, string value) {
        Animator tmp = c.GetComponent<Animator>();
        tmp.runtimeAnimatorController = Resources.Load(value) as RuntimeAnimatorController; }

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
