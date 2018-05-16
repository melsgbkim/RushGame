﻿using UnityEngine;
using System.Collections;

public class ExpDataForEquipment : DataInterface
{
    public override string XmlFilePath() { return "ExpEquipmentInfo"; }
    public override string XmlNodeName() { return "Level"; }
    public override void Init()
    {
        NeedToUp = int.Parse(GetElementInnerTextByTag("NeedToUp"));
        Sum = int.Parse(GetElementInnerTextByTag("Sum"));
        LevelMax = isAble("LevelMax");
    }
    public ExpDataForEquipment(int lv)
    {
        ID = lv.ToString();
        Level = lv;
    }
    public ExpDataForEquipment(string id)
    {
        ID = id;
        Level = int.Parse(id);
    }

    public int Level = 0;
    public int NeedToUp = 0;
    public int Sum = 0;
    public bool LevelMax = false;
}