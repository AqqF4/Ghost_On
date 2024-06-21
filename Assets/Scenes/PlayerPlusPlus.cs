using System;
using UnityEngine;

public class PlayerPlusPlus : MonoBehaviour
{
    // ������ �� ���������� PlayerTook
    public PlayerTook Pt0;
    public PlayerTook Pt1;
    public PlayerTook Pt2;

    void Start()
    {
        // ������������� �� ������� ��������� ���������� ��� ������� PlayerTook
        Pt0.OnVariableChanged += OnVariableChangedHandler;
        Pt1.OnVariableChanged += OnVariableChangedHandler;
        Pt2.OnVariableChanged += OnVariableChangedHandler;
    }

    // �����-���������� ������� ��������� ����������
    private void OnVariableChangedHandler(string fieldName, bool value)
    {
        // ��������� ��������� �� ���� PlayerTook
        ApplyChange(Pt0, fieldName, value);
        ApplyChange(Pt1, fieldName, value);
        ApplyChange(Pt2, fieldName, value);
    }

    // ����� ��� ���������� ��������� � PlayerTook
    private void ApplyChange(PlayerTook playerTook, string fieldName, bool value)
    {
        switch (fieldName)
        {
            case "hasMarker":
                playerTook.hasMarker = value;
                break;
            case "hasFlashlight":
                playerTook.hasFlashlight = value;
                break;
            case "hasGun":
                playerTook.hasGun = value;
                break;
            case "hasLadder":
                playerTook.hasLadder = value;
                break;
            case "hasKey":
                playerTook.hasKey = value;
                break;
            case "hasGravity":
                playerTook.hasGravity = value;
                break;
            case "hasPassword":
                playerTook.hasPassword = value;
                break;
            case "turnedLight":
                playerTook.turnedLight = value;
                break;
            case "hasBucket":
                playerTook.hasBucket = value;
                break;
            default:
                Debug.LogError("Unknown field name: " + fieldName);
                break;
        }
    }
}
