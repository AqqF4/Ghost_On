using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneComunication : MonoBehaviour
{
    public int sceneId; // ID сцены по умолчанию
    public int SecondNightSceneID; // ID второй ночи
    public int ThirdNightSceneID; // ID третьей ночи
    public int FourthNightSceneID; // ID четвертой ночи
    public int FifthNightSceneID; // ID пятой ночи
    public int FinalNightSceneID; // ID финальной сцены

    public int Money; // Сумма для добавления к бюджету на сцене пиццерии

    private const string AdditionalMoneyKey = "AdditionalMoney"; // Ключ для сохранения добавочной суммы
    private NightSaver nightSaver; // Ссылка на NightSaver
    private PizzeriaUpgrade pizzeriaUpgrade; // Ссылка на PizzeriaUpgrade (для проверки "готово")

    void Start()
    {
        // Ищем NightSaver и PizzeriaUpgrade в сцене
        nightSaver = FindObjectOfType<NightSaver>();
        pizzeriaUpgrade = FindObjectOfType<PizzeriaUpgrade>();
    }

    public void TransformB_Teleportation()
    {
        // Сохраняем значение Money в PlayerPrefs
        PlayerPrefs.SetInt(AdditionalMoneyKey, 50);
        PlayerPrefs.Save();

        // Перемещаемся на сцену
        SceneManager.LoadScene(sceneId);
    }

    public void TransformA_Teleportation()
    {
        // Сохраняем значение Money в PlayerPrefs
        PlayerPrefs.SetInt(AdditionalMoneyKey, 100);
        PlayerPrefs.Save();

        // Перемещаемся на сцену
        SceneManager.LoadScene(sceneId);
    }

    public void LoadPublicScene()
    {
        SceneManager.LoadScene(sceneId);
    }

    public void LoadCurrentScene()
    {
        // Получаем текущий номер ночи
        int currentNight = nightSaver.GetCurrentNight();

        // Логика переключения сцен
        if (currentNight == 1)
        {
            SceneManager.LoadScene(SecondNightSceneID);
        }
        else if (currentNight == 2)
        {
            SceneManager.LoadScene(ThirdNightSceneID);
        }
        else if (currentNight == 3)
        {
            SceneManager.LoadScene(FourthNightSceneID);
        }
        else if (currentNight == 4)
        {
            SceneManager.LoadScene(FifthNightSceneID);
        }
        else if (currentNight >= 5 && pizzeriaUpgrade.newStatus.activeSelf) // Если "готово" активно
        {
            SceneManager.LoadScene(FinalNightSceneID);
        }
        else
        {
            SceneManager.LoadScene(sceneId); // По умолчанию
        }
    }
}