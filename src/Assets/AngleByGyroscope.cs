using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleByGyroscope : MonoBehaviour
{
    public Text field;
    public Text field2;
    public Quaternion angle1;
    public Quaternion angle2;

    bool angle1Done;
    bool angle2Done;
    
    void Start()
    {
        Input.gyro.enabled = true;
    }
    public void Back()
    {
        Data.Instance.LoadLevel("0_Home");
    }
    private void Update()
    {
        field2.text = Input.gyro.attitude.eulerAngles.ToString();
    }
    void SetFields()
    {
       
        if(angle1Done && angle2Done)
        {
            field.text = "[1]" + Quaternion.Angle(angle1, angle2);
            field.text += "\n 1 " + angle1.eulerAngles + " 2: " + angle2.eulerAngles;
        }
        
    }
    public void OnAngulo(int id)
    {
        if (id == 1)
        {
            angle1 = Input.gyro.attitude;
            angle1Done = true;
        }
           
        else
        {
            angle2 = Input.gyro.attitude;
            angle2Done = true;
        }

        SetFields();
    }
    public void Reset()
    {
        angle1Done = angle2Done = false;
        field.text = "";
    }
}
