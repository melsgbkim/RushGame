using UnityEngine;
using System.Collections;

public class PopupTextList : MonoBehaviour
{
    public static PopupTextList Get = null;
    public PopupTextList()
    {
        if (Get == null)
            Get = this;
    }
    public GameObject prefab;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewText(string txt, Vector2 pos)
    {
        GameObject canvas = GameObject.Find("Canvas");

        GameObject Text = Instantiate(prefab) as GameObject;
        Text.transform.SetParent(canvas.transform);
        Text.GetComponent<PopupTextParent>().pos = pos;
        Text.GetComponent<PopupTextParent>().NewPopupText(txt);
    }

    public static void New(string txt,Vector2 pos)
    {
        Get.NewText(txt, pos);
    }
}
