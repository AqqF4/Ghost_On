using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool Pressed;
    public bool ComingBack;
    
    void Unnpress()
    {
       Pressed = false; 
    }

    void Awake()
    {
        Unnpress();
    }

    
    public void Press()
    {
        Pressed = true;
    }
}
