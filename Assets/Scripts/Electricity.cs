using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    public int shockCounter;
    public bool isShoking;
    public Animator shockAnim;
    public SoundCheck sc;
    public SoundCheckManager scm;
    public GameObject PlayButton, StopButton, ShockButton, ListButton;
    public DeathTimer dt;


    public void Shocker()
    {
        if(!isShoking && shockCounter >= 1)
        {
            sc.PauseSound();
            shockAnim.SetTrigger("Shock");
            isShoking = true;
            shockCounter -= 1;
            StopButton.SetActive(false);
            ShockButton.SetActive(false);
            ListButton.SetActive(false);
            ResetFaze();
        }
    }


    public void SetShockFalse()
    {
        isShoking = false;
        ShockButton.SetActive(true);
        if (sc.IsCheckmarkPlaced(scm.currentSoundIndex - 2))
        {
            PlayButton.SetActive(true);
        }
    }

    public void ResetFaze()
    {
        dt.timer = 0f;
        dt.animatronicAnim.SetTrigger("Reset");
        dt.phase2Triggered = false;
        dt.phase3Triggered = false;
        dt.phase4Triggered = false;
        if (sc.IsCheckmarkPlaced(scm.currentSoundIndex - 2))
        {
            PlayButton.SetActive(true);
            scm.listButton.SetActive(false);
            scm.pauseButton.SetActive(true);
            scm.PlayNextSound();
            sc.audioSource.Pause();
            scm.isPaused = true;
            scm.pauseButton.SetActive(false);
            scm.continueButton.SetActive(true);
        }
        else
        {
            PlayButton.SetActive(false);
            scm.currentSoundIndex -= 1;
            //scm.listButton.SetActive(false);
            //scm.pauseButton.SetActive(true);
            //scm.PlayNextSound();
            //sc.audioSource.Pause();
            //scm.isPaused = true;
            //scm.pauseButton.SetActive(false);
            //scm.continueButton.SetActive(true);
        }
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
