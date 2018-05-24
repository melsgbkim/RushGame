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
    public Level level;
    public int count = 0;
    public bool isLocked = false;
    public string State = "";
    public List<int> OptionIDList = null;
    public StatusValues OptionBase = new StatusValues(StatusValues.TYPE.integer);
    public StatusValues OptionLvBonus = new StatusValues(StatusValues.TYPE.integer);
    public StatusValues OptionPerBase = new StatusValues(StatusValues.TYPE.percent);
    public StatusValues OptionPerLvBonus = new StatusValues(StatusValues.TYPE.percent);

    public UIItemInfoUpdater _UI = null;

    public Item(string id, bool onlyInfo = false)
    {
        data = new ItemData(id);
        if (data != null)
        {
            this.id = id;
            this.ItemNumber = GetNextNumber();

            Name = data.Name;
            Grade = data.Grade;
            gradeData = new GradeData(Grade);
            TrySetEquipmentRandomOptionData(data.OptionID);
            level = new Level(ExpData.EXPTYPE.Equipment,data.level);
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
        int cost = Random.Range(0,7);
        List<EquipmentRandomOptionNode> nodeList = OptionData.GetRandomOptionList(cost);

        foreach (EquipmentRandomOptionNode node in nodeList)
        {
            if (node.isInt)
            {
                OptionBase += node.statValue;
                OptionLvBonus += node.statValueLevelBonus;   
            }
            else
            {
                OptionPerBase += node.statValue;
                OptionPerLvBonus += node.statValueLevelBonus;
            }
            OptionIDList.Add(node.id);
        }
    }

    public StatusValues OptionValues(int PlayerLevel)
    {
        if (OptionIDList != null) TrySetEquipmentRandomOptionData(data.OptionID);
        return OptionBase + OptionLvBonus * (this.level.level < PlayerLevel ? this.level.level : PlayerLevel);
    }

    public StatusValues OptionValuesPer(int PlayerLevel)
    {
        if (OptionIDList != null) TrySetEquipmentRandomOptionData(data.OptionID);
        return OptionPerBase + OptionPerLvBonus * (this.level.level < PlayerLevel ? this.level.level : PlayerLevel);
    }

    public void SetLevel(Level lv)
    {
        level = lv;
        _UI.SetData(data, this);
    }
}
