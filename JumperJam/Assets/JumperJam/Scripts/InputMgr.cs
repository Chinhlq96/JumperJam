using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager;
using System;

public class InputMgr : MonoBehaviour 
{
    public static Action TapToScreen;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TapToScreen!=null)
            {
                TapToScreen.Invoke();
            }
        }
    }
}
