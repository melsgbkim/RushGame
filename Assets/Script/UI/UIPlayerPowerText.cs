using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UIPlayerPowerText : MonoBehaviour
{
    public GameObject player;
    Rigidbody2D rigid;
    PlayerStat stat;
    Text txt;
    // Use this for initialization
    void Start()
    {
        if(player != null)
        {
            rigid = player.GetComponent<Rigidbody2D>();
            stat = player.GetComponent<PlayerStat>();
        }
        txt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int Power10 = Mathf.RoundToInt(rigid.velocity.magnitude * stat.Stat.mas * 10);
        int Power = Power10 / 10;
        string powerUnderOne = "."+(Power10 % 10);
        if (Power >= 100) powerUnderOne = "";
        txt.text = "" + Power + powerUnderOne;
    }
}
