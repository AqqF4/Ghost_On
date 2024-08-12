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

    private float timerCooldown; // Таймер для следующего сканирования
    private float timerShow; // Таймер для времени видимости
    private bool isSkanning; // Флаг для проверки, сканируем ли мы

    void Update()
    {
        if (isSkanning)
        {
            Skan();
        }
    }

    public void StartScanning()
    {
        isSkanning = true;
        timerCooldown = Random.Range(minCooldown, maxCooldown); // Устанавливаем начальный таймер для сканирования
        SetLoadingAnimator(true); // Включаем GameObject для отображения процесса загрузки
    }

    public void StopScanning()
    {
        isSkanning = false;
        // Скрыть аниматроников при остановке сканирования
        SetAnimatronicColor(Color.clear); // Сброс цвета на прозрачный
        SetLoadingAnimator(false); // Выключаем GameObject для отображения процесса загрузки
    }

    void Skan()
    {
        if (timerCooldown > 0f)
        {
            timerCooldown -= Time.deltaTime;
        }
        else
        {
            timerCooldown = Random.Range(minCooldown, maxCooldown); // Перезагрузить таймер
            timerShow = Random.Range(minShowTime, maxShowTime); // Устанавливаем время показа
            ShowFor(timerShow); // Показать аниматроников
        }
    }

    void ShowFor(float ShowTime)
    {
        SetAnimatronicColor(Color.blue); // Изменяем цвет на синий

        StartCoroutine(HideAfterDelay(ShowTime)); // Запускаем корутину для скрытия после задержки
    }

    void SetAnimatronicColor(Color color)
    {
        foreach (SpriteRenderer sr in animatronicRenderers)
        {
            sr.color = color; // Устанавливаем цвет для каждого SpriteRenderer
        }
    }

    void SetLoadingAnimator(bool state)
    {
        if (loadingAnimator != null)
        {
            loadingAnimator.SetActive(state); // Включаем или выключаем GameObject
        }
    }

    IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Ждём указанное время
        SetAnimatronicColor(Color.clear); // Устанавливаем цвет на прозрачный (или любой другой, который сделает аниматроника невидимым)
    }
}