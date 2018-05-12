using UnityEngine;
using System.Collections;

[System.Serializable]
public class UIGaugeData
{
    public UIGaugeData(float barPoint, float barPointMax)
    {
        this.barPoint = barPoint;
        this.barPointBack = barPointMax;
        this.barPointMax = barPointMax;
    }
    public UIGaugeData(float barPoint, float barPointBack, float barPointMax)
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
    public UIGaugeData Progress(UIGaugeData from, float per)
    {
        per = 1 - Mathf.Pow(1 - per, 3);
        return new UIGaugeData(
            per * barPoint + (1 - per) * from.barPoint,
            per * barPointBack + (1 - per) * from.barPointBack,
            per * barPointMax + (1 - per) * from.barPointMax);
    }
}
