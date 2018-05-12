using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIPlayerGauge : UIGaugeUpdater
{
    // Use this for initialization
    void Start()
    {
        barStart = 20f;
        barMax = 680f;
}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) NewGaugeData(new UIGaugeData(Random.Range(10f, 150f), data.barPointBack, data.barPointMax));
        if (Input.GetKeyDown(KeyCode.X)) NewGaugeData(new UIGaugeData(Random.Range(10f, 150f), Random.Range(10f, 150f), data.barPointMax));
        if (Input.GetKeyDown(KeyCode.C)) NewGaugeData(new UIGaugeData(Random.Range(10f, 150f), Random.Range(10f, 150f), Random.Range(10f, 150f)));
    }
}