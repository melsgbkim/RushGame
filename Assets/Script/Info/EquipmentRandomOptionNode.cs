using UnityEngine;
using System.Collections;
using System.Xml;

[System.Serializable]
public class EquipmentRandomOptionNode
{
    public XmlElement SetLine
    {
        set
        {
            id = XMLUtil.GetAttributeValue(value, "id", 1);
            foreach (XmlElement node in value.ChildNodes)
            {
                StatusValues.VALUE valName = StatusValues.GetVALUETYPE(node.Name);

                statValue.SetValue(valName,XMLUtil.GetFloat(node.InnerText));
                statValueLevelBonus.SetValue(valName,XMLUtil.GetAttributeValue(node, "LvBonus", 0f));
            }
        }
    }
    public EquipmentRandomOptionNode(XmlElement node)
    {
        SetLine = node;
    }

    public int id = 0;
    public StatusValues statValueLevelBonus = new StatusValues();
    public StatusValues statValue = new StatusValues();
}
