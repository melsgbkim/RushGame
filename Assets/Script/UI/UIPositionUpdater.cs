using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIPositionUpdater : MonoBehaviour
{
    public const string DefaultToggle = "Default";
    public string NowToggle = DefaultToggle;
    float progress = 0f;
    public bool end = true;
    public string toggle
    {
        get
        {
            return NowToggle;
        }
        set
        {
            if(NowToggle != value)
            {
                beforeData = GetData(NowToggle);
                NowToggle = (dataTable.Contains(value) ? value : DefaultToggle);
                StartUpdate();
            }
        }
    }

    Hashtable dataTable = new Hashtable();
    UIPositionData beforeData;

    public List<UIPositionData> PosDataList;
    public string name = "";
    public string ThisDataName = DefaultToggle;
    public UIPositionData.MOVETYPE NoInputType = UIPositionData.MOVETYPE.Nomal;
    public float SpeedToggle = 2f;

    // Use this for initialization
    void Start()
    {
        foreach(UIPositionData data in PosDataList)
            dataTable.Add(data.name, data);

        string checkKey = (ThisDataName == "" ? DefaultToggle : ThisDataName);
        if (dataTable.ContainsKey(checkKey) == false)
            dataTable.Add(checkKey, new UIPositionData(checkKey, transform.localPosition, transform.localScale, NoInputType));
        else
            SetByData(dataTable[DefaultToggle] as UIPositionData);
        beforeData = dataTable[DefaultToggle] as UIPositionData;
    }

    void StartUpdate()
    {
        end = false;
        progress = 0f;
    }

    public void UpdatePos()
    {
        progress += SpeedToggle * Time.deltaTime;

        if (progress > 1f) end = true;
        if (progress > 1f) progress = 1f;

        SetByData(  (GetData(toggle) as UIPositionData).Progress(beforeData, progress)  );
    }

    UIPositionData GetData(string key)
    {
        if (dataTable.ContainsKey(key))
            return dataTable[key] as UIPositionData;
        return dataTable[DefaultToggle] as UIPositionData;
    }

    public void SetByData(UIPositionData data)
    {
        transform.localPosition = data.pos;
        transform.localScale = data.scale;
    }
}


[System.Serializable]
public class UIPositionData
{
    public UIPositionData() { }
    public UIPositionData(string name,Vector3 pos,Vector2 scale, MOVETYPE MoveType)
    {
        this.name = name; 
        this.pos = pos;
        this.scale = scale;
        this.MoveType = MoveType;
    }
    public string name = "";
    public Vector3 pos = Vector3.zero;
    public Vector2 scale = Vector2.one;
    public MOVETYPE MoveType = MOVETYPE.Data;

    public enum MOVETYPE
    {
        Nomal,
        SlowToFaster,
        FastToSlower,
        Teleport,
        Data
    }
    float SlowToFaster(float per) { return Mathf.Pow(per, 3); }
    float FastToSlower(float per) { return 1 - Mathf.Pow(1-per, 3); }
    float Nomal(float per) { return per; }
    float Teleport(float per) { return 1; }

    public UIPositionData Progress(UIPositionData from,float per)
    {
        per = GetPer(per);
        return new UIPositionData("result",
            per * pos   + (1 - per) * from.pos,
            per * scale + (1 - per) * from.scale,
            MOVETYPE.Data);
    }

    float GetPer(float per)
    {
        switch (MoveType)
        {
            case MOVETYPE.SlowToFaster: return SlowToFaster(per);
            case MOVETYPE.FastToSlower: return FastToSlower(per);
            case MOVETYPE.Teleport:     return Teleport(per);
        }
        return per;
    }
}