using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopupText : MonoBehaviour
{
    public void OnDisable()
    {
        Destroy(transform.parent.gameObject);
    }
}
