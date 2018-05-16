using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class ItemData : DataInterface
{
    public override string XmlFilePath() { return "ItemInfo"; }
    public override string XmlNodeName() { return "Item"; }
    public override void Init()
    {
        Name = GetElementInnerTextByTag("Name");
        Category = GetElementInnerTextByTag("Category");
        Grade = GetElementInnerTextByTag("Grade");
        Info = GetElementInnerTextByTag("Info");
        Exp = int.Parse(GetElementInnerTextByTag("Exp"));
    }
    public ItemData(string id)
    {
        ID = id;
    }

    public string Name = "";
    public string Category = "";
    public string Grade = "";
    public string Info = "";
    public int Exp = 0;
}
