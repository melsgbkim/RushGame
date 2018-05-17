using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class EquipmentRandomOptionData : DataInterface
{
    public override string XmlFilePath() { return "EquipmentRandomOptionInfo"; }
    public override string XmlNodeName() { return "Option"; }
    public override void Init()
    {
        foreach(XmlElement node in DataNode.ChildNodes)
        {
            EquipmentRandomOptionNode tmp = new EquipmentRandomOptionNode(node);
            NodeList.Add(tmp);
        }
    }
    public EquipmentRandomOptionData(string id)
    {
        ID = id;
    }

    public List<EquipmentRandomOptionNode> NodeList = new List<EquipmentRandomOptionNode>();

    public List<EquipmentRandomOptionNode> GetRandomOptionList(int cost)
    {
        List<EquipmentRandomOptionNode> result = new List<EquipmentRandomOptionNode>();
        for(int i=0;i<cost;i++)
        {
            int RandIndex = Random.Range(0, NodeList.Count);
            result.Add(NodeList[RandIndex]);
        }
        return result;
    }
}