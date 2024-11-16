using UnityEngine;
using TMPro;
using System.Collections;

public class PizzeriaUpgrade : MonoBehaviour
{
    public GameObject[] rooms; // Массив с комнатами (пиццерия)
    public TextMeshProUGUI priceText, budgetText; // Текст для отображения цены и бюджета
    public GameObject statusText, newStatus; // Текст для отображения состояния
    public GameObject upgradeSoundPrefab; // Префаб звука улучшения
    public GameObject upgradeButton; // Кнопка улучшения
    public bool DoConsequenses;
    
    public int currentUpgradeLevel = 1; // Текущий уровень улучшения
    public float priceMultiplier = 1.5f;
    public float currentPrice = 15f; // Начальная цена
    public float budget; // Основной бюджет
    public float startingBudget; // Стартовый бюджет

    private const string BudgetKey = "Budget"; // Ключ для сохранения бюджета
    private float targetBudget; // Целевое значение бюджета при добавлении суммы
    public float additionSpeed = 1f; // Скорость добавления единиц в секунду

    void Start()
    {
        // Загрузка бюджета из PlayerPrefs или использование значения по умолчанию
        budget = PlayerPrefs.GetFloat(BudgetKey, 100f);
        
        // Проверка стартового бюджета
        if (budget != startingBudget && DoConsequenses)
        {
            budget = startingBudget;
            PlayerPrefs.SetFloat(BudgetKey, budget);
            PlayerPrefs.Save();
        }

        targetBudget = budget; // Устанавливаем целевое значение равным начальному бюджету
        UpdateUI();
        SetRoomActive(currentUpgradeLevel - 1); // Устанавливаем первую комнату активной
    }

    public void OnUpgradeButtonClicked()
    {
        if (budget >= currentPrice)
        {
            // Воспроизведение звука улучшения
            Instantiate(upgradeSoundPrefab, transform.position, Quaternion.identity);

            // Уменьшение бюджета на текущую цену
            budget -= currentPrice;

            // Сохранение бюджета
            PlayerPrefs.SetFloat(BudgetKey, budget);
            PlayerPrefs.Save();

            // Увеличение цены
            currentPrice *= priceMultiplier;

            // Смена активной комнаты
            SetRoomActive(currentUpgradeLevel);

            currentUpgradeLevel++;

            // Проверка уровня улучшения
            if (currentUpgradeLevel >= 11)
            {
                statusText.SetActive(false);
                newStatus.SetActive(true);
                upgradeButton.SetActive(false);
                priceText.gameObject.SetActive(false);
            }

            UpdateUI();
        }
    }

    public void AddToBudget(float amount)
    {
        targetBudget += amount;
        StartCoroutine(GradualBudgetAddition());
    }

    private IEnumerator GradualBudgetAddition()
    {
        while (budget < targetBudget)
        {
            budget += additionSpeed * Time.deltaTime;

            // Округление значения, если превышает targetBudget
            if (budget > targetBudget)
            {
                budget = targetBudget;
            }

            // Обновление UI и сохранение бюджета
            UpdateUI();
            PlayerPrefs.SetFloat(BudgetKey, budget);
            PlayerPrefs.Save();

            yield return null;
        }
    }

    private void UpdateUI()
    {
        priceText.text = $"{currentPrice:F0}$";
        budgetText.text = $"{budget:F0}$";
    }

    private void SetRoomActive(int roomIndex)
    {
        // Деактивируем все комнаты
        foreach (GameObject room in rooms)
        {
            room.SetActive(false);
        }
        // Активируем текущую комнату
        if (roomIndex < rooms.Length)
        {
            rooms[roomIndex].SetActive(true);
        }
    }
}
