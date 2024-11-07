using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneTest : MonoBehaviour
{
    public int Faze;
    public int CloneFaze = 1;
    public float minTime, maxTime;
    float Timer, NeededTime;

    public int CurrentNight = 1;

    public GameObject Clone1, Clone2, Clone3, Clone4;
    public GameObject Bird1, Bird2, Bird3, Bird4; // Теги для галочек

    public List L;

    private bool jumpScarePlayed = false; // Флаг для отслеживания, был ли скример уже активирован

    void Update()
    {
        // Проверяем условия для разных ночей
        if (CurrentNight == 1)
        {
            if (L.ListActive)
            {
                if (Faze >= 3)
                {
                    WaitForJumpscare();
                }
            }
            else
            {
                ResetTimers();
            }
        }
        else if (CurrentNight >= 2)
        {
            if (L.ListActive)
            {
                if (Faze >= 2 || CurrentNight > 3)
                {
                    WaitForJumpscare();
                }
            }
            else
            {
                ResetTimers();
            }
        }

        // Проверяем, если все галочки активны и скример еще не был запущен
        if (AreAllBirdsActive() && !jumpScarePlayed)
        {
            PlayJump();
        }
    }

    void Start()
    {
        NeededTime = Random.Range(minTime, maxTime);
    }

    public void WaitForJumpscare()
    {
        if (Timer < NeededTime)
        {
            Timer += Time.deltaTime;
        }
        else
        {
            if (CloneFaze != 4)
            {
                CloneFaze += 1;
            }
            else
            {
                PlayJump();
            }
        }
    }

    void PlayJump()
    {
        // Реализуйте здесь логику скримера
        Debug.Log("Jumpscare activated!");
        jumpScarePlayed = true; // Устанавливаем флаг, чтобы не запускать скример снова
        // Добавьте здесь код для запуска скримера (например, активация анимации, звука и т.д.)
    }

    public void ResetFaze()
    {
        CloneFaze = 1;
        ResetTimers();
    }

    void ResetTimers()
    {
        NeededTime = Random.Range(minTime, maxTime);
        Timer = 0f;
    }

    bool AreAllBirdsActive()
    {
        return Bird1.activeSelf && Bird2.activeSelf && Bird3.activeSelf && Bird4.activeSelf;
    }
}