using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml;


public class DataInterface
{
    public virtual string XmlFilePath() { return ""; }
    public virtual string XmlNodeName() { return ""; }
    string id = "";
    public XmlElement DataNode = null;
    Hashtable AbleList = new Hashtable();

    public string ID
    {
        get { return id; }
        set
        {
            XmlFile File = XmlFile.Load(XmlFilePath());
            DataNode = File.GetNodeByID(value, XmlNodeName());
            if (DataNode == null) this.id = ""; 
            else
            {
                this.id = value;
                Init();
            }
        }
    }

    public virtual void Init() { }

    public string GetElementInnerTextByTag(string tag, string DefaultValue = "")
    {
        return GetElementInnerTextByTag(DataNode, tag, DefaultValue);
    }
    public string GetElementInnerTextByTag(XmlElement node, string tag,string DefaultValue = "")
    {
        XmlElement result = XMLUtil.FindOneByTag(DataNode, tag);
        return (result == null ? DefaultValue : result.InnerText);
    }

    public Sprite SpriteWithIndex
    {
        get
        {
            XmlElement SpriteTagNode = XMLUtil.FindOneByTag(DataNode, "Sprite"); if (SpriteTagNode == null) return null;
            XmlElement ImageTagNode = XMLUtil.FindOneByTag(SpriteTagNode, "Image"); if (ImageTagNode == null) return null;
            object[] objArr = Resources.LoadAll(ImageTagNode.InnerText); if (objArr.Length == 0) return null;
            return objArr[int.Parse(ImageTagNode.GetAttribute("Index"))] as Sprite;
        }
    }

    public Sprite Sprite
    {
        get
        {
            XmlElement SpriteTagNode = XMLUtil.FindOneByTag(DataNode, "Sprite"); if (SpriteTagNode == null) return null;
            return Resources.Load(SpriteTagNode.InnerText) as Sprite;
        }
    }

    public bool isAble(string parameterName)
    {
        if (AbleList.ContainsKey(parameterName)) return (AbleList[parameterName] as bool?).Value;
        XmlElement element = XMLUtil.FindOneByTag(DataNode, parameterName);
        AbleList.Add(parameterName, element != null);
        return element != null;
    }

    public Color GetColorByTag(string tag)
    {
        XmlElement nameColor = XMLUtil.FindOneByTag(DataNode, tag);
        float r = float.Parse(GetElementInnerTextByTag(nameColor, "r","0"));
        float g = float.Parse(GetElementInnerTextByTag(nameColor, "g","0"));
        float b = float.Parse(GetElementInnerTextByTag(nameColor, "b","0"));
        float a = float.Parse(GetElementInnerTextByTag(nameColor, "a","0"));
        return new Color(r, g, b, a);
    }
}
