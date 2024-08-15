using System.Collections;
using UnityEngine;
using TMPro;

public class Temperature : MonoBehaviour
{
    public int temperature; // Текущая температура
    public float coolDown = 1f; // Интервал времени в секундах
    public bool isFreezing;
    public TMP_Text PlusObject;
    public SoundGenerator sg;

    public TMP_Text sign; // Текстовый объект для отображения температуры
    public Color lowTemperatureColor = Color.blue; // Цвет для низкой температуры (например, 60 градусов)
    public Color highTemperatureColor = Color.red; // Цвет для высокой температуры (например, 105 градусов)

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

        if(temperature >= 100)
        {
            sg.soundInterval = 2;
            sg.detectionRadius = 2.3f;
        }
        else if(temperature >= 103)
        {
            sg.soundInterval = 1;
            sg.detectionRadius = 4.1f;
        }
    }

    private IEnumerator TemperatureControl()
    {
        while (true)
        {
            // Проверяем состояние freezing
            if (isFreezing)
            {
                if (temperature > 60)
                {
                    temperature -= 1; // Уменьшаем температуру
                }
            }
            else
            {
                if (temperature < 105)
                {
                    temperature += 1; // Увеличиваем температуру
                }
            }

            UpdateTemperatureDisplay(); // Обновляем отображение температуры

            if(isFreezing)
            {
                yield return new WaitForSeconds(coolDown); // Ждём заданное количество секунд
            }
            else
            {
                yield return new WaitForSeconds(coolDown - 1f); // Ждём заданное количество секунд
            }
        }
    }

    private void UpdateTemperatureDisplay()
    {
        if (sign != null)
        {
            sign.text = temperature.ToString(); // Обновляем текст на экране

            // Определяем процентное соотношение температуры между 60 и 105 градусами
            float t = Mathf.InverseLerp(60f, 105f, temperature);

            // Интерполируем цвет между lowTemperatureColor и highTemperatureColor
            Color currentColor = Color.Lerp(lowTemperatureColor, highTemperatureColor, t);

            // Применяем цвет к тексту
            sign.color = currentColor;
            PlusObject.color = currentColor;
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