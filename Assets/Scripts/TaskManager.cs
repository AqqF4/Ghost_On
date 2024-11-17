using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    public int CompletedTasks = 0;  // Счётчик выполненных заданий
    public int NeededTasks;         // Необходимое количество заданий для завершения всех
    public int EndingScene;
    public NightSaver ns;

    public bool AllTasksCompleted = false;  // Флаг завершения всех заданий

    public void TaskCompleted(GameObject taskObject)
    {
        // Увеличиваем счётчик выполненных заданий
        CompletedTasks++;

        // Проверяем, достигли ли необходимого количества выполненных заданий
        if (CompletedTasks >= NeededTasks)
        {
            AllTasksCompleted = true;
            Debug.Log("All tasks are completed!");
        }

        // Удаляем объект, в котором задание выполнено
        Destroy(taskObject);
    }


    public void SaveTasks()
    {
        if(AllTasksCompleted)
        {
            ns.IncrementNight();
            SceneManager.LoadScene(EndingScene);
        }
    }
}
