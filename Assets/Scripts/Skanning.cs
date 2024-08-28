using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skanning : MonoBehaviour
{
    public SpriteRenderer[] animatronicRenderers; // Массив SpriteRenderer для аниматроников
    public GameObject loadingAnimator; // Ссылка на GameObject для отображения процесса загрузки
    public float minCooldown = 0.2f; // Минимальное время между сканированием
    public float maxCooldown = 1.6f; // Максимальное время между сканированием
    public float minShowTime = 0.01f; // Минимальное время видимости
    public float maxShowTime = 0.09f; // Максимальное время видимости

    private bool isSkanning; // Флаг для проверки, сканируем ли мы

    void Update()
    {
        if (isSkanning)
        {
            // Можно добавить логику Update, если потребуется
        }
    }

    public void StartScanning()
    {
        isSkanning = true;
        SetLoadingAnimator(true); // Включаем GameObject для отображения процесса загрузки

        foreach (SpriteRenderer sr in animatronicRenderers)
        {
            StartCoroutine(HandleAnimatronicAppearance(sr));
        }
    }

    public void StopScanning()
    {
        isSkanning = false;
        // Скрыть аниматроников при остановке сканирования
        foreach (SpriteRenderer sr in animatronicRenderers)
        {
            sr.color = Color.clear; // Сброс цвета на прозрачный
        }
        SetLoadingAnimator(false); // Выключаем GameObject для отображения процесса загрузки
    }

    IEnumerator HandleAnimatronicAppearance(SpriteRenderer sr)
    {
        while (isSkanning)
        {
            // Задаем случайный интервал до следующего появления аниматроника
            float cooldown = Random.Range(minCooldown, maxCooldown);
            yield return new WaitForSeconds(cooldown); // Ждем интервал перед показом

            // Устанавливаем цвет аниматроника
            sr.color = Color.white;

            // Задаем случайное время видимости
            float showTime = Random.Range(minShowTime, maxShowTime);
            yield return new WaitForSeconds(showTime); // Ждем, пока аниматроник будет виден

            // Скрываем аниматроника
            sr.color = Color.clear;
        }
    }

    void SetLoadingAnimator(bool state)
    {
        if (loadingAnimator != null)
        {
            loadingAnimator.SetActive(state); // Включаем или выключаем GameObject
        }
    }
}
