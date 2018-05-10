﻿using UnityEngine;
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

[System.Serializable]
public class StatusValues
{
    public StatusValues() { }
    public StatusValues(StatusValues a)
    {
        maxHP = a.maxHP;
        maxSP = a.maxSP;
        str = a.str;
        dex = a.dex;
        luk = a.luk;
        mas = a.mas;
        incHP = a.incHP;
        incSP = a.incSP;
        startPower = a.startPower;
        power = a.power;
        footSpeed = a.footSpeed;
        attackSpeed = a.attackSpeed;
        evasion = a.evasion;
        critical = a.critical;
        attackPoint = a.attackPoint;
        damage = a.damage;
        StatPoint = a.StatPoint;
}
    public string name = "";
    public enum TYPE
    {
        integer,
        percent,

    }
    public TYPE type = TYPE.integer;

    public enum VALUE
    {
        level,
        HP,
        SP,
        STR,
        DEX,
        LUK,
        MAS,
        HPregen,
        SPregen,
        StartPower,
        Power,
        FootSpeed,
        AttackSpeed,
        Evasion,
        Critical,
        AttackPoint,
        Damage,

        //Add
        StatPoint
    }
    public bool AddValue(VALUE v,int val)
    {
        switch (v)
        {
            case VALUE.HP: maxHP += val; return true;
            case VALUE.SP: maxSP += val; return true;
            case VALUE.STR: str += val; return true;
            case VALUE.DEX: dex += val; return true;
            case VALUE.LUK: luk += val; return true;
            case VALUE.MAS: mas += val; return true;
        }
        return false;
    }
    public float GetValue(VALUE v)
    {
        switch(v)
        {
            case VALUE.HP:         return maxHP;
            case VALUE.SP:         return maxSP;
            case VALUE.STR:        return str;
            case VALUE.DEX:        return dex;
            case VALUE.LUK:        return luk;
            case VALUE.MAS:        return mas;
            case VALUE.HPregen:    return incHP;
            case VALUE.SPregen:    return incSP;
            case VALUE.StartPower: return startPower;
            case VALUE.Power:      return power;
            case VALUE.FootSpeed:  return footSpeed;
            case VALUE.AttackSpeed:return attackSpeed;
            case VALUE.Evasion:    return evasion;
            case VALUE.Critical:   return critical;
            case VALUE.AttackPoint:return attackPoint;
            case VALUE.Damage:     return damage;

            case VALUE.StatPoint: return StatPoint;
        }
        return 0f;
    }
    public float maxHP = 0f;
    public float maxSP = 0f;
    public float str = 0f;
    public float dex = 0f;
    public float luk = 0f;
    public float mas = 0f;
    public float incHP = 0f;
    public float incSP = 0f;
    public float startPower = 0f;
    public float power = 0f;
    public float footSpeed = 0f;
    public float attackSpeed = 0f;
    public float evasion = 0f;
    public float critical = 0f;
    public float attackPoint = 0f;
    public float damage = 0f;
    public int StatPoint = 0;

