using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatronicBlocker : MonoBehaviour
{
    public GameObject rightSign;  // Объект, отвечающий за правую сторону
    public GameObject leftSign;   // Объект, отвечающий за левую сторону

    public bool BlocksRight { get; private set; } // Флаг блокировки движения вправо
    public bool BlocksLeft { get; private set; }  // Флаг блокировки движения влево

    void Update()
    {
        // Проверяем активен ли rightSign, если да - блокируем движение вправо, иначе - разблокируем
        if (!rightSign.activeSelf)
        {
            BlocksRight = true;
        }
        else
        {
            BlocksRight = false;
        }

        // Проверяем активен ли leftSign, если да - блокируем движение влево, иначе - разблокируем
        if (!leftSign.activeSelf)
        {
            BlocksLeft = true;
        }
        else
        {
            BlocksLeft = false;
        }
    }
}
