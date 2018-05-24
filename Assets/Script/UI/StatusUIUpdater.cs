using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatusUIUpdater : MonoBehaviour
{
    public PlayerStat player;
    PlayerEquipment _PlayerEquip;
    PlayerEquipment PlayerEquip
    {
        get
        {
            if (_PlayerEquip == null)
                _PlayerEquip = player.GetComponent<PlayerEquipment>();
            return _PlayerEquip;
        }
    }
            
    public UIPositionUpdater statUI;
    public StatusValues BeforeStat;
    public StatusValues NowStat;
    public StatusValues BonusStatInt;
    public StatusValues BonusStatPer;
    public StatusValues StatSum;

    List<StatusUILine> list = new List<StatusUILine>();
    float time = -1f;

    string UIBeforeToggle = "";
    // Use this for initialization
    void Init()
    {
        for(int i=0;i< transform.childCount;i++)
        {
            StatusUILine tmp = transform.GetChild(i).GetComponent<StatusUILine>();
            if (tmp != null)
            {
                list.Add(tmp);
                tmp.Parent = this;
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (statUI.toggle == "Stat" && UIBeforeToggle != "Stat")
            StatReset();
        UIBeforeToggle = statUI.toggle;
    }

    public void StatAdd(StatusValues.VALUE valueType, int val)
    {
        if (NowStat.StatPoint < val) val = NowStat.StatPoint;
        if (val == 0) return;
        NowStat.StatPoint -= val;
        NowStat.AddValue(valueType, val);
        UpdateStat();
    }

    public void OnEnable()
    {
        StatReset();
    }

    public void UpdateBonusStat()
    {
        BonusStatInt = PlayerEquip.GetStatInt();
        BonusStatPer = PlayerEquip.GetStatPer();
    }

    public void UpdateStat()
    {
        StatSum = (NowStat + BonusStatInt) * BonusStatPer;
        StatusValues v1;
        StatusValues v2;
        StatusValues v3;
        StatusValues.GetStatusValuesForUI(NowStat, BonusStatInt, BonusStatPer, out v1, out v2, out v3);
        if (list.Count == 0)
            Init();
        for (int i = 0; i < list.Count; i++)
            list[i].UpdateText(v1,v2,v3,StatSum);
    }

    public void StatSave()
    {
        player.statInt = new StatusValues(NowStat);
        player.StatUpdate();
    }

    public void StatReset()
    {
        BeforeStat = new StatusValues(player.statInt);
        NowStat = new StatusValues(BeforeStat);

        UpdateBonusStat();
        UpdateStat();
    }
}
