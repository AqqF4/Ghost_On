using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRoom : MonoBehaviour
{
    public GameObject GroundedR;
    public GameObject NormR;
    public Functions F;
    
    public void Ground()
    {
        NormR.SetActive(false);
        GroundedR.SetActive(true);
    }
}
