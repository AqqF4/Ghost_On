using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VENT_Controler : MonoBehaviour
{
    public bool isFreething;
    public bool ComputerOn;
    public GameObject VENTS;
    public GameObject ComputerS;
    public bool isSilent;
    public Tasks[] TasksBB;

    public GameObject Computer;
    public GameObject VENTSound;
    public SpriteRenderer[] sr;
    public float ventForce = 1.8f;
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

                if(GameObject.FindGameObjectWithTag("PlayingSound"))
                {
                    GameObject[] sods = GameObject.FindGameObjectsWithTag("PlayingSound");
                    foreach(GameObject sod in sods)
                    {
                        sod.SetActive(false);
                    }
                }

                if(!isSilent)
                {
                    sg.soundLevel -= 1.2f;
                }
                else if(isSilent && isFreething)
                {
                    sg.soundLevel -= 0.5f;
                }
                else if(isSilent && !isFreething)
                {
                    sg.soundLevel -= 1.2f;
                }


                
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
                if(isSilent)
                {
                    sg.soundLevel -= 0.5f;
                }
                else
                {
                    sg.soundLevel -= 1.5f;
                }

                VENTSound.SetActive(false);
                isFreething = false;
                Instantiate(VENTS, transform.position, Quaternion.identity);
            }
            else
            {
                if(isSilent)
                {
                    sg.soundLevel += 0.5f;
                }
                else
                {
                    sg.soundLevel += 1.5f;
                }

                VENTSound.SetActive(true);
                isFreething = true;
                Instantiate(VENTS, transform.position, Quaternion.identity);
            }
        }
    }
}
