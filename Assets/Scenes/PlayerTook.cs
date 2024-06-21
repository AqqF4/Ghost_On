using System;
using UnityEngine;

public class PlayerTook : MonoBehaviour
{
    // Булевые переменные
    public bool hasMarker;
    public bool hasFlashlight;
    public bool hasGun;
    public bool hasLadder;
    public bool hasKey;
    public bool hasGravity;
    public bool hasPassword;
    public bool turnedLight;
    public bool hasBucket;

    // Событие для уведомления об изменении переменных
    public event Action<string, bool> OnVariableChanged;

    // Метод для изменения значения переменной и вызова события
    public void SetField(string fieldName, bool value)
    {
        switch (fieldName)
        {
            case "hasMarker":
                if (hasMarker != value)
                {
                    hasMarker = value;
                    OnVariableChanged?.Invoke("hasMarker", value);
                }
                break;
            case "hasFlashlight":
                if (hasFlashlight != value)
                {
                    hasFlashlight = value;
                    OnVariableChanged?.Invoke("hasFlashlight", value);
                }
                break;
            case "hasGun":
                if (hasGun != value)
                {
                    hasGun = value;
                    OnVariableChanged?.Invoke("hasGun", value);
                }
                break;
            case "hasLadder":
                if (hasLadder != value)
                {
                    hasLadder = value;
                    OnVariableChanged?.Invoke("hasLadder", value);
                }
                break;
            // Добавьте обработку для остальных переменных...
            default:
                Debug.LogError("Unknown field name: " + fieldName);
                break;
        }
    }
}
