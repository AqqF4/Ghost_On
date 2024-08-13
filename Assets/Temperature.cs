using System.Collections;
using UnityEngine;
using TMPro; // Не забудьте добавить эту директиву

public class Temperature : MonoBehaviour
{
    public int temperature; // Изменено с Temperatura на temperature для соответствия стандартам именования
    public float coolDown = 1f; // Интервал времени в секундах
    public bool isFreezing;

    public TMP_Text sign; // Объект для отображения температуры

    private Coroutine temperatureCoroutine; // Ссылка на корутину

    void Start()
    {
        // Запускаем корутину при запуске сцены
        temperatureCoroutine = StartCoroutine(TemperatureControl());
    }

    void Update()
    {
        // Обновляем состояние freezing на основе наличия объекта с тегом "Ventelator"
        isFreezing = GameObject.FindGameObjectWithTag("Ventelator") != null;
    }

    private IEnumerator TemperatureControl()
    {
        while (true)
        {
            // Проверяем состояние freezing
            if (isFreezing)
            {
                if(temperature >= 61)
                {
                    temperature -= 1; // Уменьшаем температуру
                }

                UpdateTemperatureDisplay(); // Обновляем отображение температуры

                yield return new WaitForSeconds(coolDown); // Ждём заданное количество секунд
            }
            else
            {
                temperature += 1; // Увеличиваем температуру
                UpdateTemperatureDisplay(); // Обновляем отображение температуры

                yield return new WaitForSeconds(coolDown - 1f); // Ждём заданное количество секунд
            }
        }
    }

    private void UpdateTemperatureDisplay()
    {
        if (sign != null)
        {
            sign.text = temperature.ToString(); // Обновляем текст на экране
        }
    }

    void OnDestroy()
    {
        // Останавливаем корутину при уничтожении объекта
        if (temperatureCoroutine != null)
        {
            StopCoroutine(temperatureCoroutine);
        }
    }
}