using UnityEngine;
using System.Collections;
using System.Xml;

public class Item
{
    public string id = "";
    public int count = 0;
    public XmlElement ItemNode = null;
    public GameObject UI = null;
    public Sprite sprite
    {
        get
        {
            XmlElement node = XMLUtil.FindOneByTag(XMLUtil.FindOneByTag(ItemNode, "Sprite"), "Image");
            return Resources.LoadAll(node.InnerText)[int.Parse(node.GetAttribute("Index"))] as Sprite;
        }
    }

    public bool OK
    {
        get { return id != ""; }
    }
    public Item(string id)
    {
        XmlFile ItemInfoFile = XmlFile.Load("ItemInfo");
        ItemNode = ItemInfoFile.GetNodeByID(id, "Item");
        if (ItemNode != null)
            this.id = id;
    }

    public void AddCount(int count)
    {
        this.count += count;
        if (this.count < 0) this.count = 0;
    }


}
