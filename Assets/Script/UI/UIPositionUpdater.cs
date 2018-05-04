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
                _toggle = value;
                StartUpdate();
            }
        }
    }


    public string name = "";
    public bool NoInput = false;
    public Vector3 PosToggleOFF;
    public Vector2 SizeToggleOFF = Vector2.one;
    public Vector3 PosToggleON;
    public Vector2 SizeToggleON = Vector2.one;
    public float SpeedToggle = 2f;
    public float SpeedTogglePow = 3f;
    // Use this for initialization
    void Start()
    {
        
        if(NoInput)
        {
            PosToggleOFF = PosToggleON = transform.localPosition;
            SizeToggleOFF = SizeToggleON = transform.localScale;
            NoInput = false;
        }
        else
        {
            transform.localPosition = PosToggleOFF;
            transform.localScale = SizeToggleOFF;
        }
    }

    void StartUpdate()
    {
        end = false;
        progress = 0f;
    }

    public void SetNextPos(Vector3 nextPos, Vector2 NextSize)
    {
        PosToggleOFF = PosToggleON;
        SizeToggleOFF = SizeToggleON;
        PosToggleON = nextPos;
        SizeToggleON = NextSize;
        StartUpdate();
    }
    public void SetNextPos(Vector3 nextPos)
    {
        PosToggleOFF = PosToggleON;
        SizeToggleOFF = SizeToggleON;
        PosToggleON = nextPos;
        StartUpdate();
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
