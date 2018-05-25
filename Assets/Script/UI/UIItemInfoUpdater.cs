using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIItemInfoUpdater : MonoBehaviour
{
    void SetText(Text t,string s)
    {
        t.text = s;
        if (t.gameObject.activeSelf == false)
            t.gameObject.SetActive(true);
    }
    public Item item;
    public Image Background;

    public Image Icon;

    public Text Level;
    public void SetLevel(int val) { if (data.isAble(LevelKey)) SetText(Level, "Lv " + val); }

    public int LastCount = 0;
    public Text Count;
    public void SetCount(int val) { if (data.isAble(CountKey)) SetText(Count, "" + (LastCount = val)); }

    public Image Lock;

    public Image Black;

    public Text ItemNumber;

    public Image Select;

    ItemData data;

    public void SetSelect(bool val)
    {
        Select.gameObject.SetActive(val);
    }

    void Init()
    {
        SetSelect(false);
    }
    public void SetData(Item i)
    {
        SetData(i.data, i);
        Init();
    }
    public void SetData(ItemData data, Item i = null)
    {
        this.data = data;
        Icon.sprite = data.SpriteWithIndex;//Sprite
        SetLevel(i.level.level);//HasLevel
        SetCount(i.count);//HasCount
        if (i != null)
        {
            item = i;
            ItemNumber.text = "ID " + item.ItemNumber;
            ItemNumber.gameObject.SetActive(true);
        }
    }

    public void SetData(ItemData data, Hashtable i = null)
    {
        this.data = data;
        Icon.sprite = data.SpriteWithIndex;//Sprite
        if (data.isAble(LevelKey) && i.ContainsKey(LevelKey)) SetLevel((i[LevelKey] as int?).Value);//HasLevel
        if (data.isAble(CountKey) && i.ContainsKey(CountKey)) SetCount((i[CountKey] as int?).Value);//HasCount
    }

    const string LevelKey = "Level";
    const string CountKey = "Count";
}
