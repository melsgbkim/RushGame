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
                ItemPositionDataList.RemoveAt(i);
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
    public ItemPositionData(Transform item, Vector2 posStart, Vector2 posEnd, float height)
    {
        this.item = item;
        this.posStart = posStart;
        this.posEnd = posEnd;
        this.height = height;
    }
    public Transform item;
    public Vector2 posStart;
    public Vector2 posEnd;
    public float height;
    public float timeMax = 0.6f;
    public float timeNow = 0f;
    public bool end = false;

    public void update(float deltaTime)
    {
        timeNow += deltaTime;
        float per = timeNow / timeMax;
        if (per > 1)
        {
            end = true;
            per = 1;
        }
        float heightPer = per * 2 - 1f;
        float z = height * (1 - Mathf.Pow(heightPer, 2));
        Vector2 tmpPos = posStart * (1 - per) + posEnd * per;
        item.localPosition = new Vector3(tmpPos.x, tmpPos.y, -z);
    }
}