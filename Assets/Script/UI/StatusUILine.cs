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
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void UpdateText()
    {
        Name.text = ValueType.ToString();
        for (int i=0;i< ValueList.Count;i++)
            ValueList[i].text = player.Stat.GetValue(ValueType).ToString();
    }
}
