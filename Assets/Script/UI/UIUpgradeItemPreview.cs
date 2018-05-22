using UnityEngine;
using System.Collections;

public class UIUpgradeItemPreview : MonoBehaviour
{
    public PopupItemInfo InfoMain;
    public PopupItemInfo InfoSub;
    //public PlayerLevel player;

    public void SetActive(Item main, Item sub = null)
    {
        InfoMain.item = main;
        InfoSub.item = sub;
        gameObject.SetActive(true);
    }

    void Start()
    {
        //SetActive(new Item("Item_E0001"), new Item("Item_E0001"));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            InfoMain.item.level += 10;
            InfoMain.CheckExpBar();
        }
    }
}
