using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGenerator : MonoBehaviour
{
    public AnimatronicMovement animatronic;
    public float soundLevel = 10f; // Уровень звука
    public float soundInterval = 5f; // Интервал между звуками
    public float detectionRadius = 10f; // Радиус обнаружения звука

    void Start()
    {
        InvokeRepeating("GenerateSound", soundInterval, soundInterval);
    }

    void GenerateSound()
    {
        // Генерация звука и уведомление аниматроника
        Vector3 soundPosition = transform.position;

        // Проверка, находится ли источник звука в радиусе обнаружения аниматроника
        if (Vector3.Distance(animatronic.transform.position, soundPosition) <= detectionRadius)
        {
            animatronic.HearSound(soundLevel, soundPosition);
        }
    }

    // Отображение радиуса обнаружения в редакторе
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Цвет радиуса
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // Рисуем круг вокруг позиции объекта
    }
}