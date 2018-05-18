using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExpDataManager
{
    static ExpDataManager _get = null;
    public static ExpDataManager Get
    {
        get
        {
            if (_get == null)
                _get = new ExpDataManager();
            return _get;
        }
    }

    List<ExpData> PlayerExpList = new List<ExpData>();
    List<ExpData> EquipmentExpList = new List<ExpData>();

    public ExpDataManager()
    {
        int level = 1;
        bool loop = true;
        while (loop)
        {
            ExpDataForPlayer tmp = new ExpDataForPlayer(level);
            if (tmp.LevelMax)
                loop = false;
            PlayerExpList.Add(tmp);
            level++;
        }

        level = 1;
        loop = true;
        while (loop)
        {
            ExpDataForEquipment tmp = new ExpDataForEquipment(level);
            if (tmp.LevelMax)
                loop = false;
            EquipmentExpList.Add(tmp);
            level++;
        }
    }

    List<ExpData> GetList(ExpData.EXPTYPE type)
    {
        if (type == ExpData.EXPTYPE.Player) return PlayerExpList;
        if (type == ExpData.EXPTYPE.Equipment) return EquipmentExpList;
        return null;
    }

    public int GetLevel(Level lv)
    {
        int result = GetLevel(lv.sumExp, lv.type, lv.level);
        return result;
    }

    public int GetLevel(int sum, ExpData.EXPTYPE type, int startLevel = -1)
    {
        List<ExpData> list = GetList(type);
        int min = 0;
        int max = list.Count - 1;
        int mid = (int)((min + max) / 2f);
        if (startLevel != -1)
            mid = startLevel - 1;
        ExpData index = list[mid];
        while (index.InThisRange(sum) != 0)
        {
            if (mid == max || index.LevelMax) break;
            if (index.InThisRange(sum) < 0) max = mid - 1;
            else if (index.InThisRange(sum) > 0) min = mid + 1;
            mid = (int)((min + max) / 2f);
            index = list[mid];
        }
        return index.Level;
    }

    public ExpData GetExpData(int lv, ExpData.EXPTYPE type)
    {
        List<ExpData> list = GetList(type);
        lv -= 1;
        if (lv < 0) lv = 0;
        if (lv >= list.Count) lv = list.Count - 1;
        return list[lv];
    }
    public int GetExpStart(int lv, ExpData.EXPTYPE type)
    {
        List<ExpData> list = GetList(type);
        lv -= 1;
        if (lv < 0) lv = 0;
        if (lv >= list.Count) lv = list.Count-1;
        return list[lv].Sum;
    }
    public Level GetExpUp(Level before,int exp)
    {
        Level result = new Level(before);
        result.sumExp += exp;
        result.level = GetLevel(result);

        ExpData tmp = GetExpData(result.level, result.type);
        if (tmp.LevelMax)
            result.sumExp = tmp.Sum;
        return result;
    }
}
