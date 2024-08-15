using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public GameObject Sound;
    bool isPlaying;
    GameObject PlayableSound;

    public void Play(Transform Room)
    {
        if(!isPlaying)
        {
            isPlaying = true;
            PlayableSound = Instantiate(Sound, Room.position, Quaternion.identity);
        }
        else
        {
            Destroy(PlayableSound);
            isPlaying = true;
            PlayableSound = Instantiate(Sound, Room.position, Quaternion.identity);
        }
    }

    public void Stop()
    {
        Destroy(PlayableSound);
        isPlaying = false;
    }
}
