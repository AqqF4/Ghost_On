using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingTheGame : MonoBehaviour
{
    public Animator anim;
    public GameObject Glitch;
    
    public void StartGame()
    {
        anim.SetBool("GameStarted", true);
    }

    public void EndGame()
    {
        Glitch.SetActive(false);
    }
}
