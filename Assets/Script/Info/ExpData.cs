using UnityEngine;
using System.Collections;

public class ExpData : DataInterface
{
    public override string XmlFilePath() { return ""; }
    public override string XmlNodeName() { return ""; }
    public override void Init()
    {
        Level = XMLUtil.GetAttributeValue(DataNode, "id", 1);
        NeedToUp = int.Parse(GetElementInnerTextByTag("NeedToUp"));
        Sum = int.Parse(GetElementInnerTextByTag("Sum"));
        LevelMax = isAble("LevelMax");
        maxInThisLevel = Sum + NeedToUp;
    }

    public enum EXPTYPE
    {
        Equipment,
        Player
    }

    public int Level = 0;
    public int NeedToUp = 0;
    public int Sum = 0;
    public int maxInThisLevel = 0;
    public bool LevelMax = false;

    public int InThisRange(int value)
    {
        if (value < this.Sum) return -1;
        else if (maxInThisLevel <= value) return 1;
        return 0;
    }
}