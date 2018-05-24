using UnityEngine;
using System.Collections;

public class PopupItemInfoManager : MonoBehaviour
{
    public static PopupItemInfoManager Get = null;
    public PopupItemInfoManager()
    {
        if (Get == null)
            Get = this;
    }

    public PopupItemInfo InfoMain;
    public PlayerLevel player;

    public void SetActive(Item main)
    {
        InfoMain.PlayerLevel = player.GetIntLv();
        InfoMain.item = main;
        gameObject.SetActive(true);
    }

    public void SetActive(GameObject i)
    {
        UIItemInfoUpdater info = i.GetComponent<UIItemInfoUpdater>();
        if (info != null && info.item != null)
            SetActive(info.item);
    }

    void Start()
    {

    }
}
