using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public AnimatronicMovement animatronic; // Ссылка на объект аниматроника
    public float minInterval = 2f; // Минимальный интервал времени
    public float maxInterval = 5f; // Максимальный интервал времени

    private float timer = 0f; // Таймер для отслеживания времени

    void Update()
    {

        // Обновляем таймер
        timer += Time.deltaTime;

        if (timer >= Random.Range(minInterval, maxInterval))
        {
            // Сброс таймера
            timer = 0f;

            // Получаем текущую комнату аниматроника
            RoomNode currentRoom = animatronic.GetCurrentRoom();

            // Случайная соседняя комната, отличная от текущей
            RoomNode randomRoom = null;
            if (currentRoom != null && currentRoom.neighbors != null && currentRoom.neighbors.Count > 0)
            {
                do
                {
                    randomRoom = currentRoom.neighbors[Random.Range(0, currentRoom.neighbors.Count)];
                } while (randomRoom == currentRoom);

                // Устанавливаем новую целевую комнату для аниматроника
                animatronic.SetTargetRoom(randomRoom);
            }
        }
    }
}