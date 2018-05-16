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

    public Image Background;

    public Image Icon;

    public Text Level;
    public void SetLevel(int val) { SetText(Level, "Lv " + val); }

    public Text Count;
    public void SetCount(int val) { SetText(Count, "" + val); }

    public Image Lock;

    public Image Black;

    public Text State;


    public void SetData(ItemData data, Item i = null)
    {
        Icon.sprite = data.SpriteWithIndex;//Sprite
        if (data.isAble("Level")) SetLevel(i.Level);//HasLevel
        if (data.isAble("Count")) SetCount(i.count);//HasCount
    }

    public void SetData(ItemData data, Hashtable i = null)
    {
        Icon.sprite = data.SpriteWithIndex;//Sprite
        if (data.isAble(LevelKey) && i.ContainsKey(LevelKey)) SetLevel((i[LevelKey] as int?).Value);//HasLevel
        if (data.isAble(CountKey) && i.ContainsKey(CountKey)) SetCount((i[CountKey] as int?).Value);//HasCount
    }

    const string LevelKey = "Level";
    const string CountKey = "Count";
}
