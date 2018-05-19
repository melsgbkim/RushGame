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
    public PopupItemInfo InfoSub;
    public PlayerLevel player;

    public void SetActive(Item main,Item sub = null)
    {
        InfoMain.PlayerLevel = player.GetIntLv();
        InfoSub.PlayerLevel = player.GetIntLv();
        InfoMain.item = main;
        InfoSub.item = sub;
        gameObject.SetActive(true);
    }


    void Start()
    {
    }
}
