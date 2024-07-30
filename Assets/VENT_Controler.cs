using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VENT_Controler : MonoBehaviour
{
    public bool isFreething;
    public bool ComputerOn;
    public GameObject VENTS;
    public GameObject ComputerS;

    public GameObject Computer;
    public GameObject VENTSound;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(ComputerOn)
            {
                Computer.SetActive(false);
                ComputerOn = false;
                Instantiate(ComputerS, transform.position, Quaternion.identity);
            }
            else
            {
                Computer.SetActive(true);
                ComputerOn = true;
                Instantiate(ComputerS, transform.position, Quaternion.identity);
            }
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            if(isFreething)
            {
                VENTSound.SetActive(false);
                isFreething = false;
                Instantiate(VENTS, transform.position, Quaternion.identity);
            }
            else
            {
                VENTSound.SetActive(true);
                isFreething = true;
                Instantiate(VENTS, transform.position, Quaternion.identity);
            }
        }
    }
}
