using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class SkillInfo
{
    public SkillInfo(string id)
    {
        ID = id;
    }

    XmlElement InfoNode = null;
    string id = "";
    public string ID
    {
        get { return id; }
        set
        {
            XmlFile File = XmlFile.Load("SkillInfo");
            InfoNode = File.GetNodeByID(value, "Skill");
            if (InfoNode != null)
            {
                this.id = value;
                Name = XMLUtil.FindOneByTag(InfoNode, "Name").InnerText;
                Info = XMLUtil.FindOneByTag(InfoNode, "Info").InnerText;
            }
        }
    }

    public string Name = "";    
    public Sprite Sprite
    {
        get
        {
            XmlElement node = XMLUtil.FindOneByTag(XMLUtil.FindOneByTag(InfoNode, "Sprite"), "Image");
            return Resources.LoadAll(node.InnerText)[int.Parse(node.GetAttribute("Index"))] as Sprite;
        }
    }
    public string Info = "";
}
