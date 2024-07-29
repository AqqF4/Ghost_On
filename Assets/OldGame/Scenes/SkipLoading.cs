using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipLoading : MonoBehaviour
{
    public Animator skipAnim;

    public void Skip()
    {
        skipAnim.SetTrigger("Skipping");
    }
}
