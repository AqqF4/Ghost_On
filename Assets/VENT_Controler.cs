using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VENT_Controler : MonoBehaviour
{
    public bool isFreething;
    public bool ComputerOn;
    public GameObject VENTS;
    public GameObject ComputerS;
    public Tasks[] TasksBB;

    public GameObject Computer;
    public GameObject VENTSound;
    public SpriteRenderer[] sr;
    public Color color;

    public SoundGenerator sg;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(ComputerOn)
            {
                foreach(SpriteRenderer animatronic in sr)
                {
                    animatronic.color = color;
                }

                foreach(Tasks t in TasksBB)
                {
                    t.NonEndedStop();
                }


                sg.soundLevel -= 1.2f;
                Computer.SetActive(false);
                ComputerOn = false;
                Instantiate(ComputerS, transform.position, Quaternion.identity);
            }
            else
            {
                sg.soundLevel += 1.2f;
                Computer.SetActive(true);
                ComputerOn = true;
                Instantiate(ComputerS, transform.position, Quaternion.identity);
            }
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            if(isFreething)
            {
                sg.soundLevel -= 1.8f;
                VENTSound.SetActive(false);
                isFreething = false;
                Instantiate(VENTS, transform.position, Quaternion.identity);
            }
            else
            {
                sg.soundLevel += 1.8f;
                VENTSound.SetActive(true);
                isFreething = true;
                Instantiate(VENTS, transform.position, Quaternion.identity);
            }
        }
    }
}
