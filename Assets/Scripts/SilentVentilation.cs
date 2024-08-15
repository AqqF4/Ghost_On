using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilentVentilation : MonoBehaviour
{
    public VENT_Controler vc;
    public SoundGenerator sg;


    public void SilentOff()
    {
        vc.isSilent = false;
        sg.detectionRadius = 2.08f;


        if(vc.isFreething)
        {
            sg.soundLevel = 3f;
        }
        else
        {
            sg.soundLevel = 1.5f;
        }
    }

    
    public void SilentOn()
    {
        vc.isSilent = true;
        sg.detectionRadius = 1.44f;


        if(vc.isFreething)
        {
            sg.soundLevel = 1.7f;
        }
        else
        {
            sg.soundLevel = 1.2f;
        }
    }

    void Update()
    {
        if(!vc.ComputerOn && vc.isSilent)
        {
            vc.isSilent = false;
        }

        if(GameObject.FindGameObjectWithTag("Silent"))
        {
            vc.isSilent = true;
        }
        
    }
}
