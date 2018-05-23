using UnityEngine;
using System.Collections;

public class PopupItemSelectCount : MonoBehaviour
{
    public static PopupItemSelectCount Get = null;
    public PopupItemSelectCount()
    {
        if (Get == null)
            Get = this;
    }
    public PopupItemInfo InfoMain;
    public PopupItemCountSelectArea CountSelector;
    public void SetActive(Item i, PopupItemCountSelectArea.ReturnFunc returnFunc,int defaultValue = 1)
    {
        InfoMain.item = i;
        CountSelector.item = i;
        CountSelector.returnFunc = returnFunc;
        CountSelector.ResetValue(defaultValue);
        gameObject.SetActive(true);
    }

    public void SetClose()
    {
        gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        CountSelector.Parent = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
