using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class QuestData : DataInterface
{
    public override string XmlFilePath() { return "QuestInfo"; }
    public override string XmlNodeName() { return "Quest"; }
    public override void Init()
    {
        Name = GetElementInnerTextByTag("Name");
        Type = GetElementInnerTextByTag("Type");
        Info = GetElementInnerTextByTag("Info");

        NeedItemList = GetList("NeedItemList");
        RewardItemList = GetList("RewardItemList");
    }
    public QuestData(string id)
    {
        ID = id;
    }

    public string Name = "";
    public string Type = "";
    public string Info = "";

    public List<UIItemDataForScroll> NeedItemList;
    public List<UIItemDataForScroll> RewardItemList;


    public List<UIItemDataForScroll> GetList(string tagName)
    {
        List<UIItemDataForScroll> result = new List<UIItemDataForScroll>();
        XmlElement Node = XMLUtil.FindOneByTag(DataNode, tagName);
        if (Node == null) return result;
        if (Node.ChildNodes.Count == 0) return result;
        for(int i=0;i < Node.ChildNodes.Count;i++)
        {
            XmlElement Item = Node.ChildNodes[i] as XmlElement;
            if (Item == null) continue;
            UIItemDataForScroll tmp = new UIItemDataForScroll(new ItemData(Item.GetAttribute("id")));
            for(int j=0;j< Item.ChildNodes.Count;j++)
            {
                XmlElement Child = Item.ChildNodes[j] as XmlElement;
                if (Child == null) continue;
                tmp.SetValue(Child.Name, int.Parse(Child.InnerText));
            }
            result.Add(tmp);
        }
        return result;
    }
}

