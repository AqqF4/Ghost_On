using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTeleportation : MonoBehaviour
{
    public GameObject[] canSpawnRooms; // Список комнат, в которые можно переместиться
    public GameObject animatronic; // Ссылка на объект аниматроника

    void Awake()
    {
        // Убедитесь, что список комнат и аниматроник инициализированы
        if (canSpawnRooms == null || canSpawnRooms.Length == 0)
        {
            Debug.LogError("Can-Spawn-Rooms array is not initialized or empty.");
            return;
        }

        if (animatronic == null)
        {
            Debug.LogError("Animatronic object is not assigned.");
            return;
        }

        // Перемещение аниматроника в случайную комнату
        TeleportAnimatronicToRandomRoom();
    }

    void TeleportAnimatronicToRandomRoom()
    {
        // Выбираем случайную комнату из списка
        GameObject randomRoom = canSpawnRooms[Random.Range(0, canSpawnRooms.Length)];

        if(randomRoom.GetComponent<RoomNode>().isFree)
        {
            // Перемещаем аниматроника в выбранную комнату
            animatronic.transform.position = randomRoom.GetComponent<Transform>().position;

            randomRoom.GetComponent<RoomNode>().isFree = false;
        }
        else
        {
            // Выбираем случайную комнату из списка
            randomRoom = canSpawnRooms[Random.Range(0, canSpawnRooms.Length)];
        }

    }
}