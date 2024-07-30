using System.Collections.Generic;
using UnityEngine;

public class AnimatronicMovement : MonoBehaviour
{
    public RoomNode[] rooms; // Список комнат (пустых объектов) в вентиляции
    public float hearingThreshold = 5f; // Порог слуха
    public float persistence = 3f; // Настойчивость аниматроника
    public float hearingDecayRate = 1f; // Скорость уменьшения звука в единицах в секунду
    public float moveSpeed = 2f; // Скорость перемещения аниматроника между комнатами

    public float stayTime = 5f; // Время пребывания в одной комнате
    private float stayTimer = 0f; // Таймер пребывания в комнате

    private RoomNode currentRoom; // Текущая комната аниматроника
    private RoomNode targetRoom; // Целевая комната аниматроника
    private Queue<RoomNode> path; // Путь до целевой комнаты
    private float heardSound = 0f; // Уровень услышанного звука

    private bool isMoving = false; // Флаг, указывающий, движется ли аниматроник
    private bool isWaiting = false; // Флаг, указывающий, ожидает ли аниматроник в комнате

    private Dictionary<RoomNode, float> soundLevels = new Dictionary<RoomNode, float>(); // Словарь уровней звука для каждой комнаты

    void Start()
    {
        if (rooms == null || rooms.Length == 0)
        {
            Debug.LogError("Rooms array is not set or empty");
            return;
        }

        currentRoom = FindNearestRoom(transform.position); // Установить начальную позицию аниматроника в ближайшую комнату
        if (currentRoom != null)
        {
            transform.position = currentRoom.transform.position; // Начальная позиция аниматроника
            stayTimer = stayTime; // Установить таймер пребывания в комнате
        }
        else
        {
            Debug.LogError("Current room not found");
        }
    }

    void Update()
    {
        if (isMoving)
        {
            if (isWaiting)
            {
                StayInRoom();
            }
            else
            {
                MoveAlongPath();
            }
        }
    }

    // Метод для установки уровня услышанного звука
    public void HearSound(float soundLevel, Vector3 soundPosition)
    {
        RoomNode soundRoom = FindNearestRoom(soundPosition);

        if (soundRoom != null)
        {
            if (soundLevels.ContainsKey(soundRoom))
            {
                soundLevels[soundRoom] = Mathf.Max(soundLevels[soundRoom], soundLevel);
            }
            else
            {
                soundLevels.Add(soundRoom, soundLevel);
            }

            // Выбираем комнату с максимальным уровнем звука
            RoomNode highestSoundRoom = GetRoomWithHighestSound();
            if (highestSoundRoom != null)
            {
                targetRoom = highestSoundRoom;
                path = new Queue<RoomNode>(Pathfinding.FindPath(currentRoom, targetRoom));
                isMoving = true; // Начать движение
                isWaiting = false; // Убедиться, что аниматроник не ожидает в комнате
            }
        }
    }

    // Метод для нахождения комнаты с наибольшим уровнем звука
    RoomNode GetRoomWithHighestSound()
    {
        RoomNode highestSoundRoom = null;
        float maxSoundLevel = -1f;

        foreach (var kvp in soundLevels)
        {
            if (kvp.Value > maxSoundLevel)
            {
                maxSoundLevel = kvp.Value;
                highestSoundRoom = kvp.Key;
            }
        }

        return highestSoundRoom;
    }

    // Метод для поиска ближайшей комнаты к заданной позиции
    RoomNode FindNearestRoom(Vector3 position)
    {
        if (rooms == null || rooms.Length == 0) return null;

        RoomNode nearestRoom = null;
        float minDistance = float.MaxValue;

        foreach (RoomNode room in rooms)
        {
            float distance = Vector3.Distance(room.transform.position, position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestRoom = room;
            }
        }

        return nearestRoom;
    }

    // Метод для перемещения аниматроника по пути
    void MoveAlongPath()
    {
        if (path == null || path.Count == 0) return;

        // Получить следующую комнату в пути
        RoomNode nextRoom = path.Peek(); // Не удаляем из очереди, пока не достигнем

        transform.position = Vector3.MoveTowards(transform.position, nextRoom.transform.position, moveSpeed * Time.deltaTime);

        // Если аниматроник достиг следующей комнаты, начать ожидание
        if (Vector3.Distance(transform.position, nextRoom.transform.position) < 0.1f)
        {
            currentRoom = path.Dequeue(); // Удаляем комнату из очереди
            isWaiting = true; // Включаем режим ожидания
        }
    }

    // Метод для ожидания в комнате
    void StayInRoom()
    {
        stayTimer -= Time.deltaTime;
        if (stayTimer <= 0)
        {
            stayTimer = stayTime; // Сброс таймера
            isWaiting = false; // Возобновить движение
            if (path.Count > 0)
            {
                // Продолжить движение, если в очереди есть еще комнаты
                isMoving = true;
            }
            else
            {
                // Если путь завершен, остановить движение
                isMoving = false;
            }
        }
    }
}