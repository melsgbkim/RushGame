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
        if (player.StatAdd(ValueType, val) == false)
            print("bug");
    }

    // Update is called once per frame
    public void UpdateText()
    {
        switch(type)
        {
            case TYPE.OneValue:
                if (ValueList.Count < 1) break;
                ValueList[0].text = player.Stat.GetValue(ValueType).ToString();
                break;
            case TYPE.ThreeValue:
                if (ValueList.Count < 3) break;
                ValueList[0].text = player.Stat.GetValue(ValueType).ToString();
                ValueList[1].text = "0";
                ValueList[2].text = player.Stat.GetValue(ValueType).ToString();
                break;
            case TYPE.FourValue:
                if (ValueList.Count < 4) break;
                ValueList[0].text = player.Stat.GetValue(ValueType).ToString();
                ValueList[1].text = "0";
                ValueList[2].text = "0";
                ValueList[3].text = player.Stat.GetValue(ValueType).ToString();
                break;
            case TYPE.UniqueValueLevel:
                if (ValueList.Count < 1) break;
                ValueList[0].text = player.GetComponent<PlayerLevel>().GetIntLv().ToString();
                break;
                
        }

        
    }
}
