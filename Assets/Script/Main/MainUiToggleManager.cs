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
            UpadteTownUI(value);
            updateButton(value);
            UpdateFieldUI(value);

            _NowOpenedUI = value;
        }
    }

    void UpadteTownUI(string value)
    {
        for (int i = 0; i < UIList.Count; i++)
        {
            if (UIList[i].name == _NowOpenedUI || UIList[i].name == value)
            {
                if (UIList[i].end)
                    UIUpdateList.Add(UIList[i]);
                UIList[i].toggle = value;
            }
        }
    }

    void updateButton(string value)
    {
        string toggleForButton = "";
        if (value == "PlayerInField")   toggleForButton = "PlayerInField";
        else if (value != "")           toggleForButton = "OpenWindow";
        else                            toggleForButton = "";
        for (int i = 0; i < ButtonList.Count; i++)  ButtonList[i].toggle = toggleForButton;
        UIUpdateList.AddRange(ButtonList);
    }

    void UpdateFieldUI(string value)
    {
        string toggle = "";
        if (value == "PlayerInField")   toggle = "PlayerInField";
        else                            toggle = "Default";
        for (int i = 0; i < FieldUIList.Count; i++) FieldUIList[i].toggle = toggle;
        UIUpdateList.AddRange(FieldUIList);
    }

    public void ToggleUIClose()     { if (nowAreaUiType == "TownUI") NowOpenedUI ="";}
    public void ToggleUIStat()      { if (nowAreaUiType == "TownUI") NowOpenedUI="Stat";}
    public void ToggleUISkill()     { if (nowAreaUiType == "TownUI") NowOpenedUI="Skill";}
    public void ToggleUIInven()     { if (nowAreaUiType == "TownUI") NowOpenedUI="Inven";}
    public void ToggleUIUpgrade()   { if (nowAreaUiType == "TownUI") NowOpenedUI="Upgrade";}
    public void ToggleUIQuest()     { if (nowAreaUiType == "TownUI") NowOpenedUI="Quest";}
    public void ToggleUIOption()    { if (nowAreaUiType == "TownUI") NowOpenedUI ="Option";}

    public List<UIPositionUpdater> UIList = new List<UIPositionUpdater>();
    List<UIPositionUpdater> UIUpdateList = new List<UIPositionUpdater>();

    public List<UIPositionUpdater> ButtonList;
    public Transform Player;

    public List<UIPositionUpdater> FieldUIList;

    string beforeAreaUiType = "";
    string nowAreaUiType = "";
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
        CheckUITypeInArea();
    }



    void CheckUITypeInArea()
    {
        beforeAreaUiType = nowAreaUiType;
        nowAreaUiType = AreaCheckerWhereInPlayer.Get.GetAreaUITypePos(Player.localPosition);
        if (nowAreaUiType == "TownUI" && nowAreaUiType != beforeAreaUiType) NowOpenedUI = "";
        if (nowAreaUiType == "FieldUI" && nowAreaUiType != beforeAreaUiType) NowOpenedUI = "PlayerInField";
    }
}