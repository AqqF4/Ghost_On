using UnityEngine;

public class JumpscareTrigger : MonoBehaviour
{
    public Animator anim; // Ссылка на компонент Animator
    public int targetRoomNumber = 12; // Номер комнаты, при достижении которой запускается Jumpscare

    private AnimatronicMovement animatronicMovement;

    void Start()
    {
        // Получаем ссылку на компонент AnimatronicMovement
        animatronicMovement = GetComponent<AnimatronicMovement>();

        if (anim == null)
        {
            Debug.LogError("Animator is not assigned.");
        }
    }

    void Update()
    {
        if (animatronicMovement != null && anim != null)
        {
            // Проверяем, если текущая комната - это комната с нужным номером
            if (animatronicMovement.GetCurrentRoom() != null && animatronicMovement.GetCurrentRoom().roomNumber == targetRoomNumber)
            {
                anim.SetTrigger("Jumpscare");
                Debug.Log("Jumpscare triggered in room: " + targetRoomNumber);
            }
        }
    }
}
