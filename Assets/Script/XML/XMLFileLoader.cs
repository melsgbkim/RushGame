using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XMLFileLoader {
    public XMLFileLoader()
    {
        init();
    }

    void init()
    {
        XmlTable = new Hashtable();

    }

    public XmlFile File(string path)
    {
        path = XMLPath + path;
        if (XmlTable.ContainsKey(path))
        {
            XmlFile result = XmlTable[path] as XmlFile;
            if (result != null)
                return result;
        }
        XmlFile tmp = new XmlFile();
        if(tmp.OpenXmlFile(path))
            XmlTable.Add(path, tmp);
        else
            XmlTable.Add(path, null);

        return XmlTable[path] as XmlFile;
    }
    static string XMLPath = "XML/";

    Hashtable XmlTable;

    static XMLFileLoader loader = null;
    public static XMLFileLoader Loader
    {
        get
        {
            if (loader == null)
                loader = new XMLFileLoader();
            return loader;
        }
    }
}
