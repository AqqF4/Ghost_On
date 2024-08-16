using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatronicBlocker : MonoBehaviour
{
    public GameObject rightSign;  // Объект, отвечающий за правую сторону
    public GameObject leftSign;   // Объект, отвечающий за левую сторону

    public bool BlocksRight { get; private set; } // Флаг блокировки движения вправо
    public bool BlocksLeft { get; private set; }  // Флаг блокировки движения влево

    public AnimatronicMovement[] animatronics; // Список всех аниматроников
    public RoomNode LeftRoom;  // Комната 11
    public RoomNode RightRoom; // Комната 13

    void Update()
    {
        // Проверяем активен ли rightSign, если да - блокируем движение вправо, иначе - разблокируем
        BlocksRight = !rightSign.activeSelf;

        // Проверяем активен ли leftSign, если да - блокируем движение влево, иначе - разблокируем
        BlocksLeft = !leftSign.activeSelf;

        // Проверяем движение аниматроников
        foreach (AnimatronicMovement animatronic in animatronics)
        {
            if (animatronic == null) continue;

            // Проверяем, если аниматроник движется к комнате 12 из комнаты 11 и блокировка активна
            if (animatronic.targetRoom != null && animatronic.targetRoom.roomNumber == 12 && animatronic.currentRoom == LeftRoom && BlocksLeft)
            {
                // Перемещаем аниматроника обратно в 11 комнату
                animatronic.MoveToPreviousRoom(LeftRoom);
                Debug.Log("Movement to room 12 blocked. Animatronic moving back to room 11.");
            }
            // Проверяем, если аниматроник движется к комнате 12 из комнаты 13 и блокировка активна
            else if (animatronic.targetRoom != null && animatronic.targetRoom.roomNumber == 12 && animatronic.currentRoom == RightRoom && BlocksRight)
            {
                // Перемещаем аниматроника обратно в 13 комнату
                animatronic.MoveToPreviousRoom(RightRoom);
                Debug.Log("Movement to room 12 blocked. Animatronic moving back to room 13.");
            }
        }
    }
}