using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSignal : MonoBehaviour
{
    public Text field;
    public GameObject target1;
    public GameObject target2;

    public void Init(GameObject t1, GameObject t2, string value)
    {
        target1 = t1;
        target2 = t2;
        SetText(value);
    }
    public void SetText(string text)
    {
        field.text = text;
    }
}
