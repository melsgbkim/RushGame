using UnityEngine;
using System.Collections;

public class UIPositionUpdater : MonoBehaviour
{
    bool _toggle = false;
    float progress = 0f;
    public bool end = true;
    public bool toggle
    {
        get
        {
            return _toggle;
        }
        set
        {
            if(_toggle != value)
            {
                end = false;
                _toggle = value;
                progress = 0f;
            }
        }
    }


    public string name = "";
    public Vector3 PosToggleOFF;
    public Vector2 SizeToggleOFF = Vector2.one;
    public Vector3 PosToggleON;
    public Vector2 SizeToggleON = Vector2.one;
    public float SpeedToggle = 2f;
    public float SpeedTogglePow = 1f;
    // Use this for initialization
    void Start()
    {
        transform.localPosition = PosToggleOFF;
    }

    public void UpdatePos()
    {
        progress += SpeedToggle * Time.deltaTime;

        if (progress > 1f) end = true;
        if (progress > 1f) progress = 1f;

        float per = 0f;
        if (toggle) per = 1-Mathf.Pow(1-progress, SpeedTogglePow);
        else        per = 1-Mathf.Pow(progress, SpeedTogglePow);


        transform.localPosition = per * PosToggleON + (1 - per) * PosToggleOFF;
        transform.localScale = per * SizeToggleON + (1 - per) * SizeToggleOFF;
       
    }
}
