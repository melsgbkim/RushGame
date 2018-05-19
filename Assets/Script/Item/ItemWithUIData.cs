using UnityEngine;
using System.Collections;

public class ItemWithUIData
{
    public Item item;
    public GameObject UI;
    public ItemWithUIData(Item i , GameObject u)
    {
        item = i;
        UI = u;
    }
}
