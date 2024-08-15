using System; // Пространство имён для Action
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasks : MonoBehaviour
{
    public GameObject[] TaskSounds;
    public GameObject ClickSound;
    public SoundGenerator Player;
    public VENT_Controler Cam;
    public float Noiseness;

    public float taskDuration = 10f; // Время выполнения задачи в секундах

    private float taskTimer = 0f; // Таймер задачи
    private bool isTaskActive = false; // Флаг, указывающий на выполнение задачи

    public event Action TaskCompleted; // Событие, уведомляющее о завершении задания

    GameObject PlayableSound;
    GameObject Loading;

    private TaskManager taskManager; // Ссылка на TaskManager

    void Start()
    {
        // Ищем TaskManager в сцене
        taskManager = FindObjectOfType<TaskManager>();

        // Ищем объект с компонентом, который называется Loading, среди дочерних объектов
        Loading = GetComponentInChildren<Transform>().Find("Loading")?.gameObject;

        // Если объект Loading не найден, выводим сообщение в консоль
        if (Loading == null)
        {
            Debug.LogError("Loading object not found as a child of " + gameObject.name);
        }
    }

    void Update()
    {
        // Если задача активна, запускаем таймер
        if (isTaskActive)
        {
            taskTimer -= Time.deltaTime; // Уменьшаем таймер

            // Если таймер истек, останавливаем задачу
            if (taskTimer <= 0f)
            {
                StopTask();
            }
        }
    }

    public void DoTask()
    {
        if (!isTaskActive)
        {
            isTaskActive = true;
            taskTimer = taskDuration; // Устанавливаем начальное значение таймера

            if (Loading != null)
            {
                Loading.SetActive(true); // Включаем объект Loading, если он существует
            }

            // Воспроизводим случайный звук задачи
            PlayableSound = Instantiate(TaskSounds[UnityEngine.Random.Range(0, TaskSounds.Length)], transform.position, Quaternion.identity);

            Increase();
        }
    }

    public void StopTask()
    {
        if (isTaskActive) // Проверяем, что задача активна перед остановкой
        {
            if (Loading != null)
            {
                Loading.SetActive(false); // Отключаем объект Loading, если он существует
            }

            Dismiss();

            // Уничтожаем звук задачи, если он существует
            if (PlayableSound != null)
            {
                Destroy(PlayableSound);
            }

            // Воспроизводим звук клика
            Instantiate(ClickSound, transform.position, Quaternion.identity);

            isTaskActive = false; // Сбрасываем флаг активности задачи

            TaskCompleted?.Invoke(); // Вызываем событие завершения задания

            // Уведомляем TaskManager о завершении задачи и передаем текущий объект для удаления
            if (taskManager != null)
            {
                taskManager.TaskCompleted(gameObject);
            }

            
        }
    }

    public void NonEndedStop()
    {
        if (Loading != null)
        {
            Loading.SetActive(false); // Отключаем объект Loading, если он существует
        }

        // Уничтожаем звук задачи, если он существует
        if (PlayableSound != null)
        {
            Destroy(PlayableSound);
        }
        
        Dismiss();
        isTaskActive = false; // Сбрасываем флаг активности задачи
        Debug.Log("Task Stopped");
        
    }

    void Increase()
    {
        if (isTaskActive) // Проверяем, что задача активна
        {
            Player.soundLevel += Noiseness;
            Debug.Log("Increased");
        }
    }

    void Dismiss()
    {
        if (isTaskActive) // Проверяем, что задача активна перед уменьшением
        {
            Player.soundLevel -= Noiseness;
            Debug.Log("Dismissed");
        }
    }
}
