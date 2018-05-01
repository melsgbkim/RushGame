using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainItemPositionUpdater : MonoBehaviour
{
    public static MainItemPositionUpdater Get = null;
    public MainItemPositionUpdater()
    {
        if (Get == null)
            Get = this;
    }
    List<ItemPositionData> ItemPositionDataList = new List<ItemPositionData>();
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ItemPositionDataList.Count; i++)
            ItemPositionDataList[i].update(Time.deltaTime);
        for (int i = 0; i < ItemPositionDataList.Count;)
        {
            if (ItemPositionDataList[i].end)
            {
                ItemPositionDataList[i].item.EndItemMove(ItemPositionDataList[i].type);
                ItemPositionDataList.RemoveAt(i);
            }
            else
                break;
        }
    }

    public void AddList(ItemPositionData data)
    {
        ItemPositionDataList.Add(data);
    }
}

public class ItemPositionData
{
    public ItemPositionData(ItemInfo item, Vector2 posStart, Vector2 posEnd, string type, float height, float power = 1f)
    {
        this.item = item;
        this.posStart = posStart;
        this.posEnd = posEnd;
        this.height = height;
        this.type = type;
        this.power = power;
    }
    public ItemPositionData(ItemInfo item, Vector2 posStart, Transform target, string type, float height, float power = 1f, float timeMax = 0.6f)
    {
        this.item = item;
        this.posStart = posStart;
        this.target = target;
        this.posEnd = target.localPosition;
        this.height = height;
        this.type = type;
        this.power = power;
        this.timeMax = timeMax;
    }
    public ItemInfo item;
    public Vector2 posStart;
    public Vector2 posEnd;
    public Transform target = null;
    public string type;
    public float height;
    public float timeMax = 0.6f;
    public float timeNow = 0f;
    public float power = 1f;
    public bool end = false;

    public void update(float deltaTime)
    {
        if(target != null)
            this.posEnd = target.localPosition;
        timeNow += deltaTime;
        float per = timeNow / timeMax;
        if (per > 1)
        {
            end = true;
            per = 1;
        }
        per = Mathf.Pow(per, power);
        float heightPer = per * 2 - 1f;
        float z = height * (1 - Mathf.Pow(heightPer, 2));
        Vector2 tmpPos = posStart * (1 - per) + posEnd * per;
        item.transform.localPosition = new Vector3(tmpPos.x, tmpPos.y, -z);
    }
}