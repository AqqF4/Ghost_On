using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public bool isHigh;
    public bool Pressed;

    public void Press()
    {
        Pressed = true;
    }

    
    void Update()
    {
        Pressed = false;
    }
}
