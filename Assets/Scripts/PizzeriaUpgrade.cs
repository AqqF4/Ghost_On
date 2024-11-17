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
    public int currentPrice = 15; // Начальная цена (целое число)
    public int budget; // Основной бюджет
    public int startingBudget; // Стартовый бюджет
    int additionalMoney;
    bool isAddingMoney;

    private const string BudgetKey = "Budget"; // Ключ для сохранения бюджета
    private const string UpgradeLevelKey = "UpgradeLevel"; // Ключ для сохранения уровня улучшения
    private const string UpgradePriceKey = "UpgradePrice"; // Ключ для сохранения текущей цены
    private int targetBudget; // Целевое значение бюджета при добавлении суммы
    public float additionSpeed = 1f; // Скорость добавления единиц в секунду

    void Update()
    {
        UpdateUI();

        if (targetBudget > budget)
        {
            budget = PlayerPrefs.GetInt(BudgetKey, startingBudget);
            if (targetBudget > budget && !isAddingMoney)
            {
                AddToBudget(targetBudget - budget);
            }

            if (additionalMoney == budget)
            {
                additionalMoney = 0;
                PlayerPrefs.SetInt("AdditionalMoney", 0);
                PlayerPrefs.Save();
                targetBudget = 0;
            }

        }
    }

    void Start()
    {
        // Загрузка бюджета из PlayerPrefs
        budget = PlayerPrefs.GetInt(BudgetKey, startingBudget);

        // Загрузка уровня улучшения и цены из PlayerPrefs
        currentUpgradeLevel = PlayerPrefs.GetInt(UpgradeLevelKey, 1);
        currentPrice = PlayerPrefs.GetInt(UpgradePriceKey, 15);

        // Проверка стартового бюджета
        if (budget != startingBudget && DoConsequenses)
        {
            budget = startingBudget;
            PlayerPrefs.SetInt(BudgetKey, budget);
            PlayerPrefs.Save();
        }

        if (!DoConsequenses)
        {
            additionalMoney = PlayerPrefs.GetInt("AdditionalMoney");
            if (additionalMoney > 0 && !DoConsequenses)
            {
                AddToBudget(additionalMoney);
            }
        }
        else if (DoConsequenses)
        {
            PlayerPrefs.SetInt("AdditionalMoney", 0); // Сбрасываем AdditionalMoney
            PlayerPrefs.SetInt(UpgradeLevelKey, 1); // Сбрасываем уровень улучшения
            PlayerPrefs.SetInt(UpgradePriceKey, 15); // Сбрасываем цену улучшения
            PlayerPrefs.Save();
        }

        // Устанавливаем активную комнату, соответствующую загруженному уровню
        SetRoomActive(currentUpgradeLevel - 1); 
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
            PlayerPrefs.SetInt(BudgetKey, budget);

            // Увеличение цены
            currentPrice = Mathf.RoundToInt(currentPrice * priceMultiplier);

            // Сохранение текущей цены
            PlayerPrefs.SetInt(UpgradePriceKey, currentPrice);

            // Увеличение уровня улучшения
            currentUpgradeLevel++;

            // Сохранение текущего уровня улучшения
            PlayerPrefs.SetInt(UpgradeLevelKey, currentUpgradeLevel);

            PlayerPrefs.Save();

            // Смена активной комнаты
            SetRoomActive(currentUpgradeLevel - 1);

            // Проверка уровня улучшения
            if (currentUpgradeLevel >= 11)
            {
                statusText.SetActive(false);
                newStatus.SetActive(true);
                upgradeButton.SetActive(false);
                priceText.gameObject.SetActive(false);
            }
        }
    }

    public void AddToBudget(int amount)
    {
        targetBudget = amount;
        StartCoroutine(GradualBudgetAddition());
    }

    private IEnumerator GradualBudgetAddition()
    {
        float delay = 1f / additionSpeed; // Задержка между добавлением каждой единицы (в секундах)
        while (budget < targetBudget)
        {
            if (!isAddingMoney)
            {
                isAddingMoney = true;
                additionalMoney = 0;
                PlayerPrefs.SetInt("AdditionalMoney", 0);
                PlayerPrefs.Save();
            }
            budget += 1; // Добавляем ровно 1 к бюджету

            // Обновляем UI и сохраняем изменения
            PlayerPrefs.SetInt(BudgetKey, budget);
            PlayerPrefs.Save();

            yield return new WaitForSeconds(delay); // Ждем указанное время
        }
        isAddingMoney = false;
    }

    private void UpdateUI()
    {
        priceText.text = $"{currentPrice}$";
        budgetText.text = $"{budget}$";
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
