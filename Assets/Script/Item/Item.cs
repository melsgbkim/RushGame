using UnityEngine;
using System.Collections;
using System.Xml;

public class Item
{
    static int NextNumber = 0;
    public static int GetNextNumber()
    {
        NextNumber += 1;
        return NextNumber;
    }
    public string id = "";

    public int ItemNumber = 0;
    public string Name = "";
    public bool StackAble = false;
    public string Grade = "";
    public int Level = 1;
    public int count = 0;
    public bool isLocked = false;
    public string State = "";

    Hashtable AbleList = new Hashtable();

    public XmlElement ItemNode = null;
    public XmlElement ItemGradeNode = null;
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
    public Item(string id,bool onlyInfo = false)
    {
        XmlFile ItemInfoFile = XmlFile.Load("ItemInfo");
        ItemNode = ItemInfoFile.GetNodeByID(id, "Item");
        if (ItemNode != null)
        {
            this.id = id;
            if(onlyInfo == false)
                this.ItemNumber = GetNextNumber();

            Name = XMLUtil.FindOneByTag(ItemNode, "Name").InnerText;
            XmlElement tmp = XMLUtil.FindOneByTag(ItemNode, "Grade");
            Grade = (tmp == null ? "nomal" : tmp.InnerText);

            XmlFile GradeInfoFile = XmlFile.Load("GradeInfo");
            ItemGradeNode = GradeInfoFile.GetNodeByID(Grade, "Grade");
        }
        
    }

    public bool CanAddCount()
    {
        if (isAble("Count") == false) return false;

        return true;
    }
    public bool AddCount(int count)
    {
        if (CanAddCount() == false) return false;
        this.count += count;
        if (this.count < 0)
            this.count = 0;
        _UI.SetCount(this.count);
        return true;
    }




    //Info
    public Color GetColorByGrade(string grade, string tag)
    {
        XmlElement nameColor = XMLUtil.FindOneByTag(ItemGradeNode, tag);
        float r = float.Parse(XMLUtil.FindOneByTag(nameColor, "r").InnerText);
        float g = float.Parse(XMLUtil.FindOneByTag(nameColor, "g").InnerText);
        float b = float.Parse(XMLUtil.FindOneByTag(nameColor, "b").InnerText);
        float a = float.Parse(XMLUtil.FindOneByTag(nameColor, "a").InnerText);
        return new Color(r, g, b, a);
    }

    public Color GetColorByGrade(string tag)
    {
        return GetColorByGrade(Grade,tag);
    }



}
