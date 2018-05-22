using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIItemClickable : MonoBehaviour
{
    public delegate void Action(GameObject obj, UIItemList parent);
    public UIItemList Parent = null;
    Action action = null;
    Image Icon;
    Button bt;

    public void InitWithEvent(Action action, UIItemList p)
    {
        this.action = action;
        this.Parent = p;

        UIItemInfoUpdater info = GetComponent<UIItemInfoUpdater>();
        Icon = info.Icon;
        bt = info.gameObject.GetComponent<Button>();
        if(bt == null)
            bt = info.gameObject.AddComponent<Button>();
        bt.targetGraphic = Icon;
        bt.onClick.RemoveAllListeners();
        bt.onClick.AddListener(() => ClickedEvent());
        Icon.raycastTarget = true;
    }

    void ClickedEvent()
    {
        if(action != null)action(gameObject,Parent);
    }
}
