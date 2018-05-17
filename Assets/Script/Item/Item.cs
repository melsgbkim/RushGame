using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class Item
{
    static int NextNumber = 0;
    public static int GetNextNumber()
    {
        NextNumber += 1;
        return NextNumber;
    }

    public string Name;
    public string Grade;

    public string id = "";
    public ItemData data = null;
    public GradeData gradeData = null;
    public int ItemNumber = 0;
    public bool StackAble = false;
    public int Level = 1;
    public int count = 0;
    public bool isLocked = false;
    public string State = "";
    public List<int> OptionIDList = null;
    public StatusValues OptionBase = new StatusValues();
    public StatusValues OptionLvBonus = new StatusValues();

    public UIItemInfoUpdater _UI = null;

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
            TrySetEquipmentRandomOptionData(data.OptionID);
        }

    }

    public GameObject UI
    {
        get { return _UI.gameObject; }
        set
        {
            if (value.GetComponent<UIItemInfoUpdater>() == null) return;
            _UI = value.GetComponent<UIItemInfoUpdater>();
            _UI.SetData(data,this);
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

    void TrySetEquipmentRandomOptionData(string id)
    {
        if (id == "") return;
        if (OptionIDList != null) return;
        OptionIDList = new List<int>();
        EquipmentRandomOptionData OptionData = new EquipmentRandomOptionData(id);
        int cost = 3;
        List<EquipmentRandomOptionNode> nodeList = OptionData.GetRandomOptionList(cost);

        foreach (EquipmentRandomOptionNode node in nodeList)
        {
            OptionBase += node.statValue;
            OptionLvBonus += node.statValueLevelBonus;
            OptionIDList.Add(node.id);
        }
    }

    public StatusValues OptionValues(int PlayerLevel)
    {
        if (OptionIDList != null) TrySetEquipmentRandomOptionData(data.OptionID);
        return OptionBase + OptionLvBonus * (this.Level < PlayerLevel ? this.Level : PlayerLevel);
    }
}
