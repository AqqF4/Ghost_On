using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloneButtons : MonoBehaviour
{
    public int Ending;
    public GameObject C047;
    public GameObject C325;
    public GameObject C586;
    public GameObject C993;

    public int W047;
    public int W325;
    public int W586;
    public int W993;

    public bool WasOn047;
    public bool WasOn325;
    public bool WasOn586;
    public bool WasOn993;

    void Start()
    {
        // Загрузка данных из PlayerPrefs
        Ending = PlayerPrefs.GetInt("Ending", 0);
        W047 = PlayerPrefs.GetInt("W047", 0);
        W325 = PlayerPrefs.GetInt("W325", 0);
        W586 = PlayerPrefs.GetInt("W586", 0);
        W993 = PlayerPrefs.GetInt("W993", 0);

        // Установка состояния объектов в зависимости от сохраненных данных
        C047.SetActive(W047 == 1);
        C325.SetActive(W325 == 1);
        C586.SetActive(W586 == 1);
        C993.SetActive(W993 == 1);

        // Установка флагов активности объектов
        WasOn047 = W047 == 1;
        WasOn325 = W325 == 1;
        WasOn586 = W586 == 1;
        WasOn993 = W993 == 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Сброс данных и деактивация объектов
            PlayerPrefs.DeleteKey("W047");
            PlayerPrefs.DeleteKey("W325");
            PlayerPrefs.DeleteKey("W586");
            PlayerPrefs.DeleteKey("W993");
            PlayerPrefs.DeleteKey("Ending");
            PlayerPrefs.Save();

            WasOn047 = false;
            WasOn325 = false;
            WasOn586 = false;
            WasOn993 = false;
            C047.SetActive(false);
            C325.SetActive(false);
            C586.SetActive(false);
            C993.SetActive(false);
            Ending = 0;
            PlayerPrefs.Save();
        }
    }

    void LateUpdate()
    {
        // Обработка Ending и установка соответствующих объектов
        if (Ending == 1)
        {
            C993.SetActive(true);
            Ending = 0;
            W993 = 1;
            WasOn993 = true;
        }
        else if (Ending == 2)
        {
            C325.SetActive(true);
            Ending = 0;
            W325 = 1;
            WasOn325 = true;
        }
        else if (Ending == 4)
        {
            C047.SetActive(true);
            Ending = 0;
            W047 = 1;
            WasOn047 = true;
        }
        else if (Ending == 6)
        {
            C586.SetActive(true);
            Ending = 0;
            W586 = 1;
            WasOn586 = true;
        }

        // Сохранение данных в PlayerPrefs
        PlayerPrefs.SetInt("Ending", Ending);
        PlayerPrefs.SetInt("W047", W047);
        PlayerPrefs.SetInt("W325", W325);
        PlayerPrefs.SetInt("W586", W586);
        PlayerPrefs.SetInt("W993", W993);
        PlayerPrefs.Save();
    }

    public void L000()
    {
        SceneManager.LoadScene(1);
    }

    public void L047()
    {
        SceneManager.LoadScene(2);
    }

    public void L325()
    {
        SceneManager.LoadScene(3);
    }

    public void L586()
    {
        SceneManager.LoadScene(4);
    }

    public void L993()
    {
        SceneManager.LoadScene(5);
    }
}