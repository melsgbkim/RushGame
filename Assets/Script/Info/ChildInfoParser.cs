using UnityEngine;
using System.Collections;
using System.Xml;

public class ChildInfoParser
{
    static ChildInfoParser _Get = null;
    public static ChildInfoParser Get
    {
        get
        {
            if (_Get == null)
                _Get = new ChildInfoParser();
            return _Get;
        }
    }

    public void SetChildInfo(Transform transform,XmlElement ParentNode)
    {
        for (int i = 0; i < transform.GetChildCount(); i++)
        {
            Transform child = transform.GetChild(i);
            XmlNodeList list = ParentNode.GetElementsByTagName(child.name);
            if (list.Count == 0) continue;
            foreach (XmlElement node in list[0].ChildNodes)
            {
                string val = node.InnerText;
                try
                {
                    switch (node.Name)
                    {
                        //All
                        case "x": SetChildLocalPosX(child, val); break;
                        case "y": SetChildLocalPosY(child, val); break;
                        case "z": SetChildLocalPosZ(child, val); break;
                        case "scale": SetChildLocalScale(child, val); break;

                        //Sprite
                        case "Animation": SetChildSpriteAnimation(child, val); break;
                        case "Image": SetChildSprite(child, val,int.Parse(node.GetAttribute("Index")));break;
                    }
                }
                catch (System.Exception e)
                {
                    MonoBehaviour.print(e);
                }
            }

        }
    }
    void SetChildLocalPosX(Transform c, string value) { c.localPosition = new Vector3(float.Parse(value), c.localPosition.y, c.localPosition.z); }
    void SetChildLocalPosY(Transform c, string value) { c.localPosition = new Vector3(c.localPosition.x, float.Parse(value), c.localPosition.z); }
    void SetChildLocalPosZ(Transform c, string value) { c.localPosition = new Vector3(c.localPosition.x, c.localPosition.y, float.Parse(value)); }
    void SetChildLocalScale(Transform c, string value) { c.localScale = new Vector3(float.Parse(value), float.Parse(value), 0); }
    void SetChildSpriteAnimation(Transform c, string value) { c.GetComponent<Animator>().runtimeAnimatorController = Resources.Load(value) as RuntimeAnimatorController; }
    void SetChildSprite(Transform c, string value,int index = 0) { c.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll(value)[index] as Sprite; }

}
