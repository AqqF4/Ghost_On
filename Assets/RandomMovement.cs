using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public AnimatronicMovement animatronic; // Ссылка на объект аниматроника
    public float minInterval = 2f; // Минимальный интервал времени
    public float maxInterval = 5f; // Максимальный интервал времени

    private float timer = 0f; // Таймер для отслеживания времени

    float cooldownTimer;

    void Update()
    {
        if(cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            if(animatronic.heardSound == 0f)
            {
            timer += Time.deltaTime;

            if (timer >= Random.Range(minInterval, maxInterval))
            {
                timer = 0f;

                RoomNode currentRoom = animatronic.GetCurrentRoom();

                RoomNode randomRoom = null;
                if (currentRoom != null && currentRoom.neighbors != null && currentRoom.neighbors.Count > 0)
                {
                    do
                    {
                        randomRoom = currentRoom.neighbors[Random.Range(0, currentRoom.neighbors.Count)];
                    } while (randomRoom == currentRoom);

                    
                    if(animatronic.targetRoom == null)
                    {
                        animatronic.SetTargetRoom(randomRoom);
                    }
                    
                    // Запуск ожидания перед случайным движением
                    animatronic.StartRandomMovementWaiting(Random.Range(minInterval, maxInterval));
                    cooldownTimer = animatronic.stayTime;
                }
            }
            }
        }
    }

    public void ResetTimer()
    {
        timer = 0f;
    }


}