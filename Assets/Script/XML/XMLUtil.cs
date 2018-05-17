using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class XMLUtil
{
    public static int GetAttributeValue(XmlElement node, string name, int DefaultValue = 0)
    {
        string str = node.GetAttribute(name);
        return GetInt(str,DefaultValue);
    }
    public static float GetAttributeValue(XmlElement node, string name, float DefaultValue = 0f)
    {
        string str = node.GetAttribute(name);
        return GetFloat(str, DefaultValue);
    }
    public static string GetAttributeValue(XmlElement node, string name, string DefaultValue = "")
    {
        string result = node.GetAttribute(name);
        return (result == "" ? DefaultValue : result);
    }

    public static int GetInt(string str, int DefaultValue = 0)
    {
        int result = DefaultValue;
        if (int.TryParse(str, out result))
            return result;
        return DefaultValue;
    }
    public static float GetFloat(string str, float DefaultValue = 0f)
    {
        float result = DefaultValue;
        if (float.TryParse(str, out result))
            return result;
        return DefaultValue;
    }

    public static string GetElementInnerTextByTag(XmlElement node, string tag, string DefaultValue = "")
    {
        XmlElement result = XMLUtil.FindOneByTag(node, tag);
        return (result == null ? DefaultValue : result.InnerText);
    }
    public static int GetElementInnerIntByTag(XmlElement node, string tag, int DefaultValue = 0)
    {
        return GetInt(GetElementInnerTextByTag(node, tag), DefaultValue);
    }
    public static float GetElementInnerFloatByTag(XmlElement node, string tag, float DefaultValue = 0f)
    {
        return GetFloat(GetElementInnerTextByTag(node, tag), DefaultValue);
    }

    public static XmlElement FindOneByTag(XmlElement element, string tag)
    {
        if (element == null) return null;
        XmlNodeList list = element.GetElementsByTagName(tag);
        foreach (XmlElement node in list)
            return node;
        return null;
    }

    public static XmlElement FindOneByIdValue(XmlNodeList list,string id,string idValue)
    {
        foreach (XmlElement node in list)
        {
            if (node.GetAttribute(id) == idValue)
                return node;
        }
        return null;
    }

    public static XmlElement FindOneByTagIdValue(XmlElement element,string tag, string id = "", string idValue = "")
    {
        XmlNodeList list = element.GetElementsByTagName(tag);
        if (list.Count == 0)    return null;
        if (id == "")           return list[0] as XmlElement;
        return FindOneByIdValue(list, id, idValue);
    }

}