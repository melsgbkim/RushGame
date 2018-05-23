using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupItemCountSelectArea : MonoBehaviour
{
    Item _item;
    public Item item
    {
        get { return _item; }
        set
        {
            _item = value;
            ResetValue();
            maxCount = _item.count;
        }
    }
    public Text countTxt;
    public delegate void ReturnFunc(Item i,int count);
    public ReturnFunc returnFunc = null;
    public PopupItemSelectCount Parent = null;
    public void ChangeValue(int val)
    {
        count += val;
        if (count < 1) count = 1;
        if (count > maxCount) count = maxCount;
        countTxt.text = count.ToString();
    }

    public void ResetValue(int DefaultValue = 1)
    {
        count = DefaultValue;
        countTxt.text = count.ToString();
    }

    public void ValueSelectEnd()
    {
        if (Parent != null) Parent.SetClose();
        if (returnFunc != null) returnFunc(item, count);
    }

    int count = 1;
    int maxCount = 1;
}
