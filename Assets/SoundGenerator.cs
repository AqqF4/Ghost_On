using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGenerator : MonoBehaviour
{
    public float soundLevel = 10f; // Уровень звука
    public float soundInterval = 5f; // Интервал между звуками
    public float detectionRadius = 10f; // Радиус обнаружения звука

    private AnimatronicMovement[] animatronics;

    void Start()
    {
        InvokeRepeating("GenerateSound", soundInterval, soundInterval);
    }

    void GenerateSound()
    {
        // Находим все объекты с тегом "Animatronic" и получаем их компоненты AnimatronicMovement
        GameObject[] animatronicObjects = GameObject.FindGameObjectsWithTag("Animatronic");
        animatronics = new AnimatronicMovement[animatronicObjects.Length];
        
        for (int i = 0; i < animatronicObjects.Length; i++)
        {
            animatronics[i] = animatronicObjects[i].GetComponent<AnimatronicMovement>();
        }

        Vector3 soundPosition = transform.position;
        Debug.Log("Sound generated at position: " + soundPosition);

        foreach (AnimatronicMovement animatronic in animatronics)
        {
            if (animatronic == null) continue;

            // Проверка, находится ли источник звука в радиусе обнаружения аниматроника
            if (Vector3.Distance(animatronic.transform.position, soundPosition) <= detectionRadius)
            {
                Debug.Log("Animatronic detected within radius. Sending sound.");
                animatronic.HearSound(soundLevel, soundPosition);
            }
        }
    }

    // Отображение радиуса обнаружения в редакторе
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Цвет радиуса
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // Рисуем круг вокруг позиции объекта
    }
}