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

}
