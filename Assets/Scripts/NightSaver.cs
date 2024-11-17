using UnityEngine;

public class NightSaver : MonoBehaviour
{
    private const string NightKey = "NightNumber"; // Ключ для сохранения номера ночи
    private int currentNight; // Текущий номер ночи

    void Start()
    {
        // Загружаем номер ночи из PlayerPrefs или устанавливаем по умолчанию 0
        currentNight = PlayerPrefs.GetInt(NightKey, 0);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            currentNight = 0;
            SaveNight();
        }
    }

    // Увеличить номер ночи
    public void IncrementNight()
    {
        currentNight += 1;
        SaveNight();
    }

    // Уменьшить номер ночи
    public void DecrementNight()
    {
        if (currentNight > 0) // Номер ночи не должен быть отрицательным
        {
            currentNight -= 1;
            SaveNight();
        }
    }

    // Установить конкретный номер ночи
    public void SetNight(int night)
    {
        currentNight = Mathf.Max(0, night); // Не допускаем отрицательных значений
        SaveNight();
    }

    // Получить текущий номер ночи
    public int GetCurrentNight()
    {
        return currentNight;
    }

    // Сохранить номер ночи в PlayerPrefs
    private void SaveNight()
    {
        PlayerPrefs.SetInt(NightKey, currentNight);
        PlayerPrefs.Save();
    }
}
