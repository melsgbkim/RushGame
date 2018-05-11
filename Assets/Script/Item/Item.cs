using UnityEngine;
using System.Collections;
using System.Xml;

public class Item
{
    public string id = "";
    
    string Name = "";
    bool StackAble = false;
    string Grade = "";
    int Level = 1;
    public int count = 1;
    bool isLocked = false;
    string State = "";

    Hashtable AbleList = new Hashtable();

    public XmlElement ItemNode = null;
    public UIItemInfoUpdater _UI = null;
    public GameObject UI
    {
        get { return _UI.gameObject; }
        set
        {
            if (value.GetComponent<UIItemInfoUpdater>() == null) return;
            _UI = value.GetComponent<UIItemInfoUpdater>();


            _UI.Icon.sprite = sprite;//Sprite
            //_UI.Background;//Grade
            //CheckAbleList
            if (isAble("Level")) _UI.SetLevel(this.Level);//HasLevel
            if (isAble("Count")) _UI.SetCount(this.count);//HasCount
        }
    }
    public Sprite sprite
    {
        get
        {
            XmlElement node = XMLUtil.FindOneByTag(XMLUtil.FindOneByTag(ItemNode, "Sprite"), "Image");
            return Resources.LoadAll(node.InnerText)[int.Parse(node.GetAttribute("Index"))] as Sprite;
        }
    }

    public bool isAble(string parameterName)
    {
        if (AbleList.ContainsKey(parameterName)) return (AbleList[parameterName] as bool?).Value;
        XmlElement element = XMLUtil.FindOneByTag(ItemNode, parameterName);
        AbleList.Add(parameterName, element != null);
        return element != null;
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

    public bool AddCount(int count)
    {
        if (isAble("Count") == false) return false;
        this.count += count;
        if (this.count < 0)
            this.count = 0;
        _UI.SetCount(this.count);
        return true;
    }
}
