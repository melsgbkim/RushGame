using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StatusUILine : MonoBehaviour
{
    public PlayerStat player;
    public StatusValues.VALUE ValueType;
    public Text Name;
    public List<Text> ValueList;
    public List<Text> OperatorList;

    public StatusUIUpdater Parent;

    float StatusSettingTime = -1f;

    public enum TYPE
    {
        OneValue,//remains Stat Point, 
        ThreeValue,//AttackPoint
        FourValue,//Other STR,DEX,LUK,MAS
        UniqueValueLevel//Level
    }

    public TYPE type = TYPE.FourValue;
    // Use this for initialization
    void Start()
    {
        Name.text = ValueType.ToString();
    }

    public void StatAdd(int val)
    {
        Parent.StatAdd(ValueType, val);
    }

    // Update is called once per frame
    public void UpdateText(StatusValues v1, StatusValues v2, StatusValues v3, StatusValues v4)
    {
        switch(type)
        {
            case TYPE.OneValue:
                if (ValueList.Count < 1) break;
                ValueList[0].text = v1.GetValue(ValueType).ToString();
                break;
            case TYPE.ThreeValue:
                if (ValueList.Count < 3) break;
                ValueList[0].text = v2.GetValue(ValueType).ToString();
                ValueList[1].text = v3.GetValue(ValueType).ToString();
                ValueList[2].text = v4.GetValue(ValueType).ToString();
                break;
            case TYPE.FourValue:
                if (ValueList.Count < 4) break;
                ValueList[0].text = v1.GetValue(ValueType).ToString();
                ValueList[1].text = v2.GetValue(ValueType).ToString();
                ValueList[2].text = v3.GetValue(ValueType).ToString();
                ValueList[3].text = v4.GetValue(ValueType).ToString();
                break;
            case TYPE.UniqueValueLevel:
                if (ValueList.Count < 1) break;
                ValueList[0].text = player.GetComponent<PlayerLevel>().GetIntLv().ToString();
                break;
                
        }

        
    }
}
