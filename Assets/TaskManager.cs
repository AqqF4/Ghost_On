using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public Tasks[] allTasks; // Массив всех заданий на сцене
    public int CompletedTasks { get; private set; } = 0; // Количество выполненных заданий
    public bool AllTasksCompleted { get; private set; } = false; // Флаг выполнения всех заданий

    public int NeededTasks // Количество всех заданий
    {
        get { return allTasks.Length; }
    }

    void Start()
    {
        // Подписываемся на события завершения заданий
        foreach (var task in allTasks)
        {
            task.TaskCompleted += OnTaskCompleted;
        }
    }

    private void OnTaskCompleted()
    {
        CompletedTasks++; // Увеличиваем количество выполненных заданий

        // Проверяем, достигнуто ли нужное количество выполненных заданий
        if (CompletedTasks >= NeededTasks)
        {
            AllTasksCompleted = true; // Устанавливаем флаг выполнения всех заданий
            Debug.Log("All tasks completed!");
        }
    }

    void OnDestroy()
    {
        // Отписываемся от событий при уничтожении объекта
        foreach (var task in allTasks)
        {
            task.TaskCompleted -= OnTaskCompleted;
        }
    }
}