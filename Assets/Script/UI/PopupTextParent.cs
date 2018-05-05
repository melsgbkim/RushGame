using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopupTextParent : MonoBehaviour
{
    public GameObject prefab;
    public Vector2 pos;
    void Update()
    {
        Vector2 tmp = Camera.main.WorldToScreenPoint(pos);
        transform.localPosition = tmp - new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight) / 2f;
    }
    public void NewPopupText(string txt)
    {
        GameObject tmp = Instantiate(prefab);
        tmp.GetComponent<Text>().text = txt;
        tmp.transform.SetParent(transform);
        //Camera.main.poin
    }
}
