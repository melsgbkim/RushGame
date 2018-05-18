using UnityEngine;
using System.Collections;

public class ExpDataForPlayer : ExpData
{
    public override string XmlFilePath() { return "ExpPlayerInfo"; }
    public override string XmlNodeName() { return "Level"; }

    public ExpDataForPlayer(int lv)
    {
        ID = lv.ToString();
        Level = lv;
    }
    public ExpDataForPlayer(string id)
    {
        ID = id;
        Level = int.Parse(id);
    }
}
