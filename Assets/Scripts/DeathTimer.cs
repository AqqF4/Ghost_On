using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    public SoundCheckManager soundCheckManager; // Reference to the SoundCheckManager
    public SoundCheck soundCheck;
    public float timeLimit = 5f; // Time limit to check for the correct checkmark
    [HideInInspector] public float timer; // Timer variable
    bool CanTimer;

    void Update()
    {
        if (soundCheckManager.soundCheck.IsCheckmarkPlaced(soundCheckManager.currentSoundIndex - 1))
        {
            timer = 0; // Reset the timer
            CanTimer = false;
            AfterCheckMark();
        }

        if (soundCheckManager.List.activeSelf && CanTimer) // If the continue button is active
        {
            timer += Time.deltaTime; // Increment the timer

            if (timer >= timeLimit) // If time runs out
            {
                Debug.Log("Jumpscare");
            }
        }
    }

    public void AfterCheckMark()
    {
        
        if(!soundCheckManager.List.activeSelf)
        {
            soundCheckManager.listButton.SetActive(false);
            soundCheckManager.pauseButton.SetActive(true);
            soundCheckManager.PlayNextSound();
            soundCheck.audioSource.Pause();
            soundCheckManager.isPaused = true;
        }

        
    }

}
