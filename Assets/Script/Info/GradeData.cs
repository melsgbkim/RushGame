using UnityEngine;
using System.Collections;

public class GradeData : DataInterface
{
    public override string XmlFilePath() { return "GradeInfo"; }
    public override string XmlNodeName() { return "Grade"; }
    public override void Init()
    {
        Name = GetElementInnerTextByTag("Name");
        NameColor = GetColorByTag("NameColor");
        BackColor = GetColorByTag("BackColor");
    }
    public GradeData(string id)
    {
        ID = id;
    }

    public string Name = "";
    public Color NameColor;
    public Color BackColor;
}
