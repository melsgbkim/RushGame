using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml;


public class PopupItemInfo : MonoBehaviour
{
    Item _item;
    public Image Icon;
    public Text Grade;
    public Text Name;
    public Text Count;
    public Image Lock;
    public UIGaugeUpdater EXPBar;
    public Text Level;
    public Text OptionInfoText;
    public Text OptionInfoTextValue1;
    public Text OptionInfoTextValue2;
    public RectTransform OptionInfoArea;
    public Text InfoText;

    public int PlayerLevel = 1;

    int? customCount = null;
    public int? CustomCount
    {
        get { return customCount; }
        set
        {
            customCount = value;
            if (customCount != null)
                SetCountText();
        }
    }
    public void SetCountText()
    {
        if(customCount != null && customCount.HasValue)
        {
            Count.text = customCount.Value.ToString();
        }
        else
            Count.text = _item.count.ToString();
    }

    public string id
    {
        set
        {
            Init();
            SetItemId(value);
            SetItem(null);
            SetAreaPos();
        }
    }
    public Item item
    {
        get
        {
            return _item;
        }
        set
        {
            Init();
            if (value == null)
            {
                gameObject.SetActive(false);
                _item = null;
            }
            else
            {
                SetItemId(value.id);
                SetItem(value);
                SetAreaPos();
                gameObject.SetActive(true);
            }
        }
    }

    GameObject LevelArea = null;
    public List<PopupItemInfoArea> AreaList = new List<PopupItemInfoArea>();

    bool isInitialized = false;
    void Init()
    {
        if (isInitialized) return;
        isInitialized = true;
        foreach (PopupItemInfoArea tmp in AreaList)
        {
            switch (tmp.type)
            {
                case PopupItemInfoArea.InfoAreaType.Level: if (LevelArea == null) LevelArea = tmp.area.gameObject; break;
            }
        }

        customCount = null;
    }

    void SetAreaPos()
    {
        float y = 0;
        foreach (PopupItemInfoArea tmp in AreaList)
        {
            bool set = false;
            switch (tmp.type)
            {
                case PopupItemInfoArea.InfoAreaType.Option:
                    if (_item != null && _item.OptionIDList != null && _item.OptionIDList.Count > 0)
                        set = true;
                    break;
                case PopupItemInfoArea.InfoAreaType.Level:
                    if (_item != null && _item.data.isAble("Level"))
                        set = true;
                    break;
                case PopupItemInfoArea.InfoAreaType.SelectCount:
                    if (_item != null && _item.data.isAble("Count"))
                        set = true;
                    break;
                case PopupItemInfoArea.InfoAreaType.Default:
                    set = true;
                    break;
            }
            if(set)
            {
                tmp.PosY = y;
                y -= tmp.Height;
                tmp.area.gameObject.SetActive(true);
            }
            else
            {
                if(tmp.area != null)tmp.area.gameObject.SetActive(false);
            }
        }

        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(t.rect.width, -y);
    }


    void SetItemId(string id)
    {
        ItemData data = new ItemData(id);
        GradeData gradeData = new GradeData(data.Grade);
        if (Icon != null)   Icon.sprite = data.SpriteWithIndex;
        if (Grade != null)  Grade.text = gradeData.Name;
        if (Grade != null)  Grade.color = gradeData.NameColor;
        if (Name != null)   Name.text = data.Name;
        if (InfoText != null) InfoText.text = data.Info;

        /*
        Image Lock;
        UIGaugeUpdater EXPBar;
        Text Level;
        Text InfoText;*/
    }

