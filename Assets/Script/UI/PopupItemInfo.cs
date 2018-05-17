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

    

    public string id
    {
        set
        {
            SetItemId(value);
            SetItem(null);
            SetAreaPos();
        }
    }
    public Item item
    {
        set
        {
            SetItemId(value.id);
            SetItem(value);
            SetAreaPos();
        }
    }

    GameObject LevelArea = null;
    public List<PopupItemInfoArea> AreaList = new List<PopupItemInfoArea>();

    void Start()
    {
        foreach(PopupItemInfoArea tmp in AreaList)
        {
            switch(tmp.type)
            {
                case PopupItemInfoArea.InfoAreaType.Level:if (LevelArea == null) LevelArea = tmp.area.gameObject;break;
            }
        }


        //id = "Item_0001";
        item = new Item("Item_E0001");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
            item = new Item("Item_E0001");
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
                    if (_item != null && _item.OptionIDList != null)
                        set = true;
                    break;
                case PopupItemInfoArea.InfoAreaType.Level:
                    if (_item != null && _item.data.isAble("Level"))
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
                tmp.area.gameObject.SetActive(false);
            }
        }

        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(t.rect.width, -y);
    }


    void SetItemId(string id)
    {
        _item = new Item(id, true);
        Icon.sprite = _item.data.SpriteWithIndex;

        Grade.color = _item.gradeData.NameColor;
        Name.text = _item.Name;
        InfoText.text = _item.data.Info;

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
            Count.gameObject.SetActive(false);
            LevelArea.SetActive(false);
            Lock.gameObject.SetActive(false);
            OptionInfoText.gameObject.SetActive(false);
        }
        else
        {
            if (i.data.isAble("Count"))
            {
                Count.text = i.count.ToString();
                Count.gameObject.SetActive(true);
            }
            else
            {
                Count.gameObject.SetActive(false);
            }

            if (i.data.isAble("Level"))
            {
                EXPBar.NewGaugeData(new UIGaugeData(0,1));
                Level.text = "Lv " + i.Level;
                LevelArea.SetActive(true);
            }
            else
            {
                LevelArea.SetActive(false);
            }

            if(i.OptionIDList != null && i.OptionIDList.Count > 0)
            {
                int line = 0;
                SetOptionText(out line);
                OptionInfoText.GetComponent<RectTransform>().sizeDelta = new Vector2(OptionInfoText.GetComponent<RectTransform>().rect.width, line * 30f);
                OptionInfoText.gameObject.SetActive(true);
                OptionInfoArea.sizeDelta = new Vector2(OptionInfoArea.rect.width, line * 30f + 10f);
            }
            else
            {
                OptionInfoText.gameObject.SetActive(false);
            }

            Lock.gameObject.SetActive(i.isLocked);

            _item = i;
        }
        
    }


    /*
     *스텟 이름, 현재 적용될 스텟, 장비의 한계스텟(플레이어 레벨이 높으면 미표시) 
     */
    void SetOptionText(out int lineCount)
    {
        lineCount = 0;
        string NameText = "";
        string Value1Text = "";
        string Value2Text = "";
        string ln = "";
        int TmpPlayerLevel = 1;
        StatusValues statInt = _item.OptionValues(TmpPlayerLevel);
        StatusValues statIntEquipLevel = ((TmpPlayerLevel < _item.Level) ? _item.OptionValues(_item.Level) : null);
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
                string valueString = val.ToString();
                string value2String = (val2 == 0 ? "" : val2.ToString());

                NameText += ln+((StatusValues.VALUE)i).ToString();

                if (plus)   Value1Text += ln + "<color=#88ff88>+" + valueString + "</color>";
                else        Value1Text += ln + "<color=#ff8888>" + valueString + "</color>";

                if (plus2)  Value2Text += ln + "<color=#668866>(+" + value2String + ")</color>";
                else        Value2Text += ln + "<color=#886666>(" + value2String + ")</color>";

                if (ln == "") ln = "\n";

                 lineCount += 1;
            }
        }

        OptionInfoText.text = NameText;
        OptionInfoTextValue1.text = Value1Text;
        OptionInfoTextValue2.text = Value2Text;
    }
}



[System.Serializable]
public class PopupItemInfoArea
{
    public enum InfoAreaType
    {
        Default,
        Level,
        Option
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