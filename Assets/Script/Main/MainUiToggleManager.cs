using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainUiToggleManager : MonoBehaviour
{
    string _NowOpenedUI = "";
    public string NowOpenedUI
    {
        get { return _NowOpenedUI; }
        set
        {
            for(int i=0;i<UIList.Count;i++)
            {
                if (UIList[i].name == _NowOpenedUI || UIList[i].name == value)
                {
                    if (UIList[i].end)
                        UIUpdateList.Add(UIList[i]);
                    UIList[i].toggle = (UIList[i].name == value);
                }
            }
            _NowOpenedUI = value;
        }
    }

    public void ToggleUIStat()      { NowOpenedUI="Stat";}
    public void ToggleUISkill()     { NowOpenedUI="Skill";}
    public void ToggleUIInven()     { NowOpenedUI="Inven";}
    public void ToggleUIUpgrade()   { NowOpenedUI="Upgrade";}
    public void ToggleUIQuest()     { NowOpenedUI="Quest";}
    public void ToggleUIOption()    { NowOpenedUI="Option";}

    public List<UIPositionUpdater> UIList = new List<UIPositionUpdater>();
    List<UIPositionUpdater> UIUpdateList = new List<UIPositionUpdater>();



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < UIUpdateList.Count; i++)
            UIUpdateList[i].UpdatePos();
        for (int i = 0; i < UIUpdateList.Count; )
        {
            if (UIUpdateList[i].end)
            {
                UIUpdateList.Remove(UIUpdateList[i]);
            }
            else
                i++;
        }
    }
}