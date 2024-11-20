using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButtonAnimation : MonoBehaviour
{
    public Animator anim;
    
    public void LoadingSceneAnimation()
    {
        anim.SetBool("ButtonPressing", true);
    }
}
