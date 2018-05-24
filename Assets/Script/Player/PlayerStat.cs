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
    public StatusValues statBonusInt = new StatusValues();
    public StatusValues statBonusPer = new StatusValues();
    StatusValues _statSum = new StatusValues();
    public StatusValues statSum
    {
        get { return _statSum; }
        set
        {
            _statSum = value;
            SetTime = Time.time;
            SetStat(_statSum);
        }
    }

    public void StatUpdate()
    {
        PlayerEquipment p = GetComponent<PlayerEquipment>();
        statBonusInt = p.GetStatInt();
        statBonusPer = p.GetStatPer();

        statSum = (statInt + statBonusInt) * statBonusPer;
    }

    public void StatSave()
    {
        if (statEditingNow == true)
        {
            statEditingNow = false;
            statSaved = true;
            statSum = statInt;
        }
    }

    public void StatReset()
    {
        if (statEditingNow == true && statSaved == false)
        {
            statEditingNow = false;
            statSaved = true;
            statSum = statBefore;
        }
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
        StatUpdate();
    }

    // Update is called once per frame
    void Update()
    {
    }
}