using UnityEngine;
using System.Collections;

public class PlayerStat : MonoBehaviour
{
    public bool Set = false;
    public float SetTime = 0f;
    public bool statEditingNow = false;
    public bool statSaved = true;
    StatusValues statBefore = new StatusValues();
    public StatusValues statInt;
    public StatusValues Stat
    {
        get { return statInt; }
        set
        {
            statInt = new StatusValues(value);
            SetTime = Time.time;
            SetChildValue(statInt);
            SetStat(statInt);
        }
    }

    public bool StatAdd(StatusValues.VALUE valueType,int val)
    {
        if(statEditingNow == false)
        {
            statEditingNow = true;
            statSaved = false;
            statBefore = new StatusValues() + statInt;
        }
        if (statInt.StatPoint < val)    val = statInt.StatPoint;
        if (val == 0) return false;
        statInt.StatPoint -= val;
        bool result = statInt.AddValue(valueType, val);
        if (result == true)
        {
            Stat = statInt;
        }
        return result;
    }

    public void StatSave()
    {
        if (statEditingNow == true)
        {
            statEditingNow = false;
            statSaved = true;
            Stat = statInt;
        }
    }

    public void StatReset()
    {
        if (statEditingNow == true && statSaved == false)
        {
            statEditingNow = false;
            statSaved = true;
            Stat = statBefore;
        }
    }

    void SetChildValue(StatusValues v)
    {
        v.startPower = v.str * 2f * v.mas;
        v.power = v.str * 2f;
        v.footSpeed = v.dex / (2f * v.mas);
        v.attackSpeed = v.dex / (2f * v.mas);
        v.evasion = v.luk / v.mas;
        v.critical = v.luk / v.mas;
        v.damage = (v.str + v.dex) * v.mas * v.attackPoint;
    }

    void SetStat(StatusValues result)
    {
        GetComponent<PlayerMove>().MovePower = result.power;
        GetComponent<PlayerMove>().MoveRepeatTime = 1f / result.footSpeed;
        GetComponent<Rigidbody2D>().mass = result.mas;
    }
    // Use this for initialization
    void Start()
    {
        Stat = statInt;
    }

    // Update is called once per frame
    void Update()
    {
        if(Set)
        {
            Set = false;
            Stat = statInt;
        }
    }
}