using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List : MonoBehaviour
{
    public Animator anim;
    public bool ListActive;

    void Update()
    {
        
    }

    public void ListUp()
    {
        anim.SetBool("isActive", true);
        ListActive = true;
    }

    public void ListDown()
    {
        anim.SetBool("isActive", false);
        ListActive = false;
    }
}
