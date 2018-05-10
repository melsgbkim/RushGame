using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatusUIUpdater : MonoBehaviour
{
    public PlayerStat player;
    public UIPositionUpdater statUI;

    List<StatusUILine> list = new List<StatusUILine>();
    float time = -1f;

    string UIBeforeToggle = "";
    // Use this for initialization
    void Start()
    {
        for(int i=0;i< transform.childCount;i++)
        {
            StatusUILine tmp = transform.GetChild(i).GetComponent<StatusUILine>();
            if (tmp != null)
                list.Add(tmp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.SetTime != time)
        {
            time = player.SetTime;
            for(int i=0;i< list.Count;i++)
                list[i].UpdateText();
        }
        if (UIBeforeToggle == "Stat" && statUI.toggle != "Stat" && player.statEditingNow)
            player.StatReset();
        UIBeforeToggle = statUI.toggle;
    }


    public void StatSave()
    {
        player.StatSave();
    }

    public void StatReset()
    {
        player.StatReset();
    }
}