    public static StatusValues operator +(StatusValues a, StatusValues b)
    {
        StatusValues result = new StatusValues(a);
        if(a.type == TYPE.integer && b.type == TYPE.integer)
        {
            if(b.maxHP         !=0 )  result.maxHP        += b.maxHP        ;
            if(b.maxSP         !=0 )  result.maxSP        += b.maxSP        ;
            if(b.str           !=0 )  result.str          += b.str          ;
            if(b.dex           !=0 )  result.dex          += b.dex          ;
            if(b.luk           !=0 )  result.luk          += b.luk          ;
            if(b.mas           !=0 )  result.mas          += b.mas          ;
            if(b.incHP         !=0 )  result.incHP        += b.incHP        ;
            if(b.incSP         !=0 )  result.incSP        += b.incSP        ;
            if(b.startPower    !=0 )  result.startPower   += b.startPower   ;
            if(b.power         !=0 )  result.power        += b.power        ;
            if(b.footSpeed     !=0 )  result.footSpeed    += b.footSpeed    ;
            if(b.attackSpeed   !=0 )  result.attackSpeed  += b.attackSpeed  ;
            if(b.evasion       !=0 )  result.evasion      += b.evasion      ;
            if(b.critical      !=0 )  result.critical     += b.critical     ;
            if(b.attackPoint   !=0 )  result.attackPoint  += b.attackPoint  ;
            if(b.damage        !=0 )  result.damage       += b.damage       ;
            if(b.StatPoint     !=0 )  result.StatPoint    += b.StatPoint;
            result.type = TYPE.integer;
        }

        else if(a.type == TYPE.integer && b.type == TYPE.percent)
        {
            if(result.maxHP         !=0  &&  b.maxHP         !=0  )  result.maxHP        *= b.maxHP        ;
            if(result.maxSP         !=0  &&  b.maxSP         !=0  )  result.maxSP        *= b.maxSP        ;
            if(result.str           !=0  &&  b.str           !=0  )  result.str          *= b.str          ;
            if(result.dex           !=0  &&  b.dex           !=0  )  result.dex          *= b.dex          ;
            if(result.luk           !=0  &&  b.luk           !=0  )  result.luk          *= b.luk          ;
            if(result.mas           !=0  &&  b.mas           !=0  )  result.mas          *= b.mas          ;
            if(result.incHP         !=0  &&  b.incHP         !=0  )  result.incHP        *= b.incHP        ;
            if(result.incSP         !=0  &&  b.incSP         !=0  )  result.incSP        *= b.incSP        ;
            if(result.startPower    !=0  &&  b.startPower    !=0  )  result.startPower   *= b.startPower   ;
            if(result.power         !=0  &&  b.power         !=0  )  result.power        *= b.power        ;
            if(result.footSpeed     !=0  &&  b.footSpeed     !=0  )  result.footSpeed    *= b.footSpeed    ;
            if(result.attackSpeed   !=0  &&  b.attackSpeed   !=0  )  result.attackSpeed  *= b.attackSpeed  ;
            if(result.evasion       !=0  &&  b.evasion       !=0  )  result.evasion      *= b.evasion      ;
            if(result.critical      !=0  &&  b.critical      !=0  )  result.critical     *= b.critical     ;
            if(result.attackPoint   !=0  &&  b.attackPoint   !=0  )  result.attackPoint  *= b.attackPoint  ;
            if(result.damage        !=0  &&  b.damage        !=0  )  result.damage       *= b.damage       ;
            result.type = TYPE.integer;
        }

        else if(a.type == TYPE.percent && b.type == TYPE.percent)
        {
            if(result.maxHP         !=0  &&  b.maxHP         !=0  )  result.maxHP        = result.maxHP       * b.maxHP        ;
            if(result.maxSP         !=0  &&  b.maxSP         !=0  )  result.maxSP        = result.maxSP       * b.maxSP        ;
            if(result.str           !=0  &&  b.str           !=0  )  result.str          = result.str         * b.str          ;
            if(result.dex           !=0  &&  b.dex           !=0  )  result.dex          = result.dex         * b.dex          ;
            if(result.luk           !=0  &&  b.luk           !=0  )  result.luk          = result.luk         * b.luk          ;
            if(result.mas           !=0  &&  b.mas           !=0  )  result.mas          = result.mas         * b.mas          ;
            if(result.incHP         !=0  &&  b.incHP         !=0  )  result.incHP        = result.incHP       * b.incHP        ;
            if(result.incSP         !=0  &&  b.incSP         !=0  )  result.incSP        = result.incSP       * b.incSP        ;
            if(result.startPower    !=0  &&  b.startPower    !=0  )  result.startPower   = result.startPower  * b.startPower   ;
            if(result.power         !=0  &&  b.power         !=0  )  result.power        = result.power       * b.power        ;
            if(result.footSpeed     !=0  &&  b.footSpeed     !=0  )  result.footSpeed    = result.footSpeed   * b.footSpeed    ;
            if(result.attackSpeed   !=0  &&  b.attackSpeed   !=0  )  result.attackSpeed  = result.attackSpeed * b.attackSpeed  ;
            if(result.evasion       !=0  &&  b.evasion       !=0  )  result.evasion      = result.evasion     * b.evasion      ;
            if(result.critical      !=0  &&  b.critical      !=0  )  result.critical     = result.critical    * b.critical     ;
            if(result.attackPoint   !=0  &&  b.attackPoint   !=0  )  result.attackPoint  = result.attackPoint * b.attackPoint  ;
            if(result.damage        !=0  &&  b.damage        !=0  )  result.damage       = result.damage      * b.damage       ;
            result.type = TYPE.percent;
        }


        return result;
    }
}