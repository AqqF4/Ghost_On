using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatronicMovement : MonoBehaviour
{
    public RoomNode[] rooms; // Список комнат (пустых объектов) в вентиляции
    public float hearingThreshold = 5f; // Порог слуха
    public float persistence = 3f; // Настойчивость аниматроника
    public float hearingDecayRate = 1f; // Интервал уменьшения звука в секундах
    public float moveSpeed = 2f; // Скорость перемещения аниматроника между комнатами
    public float stayTime = 5f; // Время пребывания в одной комнате

    public RandomMovement randomMovement; // Ссылка на компонент RandomMovement

    private float stayTimer = 0f; // Таймер пребывания в комнате
    public RoomNode currentRoom; // Текущая комната аниматроника
    public RoomNode targetRoom; // Целевая комната аниматроника
    public float heardSound = 0f; // Уровень услышанного звука

    public GameObject soundPrefab1; // Префаб с первым звуком
    public GameObject soundPrefab2; // Префаб со вторым звуком
    public GameObject soundPrefab3; // Префаб с третьим звуком
    private bool canPlaySound = true; // Флаг, разрешающий воспроизведение звука
    private float soundCooldown = 1.0f; // Время задержки между звуками


    private bool isMoving = false; // Флаг, указывающий, движется ли аниматроник
    private bool isWaiting = false; // Флаг, указывающий, ожидает ли аниматроник в комнате
    private bool CanHear = true; // Флаг, указывающий, может ли аниматроник слышать звуки
    private Queue<RoomNode> pathQueue = new Queue<RoomNode>(); // Очередь для хранения пути

    private Pathfinding pathfinding;

    // Метод для перемещения аниматроника обратно в предыдущую комнату
    public void MoveToPreviousRoom(RoomNode previousRoom)
    {
        StartCoroutine(MoveBackToRoom(previousRoom));
    }

    // Корутина для плавного перемещения
    private IEnumerator MoveBackToRoom(RoomNode previousRoom)
    {
        while (Vector3.Distance(transform.position, previousRoom.transform.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, previousRoom.transform.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Обновляем текущую комнату после завершения перемещения
        currentRoom = previousRoom;
        targetRoom = null; // Сбрасываем целевую комнату
        StopMoving(); // Останавливаем движение

        // Спавним звуковой префаб с шансом 60%
        if (canPlaySound)
        {
            SpawnSoundPrefabWithChance(60, previousRoom);
        }
    }

    // Метод для остановки движения
    public void StopMoving()
    {
        // Логика для остановки аниматроника
        Debug.Log("Animatronic movement stopped.");
    }

    // Метод для спавна случайного звукового префаба с определенным шансом
    private void SpawnSoundPrefabWithChance(int chancePercent, RoomNode previousRoom)
    {
        int randomValue = Random.Range(0, 100);
        if (randomValue < chancePercent)
        {
            // Выбираем случайный префаб из трех доступных
            GameObject selectedPrefab = null;
            int prefabIndex = Random.Range(0, 3);
            switch (prefabIndex)
            {
                case 0:
                    selectedPrefab = soundPrefab1;
                    break;
                case 1:
                    selectedPrefab = soundPrefab2;
                    break;
                case 2:
                    selectedPrefab = soundPrefab3;
                    break;
            }

            // Спавним выбранный префаб
            if (selectedPrefab != null)
            {
                GameObject soundObject = Instantiate(selectedPrefab, transform.position, Quaternion.identity);

                // Получаем компонент AudioSource и устанавливаем значение Stereo Pan
                AudioSource audioSource = soundObject.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    // Изменяем Stereo Pan в зависимости от направления движения
                    if (previousRoom.roomNumber == 11) // Если аниматроник возвращается в левую комнату
                    {
                        audioSource.panStereo = -1.0f; // Левый канал
                    }
                    else if (previousRoom.roomNumber == 13) // Если аниматроник возвращается в правую комнату
                    {
                        audioSource.panStereo = 1.0f; // Правый канал
                    }
                }

                canPlaySound = false;
                StartCoroutine(SoundCooldownCoroutine());
            }
        }
    }

    // Корутина для восстановления возможности воспроизведения звука
    private IEnumerator SoundCooldownCoroutine()
    {
        yield return new WaitForSeconds(soundCooldown);
        canPlaySound = true;
    }

    void Start()
    {
        if (rooms == null || rooms.Length == 0)
        {
            Debug.LogError("Rooms array is not initialized or empty.");
            return;
        }

        // Инициализация текущей комнаты
        currentRoom = GetCurrentRoom();
        if (currentRoom == null)
        {
            Debug.LogError("Current room could not be determined.");
            return;
        }
        Debug.Log("Current room successfully determined: " + currentRoom.name);

        stayTimer = stayTime; // Установить начальное время ожидания
        StartCoroutine(HearingDecayRoutine());

        pathfinding = GetComponent<Pathfinding>();
        if (pathfinding == null)
        {
            Debug.LogError("Pathfinding component not found.");
            return;
        }

        // Обеспечиваем, что RandomMovement инициализирован
        if (randomMovement == null)
        {
            Debug.LogError("RandomMovement component not assigned.");
        }
    }

    void Update()
    {
        if (targetRoom != null)
        {
            CanHear = false; // Отключаем слух, если установлена целевая комната
        }

        if (targetRoom == currentRoom)
        {
            heardSound = 0f;
            CanHear = true;
            isMoving = false;
            isWaiting = false;
            targetRoom = null;
        }

        if (isWaiting)
        {
            HandleWaiting();
        }
        else if (isMoving)
        {
            MoveTowardsTarget();
        }

        // Прерываем ожидание случайного движения, если слышен звук
        if (heardSound > 0f && isWaiting)
        {
            StopRandomMovementWaiting();
        }
    }

    public void HearSound(float soundLevel, Vector3 soundPosition)
    {
        if (CanHear)
        {
            Debug.Log("Sound heard at position: " + soundPosition + " with level: " + soundLevel);

            heardSound += Mathf.Max(heardSound, soundLevel);
            UpdateTargetRoom(soundPosition);

            // Если звук громче порога, обновляем уровень услышанного звука
            if (soundLevel > hearingThreshold && soundLevel >= persistence)
            {
                Debug.Log("Animatronic has heard the sound.");
            }
            else
            {
                Debug.Log("Sound level is below animatronic persistence.");
            }
        }
    }

    public void StartRandomMovementWaiting(float waitTime)
    {
        CanHear = true;
        stayTimer = stayTime;
        isWaiting = true;
        StartCoroutine(RandomMovementWaitingRoutine());
    }

    public void StopRandomMovementWaiting()
    {
        if (isWaiting)
        {
            isWaiting = false;
            stayTimer = 0f;
            Debug.Log("Random movement waiting was stopped due to detected sound.");
        }
    }

    private IEnumerator RandomMovementWaitingRoutine()
    {
        while (stayTimer > 0)
        {
            stayTimer -= Time.deltaTime;

            if (!isWaiting)
                yield break; // Прерываем, если ожидание было отменено

            yield return null;
        }

        isWaiting = false;
        isMoving = true;
        CanHear = false;
        heardSound = 0f; // Сбрасываем heardSound
        Debug.Log("Finished waiting for random movement, ready to move.");
    }

    void HandleWaiting()
    {
        CanHear = false;

        if (stayTimer > 0)
        {
            stayTimer -= Time.deltaTime;
        }
        else
        {
            // Таймер истёк, переходим к следующему состоянию
            isWaiting = false;
            isMoving = true;
            CanHear = false;
            heardSound = 0f; // Сбрасываем heardSound
            Debug.Log("Finished waiting, ready to move.");
        }
    }

    public void SetTargetRoom(RoomNode room)
    {
        if (room == null || room == currentRoom) return;

        targetRoom = room;
        pathQueue = new Queue<RoomNode>(pathfinding.FindPath(currentRoom, targetRoom));
        isMoving = pathQueue.Count > 0;
        isWaiting = !isMoving;
        stayTimer = stayTime; // Устанавливаем таймер ожидания
        Debug.Log("New target room set: " + targetRoom.name);
    }

    public RoomNode GetCurrentRoom()
    {
        if (rooms == null || rooms.Length == 0)
        {
            Debug.LogError("Rooms array is not initialized or empty.");
            return null;
        }

        foreach (RoomNode room in rooms)
        {
            if (room == null)
            {
                Debug.LogWarning("Encountered a null room in the rooms array.");
                continue;
            }

            float distance = Vector3.Distance(transform.position, room.transform.position);
            if (distance < 0.1f)
            {
                return room;
            }
            
        }

        return null;
    }

    void MoveTowardsTarget()
    {
        if (targetRoom == null || pathQueue.Count == 0) return;

        // Двигаемся к следующей комнате в пути
        RoomNode nextRoom = pathQueue.Peek();
        Vector3 targetPosition = nextRoom.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Проверяем, достигли ли мы следующей комнаты
        if (Vector3.Distance(transform.position, targetPosition) < 0.0001f)
        {
            // Устанавливаем текущую комнату как достигнутую
            currentRoom = pathQueue.Dequeue(); 

            if (pathQueue.Count > 0)
            {
                // Если есть еще комнаты в пути, ожидаем в текущей комнате
                isMoving = false;
                isWaiting = true;
                stayTimer = stayTime; // Устанавливаем таймер ожидания
                Debug.Log("Arrived at room: " + currentRoom.name + ". Waiting...");
            }
            else
            {
                // Если путь исчерпан, аниматроник завершает движение
                isMoving = false;
                isWaiting = false;
                CanHear = true;
                stayTimer = stayTime; // Таймер ожидания не нужен после достижения конечной цели
                Debug.Log("Arrived at target room: " + currentRoom.name);
            }
        }
    }

    void UpdateTargetRoom(Vector3 soundPosition)
    {
        if (heardSound <= persistence) return;

        // Находим ближайшую комнату к источнику звука
        RoomNode nearestRoom = FindNearestRoom(soundPosition);
        if (nearestRoom != null && nearestRoom != currentRoom)
        {
            targetRoom = nearestRoom;
            pathQueue = new Queue<RoomNode>(pathfinding.FindPath(currentRoom, targetRoom));

            // Устанавливаем флаги для начала движения
            isMoving = pathQueue.Count > 0;
            isWaiting = !isMoving;
            stayTimer = stayTime; // Устанавливаем таймер ожидания

            heardSound = 0f; // Сбрасываем heardSound
            Debug.Log("New target room set: " + targetRoom.name);
        }
        else
        {
            Debug.Log("No suitable target room found or it is the current room.");
        }
    }

    RoomNode FindNearestRoom(Vector3 soundPosition)
    {
        if (rooms == null || rooms.Length == 0)
        {
            Debug.LogError("Rooms array is not initialized or empty.");
            return null;
        }

        RoomNode nearestRoom = null;
        float maxSoundLevel = -1f;

        foreach (RoomNode room in rooms)
        {
            if (room == null)
            {
                Debug.LogWarning("Encountered a null room in the rooms array.");
                continue;
            }

            float distance = Vector2.Distance((Vector2)soundPosition, (Vector2)room.transform.position);
            float soundLevelAtRoom = heardSound - (distance * hearingDecayRate);

            Debug.Log($"Checking room {room.name}. Distance: {distance}. Sound Level at Room: {soundLevelAtRoom}");

            if (soundLevelAtRoom > maxSoundLevel)
            {
                maxSoundLevel = soundLevelAtRoom;
                nearestRoom = room;
            }
        }

        return nearestRoom;
    }

    IEnumerator HearingDecayRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(hearingDecayRate);
            if (heardSound > 0)
            {
                heardSound -= 1f;

                if (heardSound <= 0 && !isMoving)
                {
                    isWaiting = true;
                    Debug.Log("Sound level dropped to zero, staying in current room: " + currentRoom.name);
                }
            }
        }
    }
}