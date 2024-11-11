using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    public int shockCounter;
    bool isShoking;
    public Animator shockAnim;
    public SoundCheck sc;
    public GameObject PlayButton, StopButton, ShockButton;
    public DeathTimer dt;


    public void Shocker()
    {
        if(!isShoking && shockCounter >= 1)
        {
            sc.PauseSound();
            shockAnim.SetTrigger("Shock");
            isShoking = true;
            shockCounter -= 1;
            PlayButton.SetActive(false);
            StopButton.SetActive(false);
            ShockButton.SetActive(false);

            ResetFaze();
        }
    }


    public void SetShockFalse()
    {
        isShoking = false;
        PlayButton.SetActive(true);
        ShockButton.SetActive(true);
    }

    public void ResetFaze()
    {
        dt.timer = 0f;
        dt.animatronicAnim.SetTrigger("Reset");
        dt.phase2Triggered = false;
        dt.phase3Triggered = false;
        dt.phase4Triggered = false;
    }

    public void SetDespawnAnimation()
    {
        shockAnim.SetBool("isDespawning", true);
    }

    public void SetRespawnAnimation()
    {
        shockAnim.SetBool("isDespawning", false);
        shockAnim.SetTrigger("Respawn");
    }
}
