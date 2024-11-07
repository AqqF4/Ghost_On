using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    public SoundCheckManager soundCheckManager; // Reference to the SoundCheckManager
    public float timeLimit = 5f; // Time limit to check for the correct checkmark
    [HideInInspector] public float timer; // Timer variable

    void Update()
    {
        if (soundCheckManager.continueButton.activeSelf) // If the continue button is active
        {
            timer += Time.deltaTime; // Increment the timer

            if (timer >= timeLimit) // If time runs out
            {
                CheckForCheckmark(); // Check if the correct checkmark is placed
            }
        }
    }

    private void CheckForCheckmark()
    {
        // Check if the current sound index's checkmark is placed correctly
        if (!soundCheckManager.soundCheck.IsCheckmarkPlaced(soundCheckManager.currentSoundIndex - 1))
        {
            Debug.Log("Jumpscare"); // Trigger a jumpscare
            // Implement jumpscare logic here
        }
        timer = 0; // Reset the timer
    }
}
