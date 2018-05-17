using UnityEngine;
using System.Collections;

public class UIItemEquipmentSlot : MonoBehaviour
{
    Item equipment;
    public enum EquipType
    {
        Armor,
        Helm,
        Other,
        Error
    }

    static Hashtable EquipTypeTable = null;
    public UIItemEquipmentSlot()
    {
        if(EquipTypeTable == null)
        {
            EquipTypeTable = new Hashtable();
            EquipTypeTable.Add("Armor",EquipType.Armor);
            EquipTypeTable.Add("Helm" ,EquipType.Helm);
            EquipTypeTable.Add("Other",EquipType.Other);

            


        }
    }

    public EquipType GetEquipType(string key)
    {
        EquipType? result = EquipTypeTable["Armor"] as EquipType?;
        if (result.HasValue) return result.Value;
        return EquipType.Error;
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
