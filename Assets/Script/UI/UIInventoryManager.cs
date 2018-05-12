using UnityEngine;
using System.Collections;

public class UIInventoryManager : MonoBehaviour
{
    public static UIInventoryManager Get = null;
    public UIInventoryManager()
    {
        if (Get == null)
            Get = this;
    }

    public GameObject ItemPrefab;

    public Transform root;
    public RectTransform Contents;

    public GameObject NewItemUI()
    {
        GameObject result = Instantiate(ItemPrefab);
        result.transform.SetParent(root);
        result.transform.localPosition = Vector3.zero;
        return result;
    }

    public GameObject GetCategoryUI(string name)
    {
        for(int i=0;i< root.childCount;i++)
        {
            if (root.GetChild(i).GetComponent<UIItemPositionUpdater>().name == name)
                return root.GetChild(i).gameObject;
        }
        return null;
    }

    public void SetInventoryContentsHeight(float h)
    {
        Contents.sizeDelta = new Vector2(Contents.rect.width,h);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
