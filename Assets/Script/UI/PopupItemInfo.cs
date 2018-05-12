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


        id = "Item_0001";
    }

    void Update()
    {

    }

    void SetAreaPos()
    {
        float y = 0;
        foreach (PopupItemInfoArea tmp in AreaList)
        {
            bool set = false;
            switch (tmp.type)
            {
                case PopupItemInfoArea.InfoAreaType.Level:
                    if (_item != null && _item.isAble("Level"))
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
        Icon.sprite = _item.sprite;

        Grade.text = XMLUtil.FindOneByTag(_item.ItemGradeNode,"Name").InnerText;
        Grade.color = _item.GetColorByGrade("NameColor");

        Name.text = _item.Name;
        InfoText.text = XMLUtil.FindOneByTag(_item.ItemNode, "Info").InnerText; ;

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
        }
        else
        {
            if (i.isAble("Count"))
            {
                Count.text = i.count.ToString();
                Count.gameObject.SetActive(true);
            }
            else
            {
                Count.gameObject.SetActive(false);
            }

            if (i.isAble("Level"))
            {
                EXPBar.NewGaugeData(new UIGaugeData(0,1));
                Level.text = "Lv " + i.Level;
                LevelArea.SetActive(true);
            }
            else
            {
                LevelArea.SetActive(false);
            }

            Lock.gameObject.SetActive(i.isLocked);

            _item = i;
        }
        
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