    void SetItem(Item i)
    {
        if(i == null)
        {
            _item = null;
            if (Count != null) Count.gameObject.SetActive(false);
            if (LevelArea != null) LevelArea.SetActive(false);
            if (Lock != null) Lock.gameObject.SetActive(false);
            if (OptionInfoText != null) OptionInfoText.gameObject.SetActive(false);
        }
        else
        {
            _item = i;
            if (i.data.isAble("Count") && Count != null)
            {
                SetCountText();
                Count.gameObject.SetActive(true);
            }
            else if (Count != null)
            {
                Count.gameObject.SetActive(false);
            }

            if (i.data.isAble("Level") && LevelArea != null)
            {
                CheckExpBar();
                LevelArea.SetActive(true);
            }
            else if (LevelArea != null)
            {
                LevelArea.SetActive(false);
            }

            if(i.OptionIDList != null && i.OptionIDList.Count > 0 && OptionInfoArea != null)
            {
                int line = 0;
                SetOptionText(out line);
                SetTextHeight(OptionInfoText, line * 30f);
                SetTextHeight(OptionInfoTextValue1, line * 30f);
                SetTextHeight(OptionInfoTextValue2, line * 30f);
                OptionInfoText.gameObject.SetActive(true);
                OptionInfoArea.sizeDelta = new Vector2(OptionInfoArea.rect.width, line * 30f + 10f);
            }
            else if (OptionInfoArea != null)
            {
                OptionInfoArea.gameObject.SetActive(false);
            }

            if (Lock != null)
            {
                Lock.gameObject.SetActive(i.isLocked);
            }
            
        }
        
    }


    public void SetExpBar(Level lv)
    {
        if (EXPBar != null && item != null)
        {
            EXPBar.NewGaugeData(new UIGaugeData(lv.CurruntExp, lv.CurruntExpMax));
            Level.text = "Lv " + lv.level;
        }
    }
    public void CheckExpBar()
    {
        if (EXPBar != null && item != null)
        {
            EXPBar.NewGaugeData(new UIGaugeData(item.level.CurruntExp, item.level.CurruntExpMax));
            Level.text = "Lv " + item.level.level;
        }
    }
    void SetTextHeight(Text t, float height)
    {
        if (t == null) return;
        RectTransform rt = t.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.rect.width, height);
    }
    void SetOptionText(out int lineCount)
    {
        lineCount = 0;
        string NameText = "";
        string Value1Text = "";
        string Value2Text = "";
        string ln = "";
        
        StatusValues statInt = _item.OptionValues(PlayerLevel);
        StatusValues statIntEquipLevel = ((PlayerLevel < _item.level.level) ? _item.OptionValues(_item.level.level) : null);
        for (int i = (int)(StatusValues.VALUE.HP); i <= (int)(StatusValues.VALUE.Damage); i++)
        {
            float val = statInt.GetValue((StatusValues.VALUE)i);
            float val2 = 0;
            if(statIntEquipLevel != null)
                val2 = statIntEquipLevel.GetValue((StatusValues.VALUE)i);
            
            if(val != 0)
            {
                bool plus = val > 0;
                bool plus2 = val2 > 0;
                string NameTextLine = "";
                string valueString = string.Format("{0:#.##}", val);
                string value2String = (val2 == 0 ? "" : string.Format("{0:#.##}", val2));

                NameText += ln+((StatusValues.VALUE)i).ToString();

                if (plus)   Value1Text += ln + "<color=#88ff88>+" + valueString + "</color>";
                else        Value1Text += ln + "<color=#ff8888>" + valueString + "</color>";

                if (value2String != "")
                {
                    if (plus2) Value2Text += ln + "<color=#668866>(+" + value2String + ")</color>";
                    else Value2Text += ln + "<color=#886666>(" + value2String + ")</color>";
                }
                else
                    Value2Text += ln;
                if (ln == "") ln = "\n";

                 lineCount += 1;
            }
        }

        if (OptionInfoText != null) OptionInfoText.text = NameText;
        if (OptionInfoTextValue1 != null) OptionInfoTextValue1.text = Value1Text;
        if (OptionInfoTextValue2 != null) OptionInfoTextValue2.text = Value2Text;
    }
}



[System.Serializable]
public class PopupItemInfoArea
{
    public enum InfoAreaType
    {
        Default,
        Level,
        Option,
        SelectCount
    }
    public InfoAreaType type = InfoAreaType.Default;
    public RectTransform area = null;
    public float PosY
    {
        set
        {
            if (area == null) return;
            area.localPosition = new Vector3(area.localPosition.x, value, area.localPosition.z);
        }
    }
    public float Height
    {
        get
        {
            if (area == null) return 0f;
            return area.rect.height;
        }
    }
}