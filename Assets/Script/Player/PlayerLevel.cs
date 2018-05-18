using UnityEngine;
using System.Collections;

public class PlayerLevel : MonoBehaviour
{
    public Level level;
    public int GetIntLv()
    {
        return level.level;
    }
}


[System.Serializable]
public class Level
{
    public int level = 1;
    public int sumExp = 0;
    public ExpData.EXPTYPE type = ExpData.EXPTYPE.Player;

    public Level(ExpData.EXPTYPE type,int lv)
    {
        this.type = type;
        this.level = lv;
        this.sumExp = ExpDataManager.Get.GetExpStart(lv, type);
    }

    public Level(int sum, ExpData.EXPTYPE type)
    {
        this.type = type;
        this.level = ExpDataManager.Get.GetLevel(sum, type);
        this.sumExp = sum;
    }

    public Level()
    {
    }

    public Level(Level lv)
    {
        this.level = lv.level;
        this.sumExp = lv.sumExp;
        this.type = lv.type;
    }

    public float CurruntExp { get { return sumExp - ExpDataManager.Get.GetExpStart(level, type); } }
    public float CurruntExpMax { get { return ExpDataManager.Get.GetExpData(level, type).NeedToUp; } }

    static public Level operator +(Level lv, int exp)
    {
        return ExpDataManager.Get.GetExpUp(lv, exp);
    }
}