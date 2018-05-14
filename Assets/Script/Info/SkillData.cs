using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class SkillData : DataInterface
{
    public override string XmlFilePath() { return "SkillInfo"; }
    public override string XmlNodeName() { return "Skill"; }
    public override void Init()
    {
        Name = GetElementInnerTextByTag("Name");
        Info = GetElementInnerTextByTag("Info");
    }
    public SkillData(string id)
    {
        ID = id;
    }

    public string Name = "";
    public string Info = "";
}
