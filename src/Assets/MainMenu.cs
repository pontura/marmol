﻿using System.Collections;
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
        Data.Instance.LoadLevel("AnglesGyroscope");
    }
}
