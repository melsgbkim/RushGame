using UnityEngine;
using System.Collections;
using System.Xml;

public class Item
{
    public string Name;
    public string Grade;
    
    public Item(string id, bool onlyInfo = false)
    {
        data = new ItemData(id);
        if (data != null)
        {
            this.id = id;
            if (onlyInfo == false)
                this.ItemNumber = GetNextNumber();

            Name = data.Name;
            Grade = data.Grade;
            gradeData = new GradeData(Grade);
        }

    }

    static int NextNumber = 0;
    public static int GetNextNumber()
    {
        NextNumber += 1;
        return NextNumber;
    }
    public string id = "";
    public ItemData data = null;
    public GradeData gradeData = null;

    public int ItemNumber = 0;
    public bool StackAble = false;
    public int Level = 1;
    public int count = 0;
    public bool isLocked = false;
    public string State = "";

    public UIItemInfoUpdater _UI = null;
    public GameObject UI
    {
        get { return _UI.gameObject; }
        set
        {
            if (value.GetComponent<UIItemInfoUpdater>() == null) return;
            _UI = value.GetComponent<UIItemInfoUpdater>();

            _UI.Icon.sprite = data.SpriteWithIndex;//Sprite
            if (data.isAble("Level")) _UI.SetLevel(this.Level);//HasLevel
            if (data.isAble("Count")) _UI.SetCount(this.count);//HasCount
        }
    }

    public bool OK
    {
        get { return id != ""; }
    }
    

    public bool CanAddCount()
    {
        if (data.isAble("Count") == false) return false;

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
}
