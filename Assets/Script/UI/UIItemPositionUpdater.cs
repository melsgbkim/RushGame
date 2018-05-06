using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UIItemPositionUpdater : MonoBehaviour
{
    UIPositionData beforeData = new UIPositionData();
    UIPositionData NextData = new UIPositionData();

    float progress = 0f;
    public string name;
    public float MoveSpeed = 2f;
    public bool end = false;
    public UIPositionData.MOVETYPE type;
    public bool isCategory = false;

    public void Start()
    {
        if (isCategory)
        {
            NextData.pos = beforeData.pos = transform.localPosition;
            NextData.scale = beforeData.scale = transform.localScale;
        }
    }

    public void SetNextPos(Vector3 next)
    {
        SetNextData(new UIPositionData("Next",next, beforeData.scale, type));
    }

    public void SetNextData(UIPositionData next)
    {
        beforeData = NextData;
        NextData = next;
        progress = 0f;
        end = false;
    }

    public void UpdatePos()
    {
        if (end) return;

        progress += MoveSpeed * Time.deltaTime;

        if (progress > 1f) end = true;
        if (progress > 1f) progress = 1f;

        SetByData(NextData.Progress(beforeData, progress));
    }

    public void SetByData(UIPositionData data)
    {
        transform.localPosition = data.pos;
        transform.localScale = data.scale;
    }
}
