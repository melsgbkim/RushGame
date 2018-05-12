using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIGaugeUpdater : MonoBehaviour
{
    public UIGaugeData data;
    UIGaugeData beforeData;
    UIGaugeData nowData;

    public RectTransform bar = null;
    public RectTransform barBack = null;
    public RectTransform barBlack = null;
    public Text txt = null;

    float per = 0f;
    bool end = false;


    public float barStart = 20f;
    public float barMax = 380f;
    // Use this for initialization
    void Start()
    {
        NewGaugeData(new UIGaugeData(150, 150, 150));
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
            if(txt != null)txt.text = nowData.GetTxt();
        }
    }

    void SetRectWidth(RectTransform t, float width)
    {
        if (t == null) return;
        t.sizeDelta = new Vector2(width, t.rect.height);
    }

    public void NewGaugeData(UIGaugeData next)
    {
        beforeData = (nowData == null ? data : nowData);
        data = next;
        per = 0f;
        end = false;
    }
}
