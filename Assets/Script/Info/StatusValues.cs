﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class StatusValues
{
    public StatusValues() { }
    public StatusValues(TYPE type)
    {
        this.type = type;
        if (type == TYPE.integer)
        {
            maxHP = maxSP = str = dex = luk = mas = 
            incHP = incSP = startPower = power = 
            footSpeed = attackSpeed = evasion = critical = 
            attackPoint = damage = StatPoint = 0;
        }
        else if (type == TYPE.percent)
        {
            maxHP = maxSP = str = dex = luk = mas =
            incHP = incSP = startPower = power =
            footSpeed = attackSpeed = evasion = critical =
            attackPoint = damage = StatPoint = 1;
        }
    }
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
        type = a.type;
    }
    public string name = "";
    public enum TYPE
    {
        integer,
        percent,

        Error = int.MaxValue
    }
    public TYPE type = TYPE.integer;

    public enum VALUE
    {
        Level,//Just String

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
        StatPoint,
        Error = int.MaxValue
    }
    public bool AddValue(VALUE v, float val)
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
        switch (v)
        {
            case VALUE.HP: return maxHP;
            case VALUE.SP: return maxSP;
            case VALUE.STR: return str;
            case VALUE.DEX: return dex;
            case VALUE.LUK: return luk;
            case VALUE.MAS: return mas;
            case VALUE.HPregen: return incHP;
            case VALUE.SPregen: return incSP;
            case VALUE.StartPower: return startPower;
            case VALUE.Power: return power;
            case VALUE.FootSpeed: return footSpeed;
            case VALUE.AttackSpeed: return attackSpeed;
            case VALUE.Evasion: return evasion;
            case VALUE.Critical: return critical;
            case VALUE.AttackPoint: return attackPoint;
            case VALUE.Damage: return damage;

            case VALUE.StatPoint: return StatPoint;
        }
        return 0f;
    }

    public void SetValue(VALUE v,float value)
    {
        switch (v)
        {
            case VALUE.HP: maxHP = value; return;
            case VALUE.SP: maxSP = value; return;
            case VALUE.STR: str = value; return;
            case VALUE.DEX: dex = value; return;
            case VALUE.LUK: luk = value; return;
            case VALUE.MAS: mas = value; return;
            case VALUE.HPregen: incHP = value; return;
            case VALUE.SPregen: incSP = value; return;
            case VALUE.StartPower: startPower = value; return;
            case VALUE.Power: power = value; return;
            case VALUE.FootSpeed: footSpeed = value; return;
            case VALUE.AttackSpeed: attackSpeed = value; return;
            case VALUE.Evasion: evasion = value; return;
            case VALUE.Critical: critical = value; return;
            case VALUE.AttackPoint: attackPoint = value; return;
            case VALUE.Damage: damage = value; return;

            case VALUE.StatPoint: StatPoint = (int)(value); return;
        }
        return;
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
        if (a.type == TYPE.integer && b.type == TYPE.integer)
        {
            if (b.maxHP != 0) result.maxHP += b.maxHP;
            if (b.maxSP != 0) result.maxSP += b.maxSP;
            if (b.str != 0) result.str += b.str;
            if (b.dex != 0) result.dex += b.dex;
            if (b.luk != 0) result.luk += b.luk;
            if (b.mas != 0) result.mas += b.mas;
            if (b.incHP != 0) result.incHP += b.incHP;
            if (b.incSP != 0) result.incSP += b.incSP;
            if (b.startPower != 0) result.startPower += b.startPower;
            if (b.power != 0) result.power += b.power;
            if (b.footSpeed != 0) result.footSpeed += b.footSpeed;
            if (b.attackSpeed != 0) result.attackSpeed += b.attackSpeed;
            if (b.evasion != 0) result.evasion += b.evasion;
            if (b.critical != 0) result.critical += b.critical;
            if (b.attackPoint != 0) result.attackPoint += b.attackPoint;
            if (b.damage != 0) result.damage += b.damage;
            if (b.StatPoint != 0) result.StatPoint += b.StatPoint;
            result.type = TYPE.integer;
        }

        else if (a.type == TYPE.integer && b.type == TYPE.percent)
        {
            if (result.maxHP != 0 && b.maxHP != 0) result.maxHP *= b.maxHP;
            if (result.maxSP != 0 && b.maxSP != 0) result.maxSP *= b.maxSP;
            if (result.str != 0 && b.str != 0) result.str *= b.str;
            if (result.dex != 0 && b.dex != 0) result.dex *= b.dex;
            if (result.luk != 0 && b.luk != 0) result.luk *= b.luk;
            if (result.mas != 0 && b.mas != 0) result.mas *= b.mas;
            if (result.incHP != 0 && b.incHP != 0) result.incHP *= b.incHP;
            if (result.incSP != 0 && b.incSP != 0) result.incSP *= b.incSP;
            if (result.startPower != 0 && b.startPower != 0) result.startPower *= b.startPower;
            if (result.power != 0 && b.power != 0) result.power *= b.power;
            if (result.footSpeed != 0 && b.footSpeed != 0) result.footSpeed *= b.footSpeed;
            if (result.attackSpeed != 0 && b.attackSpeed != 0) result.attackSpeed *= b.attackSpeed;
            if (result.evasion != 0 && b.evasion != 0) result.evasion *= b.evasion;
            if (result.critical != 0 && b.critical != 0) result.critical *= b.critical;
            if (result.attackPoint != 0 && b.attackPoint != 0) result.attackPoint *= b.attackPoint;
            if (result.damage != 0 && b.damage != 0) result.damage *= b.damage;
            result.type = TYPE.integer;
        }

        else if (a.type == TYPE.percent && b.type == TYPE.percent)
        {
            if (result.maxHP != 0 && b.maxHP != 0) result.maxHP = result.maxHP * b.maxHP;
            if (result.maxSP != 0 && b.maxSP != 0) result.maxSP = result.maxSP * b.maxSP;
            if (result.str != 0 && b.str != 0) result.str = result.str * b.str;
            if (result.dex != 0 && b.dex != 0) result.dex = result.dex * b.dex;
            if (result.luk != 0 && b.luk != 0) result.luk = result.luk * b.luk;
            if (result.mas != 0 && b.mas != 0) result.mas = result.mas * b.mas;
            if (result.incHP != 0 && b.incHP != 0) result.incHP = result.incHP * b.incHP;
            if (result.incSP != 0 && b.incSP != 0) result.incSP = result.incSP * b.incSP;
            if (result.startPower != 0 && b.startPower != 0) result.startPower = result.startPower * b.startPower;
            if (result.power != 0 && b.power != 0) result.power = result.power * b.power;
            if (result.footSpeed != 0 && b.footSpeed != 0) result.footSpeed = result.footSpeed * b.footSpeed;
            if (result.attackSpeed != 0 && b.attackSpeed != 0) result.attackSpeed = result.attackSpeed * b.attackSpeed;
            if (result.evasion != 0 && b.evasion != 0) result.evasion = result.evasion * b.evasion;
            if (result.critical != 0 && b.critical != 0) result.critical = result.critical * b.critical;
            if (result.attackPoint != 0 && b.attackPoint != 0) result.attackPoint = result.attackPoint * b.attackPoint;
            if (result.damage != 0 && b.damage != 0) result.damage = result.damage * b.damage;
            result.type = TYPE.percent;
        }


        return result;
    }
    public static StatusValues operator *(StatusValues a, float b)
    {
        StatusValues result = new StatusValues(a);
        if (result.type == TYPE.integer)
        {
            result.maxHP *= b;
            result.maxSP *= b;
            result.str *= b;
            result.dex *= b;
            result.luk *= b;
            result.mas *= b;
            result.incHP *= b;
            result.incSP *= b;
            result.startPower *= b;
            result.power *= b;
            result.footSpeed *= b;
            result.attackSpeed *= b;
            result.evasion *= b;
            result.critical *= b;
            result.attackPoint *= b;
            result.damage *= b;
        }
        else if (result.type == TYPE.percent)
        {
            result.maxHP        = (result.maxHP       -1)* b+1;
            result.maxSP        = (result.maxSP       -1)* b+1;
            result.str          = (result.str         -1)* b+1;
            result.dex          = (result.dex         -1)* b+1;
            result.luk          = (result.luk         -1)* b+1;
            result.mas          = (result.mas         -1)* b+1;
            result.incHP        = (result.incHP       -1)* b+1;
            result.incSP        = (result.incSP       -1)* b+1;
            result.startPower   = (result.startPower  -1)* b+1;
            result.power        = (result.power       -1)* b+1;
            result.footSpeed    = (result.footSpeed   -1)* b+1;
            result.attackSpeed  = (result.attackSpeed -1)* b+1;
            result.evasion      = (result.evasion     -1)* b+1;
            result.critical     = (result.critical    -1)* b+1;
            result.attackPoint  = (result.attackPoint -1)* b+1;
            result.damage       = (result.damage      -1)* b+1;
        }
        return result;
    }

    public static StatusValues operator *(StatusValues a, StatusValues b)
    {
        StatusValues v = new StatusValues(a);
        v.maxHP        *= b.maxHP        ;
        v.maxSP        *= b.maxSP        ;
        v.str          *= b.str          ;
        v.dex          *= b.dex          ;
        v.luk          *= b.luk          ;
        v.mas          *= b.mas          ;
        v.incHP        *= b.incHP        ;
        v.incSP        *= b.incSP        ;
        v.attackPoint  *= b.attackPoint  ;

        v.startPower   += v.str * 2f * v.mas;
        v.power        += v.str * 2f;
        v.footSpeed    += v.dex / (2f * v.mas);
        v.attackSpeed  += v.dex / (2f * v.mas);
        v.evasion      += v.luk / v.mas;
        v.critical     += v.luk / v.mas;
        v.damage       += (v.str + v.dex) * v.mas * v.attackPoint;

        v.startPower   *= b.startPower   ;
        v.power        *= b.power        ;
        v.footSpeed    *= b.footSpeed    ;
        v.attackSpeed  *= b.attackSpeed  ;
        v.evasion      *= b.evasion      ;
        v.critical     *= b.critical     ;
        v.damage       *= b.damage       ;
        return v;
    }

    public static void GetStatusValuesForUI(StatusValues intStat, StatusValues bonusStat, StatusValues perStat, out StatusValues v1, out StatusValues v2, out StatusValues v3)
    {
        v1 = new StatusValues();
        v2 = new StatusValues(bonusStat);
        v3 = new StatusValues();
        StatusValues v4 = new StatusValues();
        v1.StatPoint    = intStat.StatPoint  ;
        v1.maxHP        = intStat.maxHP      ;
        v1.maxSP        = intStat.maxSP      ;
        v1.str          = intStat.str        ;
        v1.dex          = intStat.dex        ;
        v1.luk          = intStat.luk        ;
        v1.mas          = intStat.mas        ;
        v1.incHP        = intStat.incHP      ;
        v1.incSP        = intStat.incSP      ;
        v1.attackPoint  = intStat.attackPoint;

        v3.maxHP        = (v1.maxHP      +v2.maxHP      )*(perStat.maxHP      -1);
        v3.maxSP        = (v1.maxSP      +v2.maxSP      )*(perStat.maxSP      -1);
        v3.str          = (v1.str        +v2.str        )*(perStat.str        -1);
        v3.dex          = (v1.dex        +v2.dex        )*(perStat.dex        -1);
        v3.luk          = (v1.luk        +v2.luk        )*(perStat.luk        -1);
        v3.mas          = (v1.mas        +v2.mas        )*(perStat.mas        -1);
        v3.incHP        = (v1.incHP      +v2.incHP      )*(perStat.incHP      -1);
        v3.incSP        = (v1.incSP      +v2.incSP      )*(perStat.incSP      -1);
        v3.attackPoint  = (v1.attackPoint+v2.attackPoint)*(perStat.attackPoint-1);

        v4.maxHP        = (v1.maxHP      +v2.maxHP      )*(perStat.maxHP      );
        v4.maxSP        = (v1.maxSP      +v2.maxSP      )*(perStat.maxSP      );
        v4.str          = (v1.str        +v2.str        )*(perStat.str        );
        v4.dex          = (v1.dex        +v2.dex        )*(perStat.dex        );
        v4.luk          = (v1.luk        +v2.luk        )*(perStat.luk        );
        v4.mas          = (v1.mas        +v2.mas        )*(perStat.mas        );
        v4.incHP        = (v1.incHP      +v2.incHP      )*(perStat.incHP      );
        v4.incSP        = (v1.incSP      +v2.incSP      )*(perStat.incSP      );
        v4.attackPoint  = (v1.attackPoint+v2.attackPoint)*(perStat.attackPoint);
        
        v1.startPower   += v4.str *   2f    * v4.mas;
        v1.power        += v4.str *   2f;   
        v1.footSpeed    += v4.dex /  (2f    * v4.mas);
        v1.attackSpeed  += v4.dex /  (2f    * v4.mas);
        v1.evasion      += v4.luk / v4.mas;
        v1.critical     += v4.luk / v4.mas;
        v1.damage       +=(v4.str + v4.dex) * v4.mas * v4.attackPoint;

        v3.startPower   = (v1.startPower +v2.startPower )*(perStat.startPower -1);
        v3.power        = (v1.power      +v2.power      )*(perStat.power      -1);
        v3.footSpeed    = (v1.footSpeed  +v2.footSpeed  )*(perStat.footSpeed  -1);
        v3.attackSpeed  = (v1.attackSpeed+v2.attackSpeed)*(perStat.attackSpeed-1);
        v3.evasion      = (v1.evasion    +v2.evasion    )*(perStat.evasion    -1);
        v3.critical     = (v1.critical   +v2.critical   )*(perStat.critical   -1);
        v3.damage       = (v1.damage     +v2.damage     )*(perStat.damage     -1);
    }

    static Hashtable VALUETable = null;
    static void InitVALUETable()
    {
        VALUETable = new Hashtable();
        VALUETable.Add(VALUE.HP.ToString(), VALUE.HP);
        VALUETable.Add(VALUE.SP.ToString(), VALUE.SP);
        VALUETable.Add(VALUE.STR.ToString(), VALUE.STR);
        VALUETable.Add(VALUE.DEX.ToString(), VALUE.DEX);
        VALUETable.Add(VALUE.LUK.ToString(), VALUE.LUK);
        VALUETable.Add(VALUE.MAS.ToString(), VALUE.MAS);
        VALUETable.Add(VALUE.HPregen.ToString(), VALUE.HPregen);
        VALUETable.Add(VALUE.SPregen.ToString(), VALUE.SPregen);
        VALUETable.Add(VALUE.StartPower.ToString(), VALUE.StartPower);
        VALUETable.Add(VALUE.Power.ToString(), VALUE.Power);
        VALUETable.Add(VALUE.FootSpeed.ToString(), VALUE.FootSpeed);
        VALUETable.Add(VALUE.AttackSpeed.ToString(), VALUE.AttackSpeed);
        VALUETable.Add(VALUE.Evasion.ToString(), VALUE.Evasion);
        VALUETable.Add(VALUE.Critical.ToString(), VALUE.Critical);
        VALUETable.Add(VALUE.AttackPoint.ToString(), VALUE.AttackPoint);
        VALUETable.Add(VALUE.Damage.ToString(), VALUE.Damage);
    }
    public static VALUE GetVALUETYPE(string key)
    {
        if (VALUETable == null) InitVALUETable();
        if (VALUETable.ContainsKey(key)) return (VALUETable[key] as VALUE?).Value;
        return VALUE.Error;
    }

    static Hashtable TYPETable = null;
    static void InitTYPETable()
    {
        TYPETable = new Hashtable();
        VALUETable.Add(TYPE.integer.ToString(), TYPE.integer);
        VALUETable.Add(TYPE.percent.ToString(), TYPE.percent);
    }
    public static TYPE GetTYPE(string key)
    {
        if (TYPETable == null) InitTYPETable();
        if (TYPETable.ContainsKey(key)) return (TYPETable[key] as TYPE?).Value;
        return TYPE.Error;
    }
}