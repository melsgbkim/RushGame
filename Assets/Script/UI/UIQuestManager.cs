using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIQuestManager : MonoBehaviour
{
    public static UIQuestManager Get = null;
    public UIQuestManager()
    {
        if (Get == null)
            Get = this;
    }

    public GameObject Prefab;

    public Transform root;
    public RectTransform Contents;
    public List<QuestUILine> ObjectList = new List<QuestUILine>();
    public float Gap = 10f;

    public GameObject NewPrefab(string id)
    {
        GameObject result = Instantiate(Prefab);
        result.transform.SetParent(root);
        result.transform.localPosition = Vector3.zero;

        QuestUILine line = result.GetComponent<QuestUILine>();
        line.SetQuest(id);
        line.SetPosY(ObjectList.Count * (Prefab.GetComponent<RectTransform>().rect.height + Gap) * -1);
        ObjectList.Add(line);

        float height = ObjectList.Count * (Prefab.GetComponent<RectTransform>().rect.height + Gap);
        if (height < 1020f) height = 1020f;
        SetContentsHeight(height);
        return result;
    }




    public void SetContentsHeight(float h)
    {
        Contents.sizeDelta = new Vector2(Contents.rect.width, h);
    }

    void Start()
    {
        NewPrefab("QuestBasic_0001");
        NewPrefab("QuestBasic_0001");
        NewPrefab("QuestBasic_0001");
        NewPrefab("QuestBasic_0001");
        NewPrefab("QuestBasic_0001");
        NewPrefab("QuestBasic_0001");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            NewPrefab("QuestBasic_0001");
        }
    }
}
