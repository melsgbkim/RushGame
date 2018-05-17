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
                OptionInfoText.text = GetOptionText(out line);
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
    string GetOptionText(out int lineCount)
    {
        lineCount = 0;
        string result = "";
        int TmpPlayerLevel = 1;
        StatusValues statInt = _item.OptionValues(TmpPlayerLevel);
        for (int i = (int)(StatusValues.VALUE.HP); i <= (int)(StatusValues.VALUE.Damage); i++)
        {
            float val = statInt.GetValue((StatusValues.VALUE)i);
            bool plus = val > 0;
            if(val != 0)
            {
                string line = "";
                string valueString = val.ToString();
                if (plus)   line = "" + ((StatusValues.VALUE)i).ToString() + " : <color=#88ff88>+" + valueString + "</color>";
                else        line = "" + ((StatusValues.VALUE)i).ToString() + " : <color=#ff8888>" + valueString + "</color>";
                if(result == "") result += line;
                else             result += "\n"+line;
                lineCount += 1;
            }
        }
        return result;
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