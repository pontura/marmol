using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Mapear()
    {
        Data.Instance.LoadLevel("1_Medicion");
    }
    public void Angles()
    {
        Data.Instance.LoadLevel("2_Angulos_gyro");
    }
    public void Dardos()
    {
        Data.Instance.LoadLevel("2_Dardos");
    }
}
