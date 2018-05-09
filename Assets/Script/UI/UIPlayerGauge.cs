using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIPlayerGauge : MonoBehaviour
{
    public PlayerGaugeData data;
    PlayerGaugeData beforeData;
    PlayerGaugeData nowData;

    public RectTransform bar;
    public RectTransform barBack;
    public RectTransform barBlack;
    public Text txt;

    float per = 0f;
    bool end = false;


    const float barStart = 20f;
    const float barMax = 680f;
    // Use this for initialization
    void Start()
    {
        NewPlayerGaugeData(new PlayerGaugeData(150,150,150));
    }

    // Update is called once per frame
    void Update()
    {
        if (end == false)
        {
            per += Time.deltaTime * 4f;
            if (per > 1f)
            {
                per = 1f;
                end = true;
            }

            nowData = data.Progress(beforeData, per);
            SetRectWidth(bar, nowData.BarPer() * barMax + barStart);
            SetRectWidth(barBack, nowData.BarBackPer() * barMax + barStart);
            txt.text = nowData.GetTxt();
        }

        if (Input.GetKeyDown(KeyCode.Z)) NewPlayerGaugeData(new PlayerGaugeData(Random.Range(10f, 150f), data.barPointBack, data.barPointMax));
        if (Input.GetKeyDown(KeyCode.X)) NewPlayerGaugeData(new PlayerGaugeData(Random.Range(10f, 150f), Random.Range(10f, 150f), data.barPointMax));
        if (Input.GetKeyDown(KeyCode.C)) NewPlayerGaugeData(new PlayerGaugeData(Random.Range(10f, 150f), Random.Range(10f, 150f), Random.Range(10f, 150f)));
    }

    void SetRectWidth(RectTransform t, float width)
    {
        t.sizeDelta = new Vector2(width, t.rect.height);
    }

    public void NewPlayerGaugeData(PlayerGaugeData next)
    {
        beforeData = (nowData == null ? data : nowData);
        data = next;
        per = 0f;
        end = false;
    }
}


[System.Serializable]
public class PlayerGaugeData
{
    public PlayerGaugeData(float barPoint,float barPointBack,float barPointMax)
    {
        this.barPoint = barPoint;
        this.barPointBack = barPointBack;
        this.barPointMax = barPointMax;
    }
    public float barPoint;
    public float barPointBack;
    public float barPointMax;

    public float BarPer() { return (barPoint / barPointMax > 1f ? 1f : (barPoint / barPointMax < 0f ? 0f : barPoint / barPointMax)); }
    public float BarBackPer() { return (barPointBack / barPointMax > 1f ? 1f : (barPointBack / barPointMax < 0f ? 0f : barPointBack / barPointMax)); }
    public string GetTxt() { return "" + Mathf.RoundToInt(barPoint) + " / " + Mathf.RoundToInt(barPointBack); }
    public PlayerGaugeData Progress(PlayerGaugeData from, float per)
    {
        per = 1 - Mathf.Pow(1 - per, 3);
        return new PlayerGaugeData(
            per * barPoint + (1 - per) * from.barPoint,
            per * barPointBack + (1 - per) * from.barPointBack,
            per * barPointMax + (1 - per) * from.barPointMax);
    }
}
