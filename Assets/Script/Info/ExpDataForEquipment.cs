using UnityEngine;
using System.Collections;

public class ExpDataForEquipment : ExpData
{
    public override string XmlFilePath() { return "ExpEquipmentInfo"; }
    public override string XmlNodeName() { return "Level"; }

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
}
