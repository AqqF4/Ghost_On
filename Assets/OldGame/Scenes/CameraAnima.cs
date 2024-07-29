using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnima : MonoBehaviour
{
    public Animator anim;
    public void Act()
    {
        anim.SetTrigger("Move");
    }

    
    public void Disact()
    {
        anim.SetTrigger("Back");
    }

    public void Down()
    {
        anim.SetTrigger("Down");
    }

    public void Up()
    {
        anim.SetTrigger("Up");
    }

    public void Exit()
    {
        Application.Quit();
    }


}
