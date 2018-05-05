using UnityEngine;
using System.Collections;
using System.Xml;

public class MainEffManager : MonoBehaviour
{
    public static MainEffManager Get = null;
    public MainEffManager()
    {
        if (Get == null)
            Get = this;

    }

    XmlFile ItemInfoFile;
    public void NewEff(string id,Vector3 pos)
    {

        XmlElement node = ItemInfoFile.GetNodeByID(id, "Eff");
        GameObject tmp = Resources.Load(XMLUtil.FindOneByTag(node, "Path").InnerText) as GameObject;
        Instantiate(tmp, pos, tmp.transform.rotation);
    }
    // Use this for initialization
    void Start()
    {
        ItemInfoFile = XmlFile.Load("EffInfo");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
