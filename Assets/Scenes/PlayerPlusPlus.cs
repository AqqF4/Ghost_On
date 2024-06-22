using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public class PlayerPlusPlus : MonoBehaviour
{
    public PlayerTook Pt0;
    public PlayerTook Pt1;
    public PlayerTook Pt2;

    private FieldInfo[] boolFields;

    void Start()
    {
        // �������� ��� ���� ���� bool �� ������ PlayerTook
        boolFields = typeof(PlayerTook).GetFields(BindingFlags.Public | BindingFlags.Instance);

        // ������������� �� ��������� ���� ������� ����� ��� ������� PlayerTook
        SubscribeToFieldChanges(Pt0);
        SubscribeToFieldChanges(Pt1);
        SubscribeToFieldChanges(Pt2);
    }

    private void SubscribeToFieldChanges(PlayerTook playerTook)
    {
        foreach (var field in boolFields)
        {
            // �������� ������� �������� ����
            bool currentValue = (bool)field.GetValue(playerTook);

            // ������������� �� ������� ��������� ����
            StartCoroutine(WatchField(playerTook, field, currentValue));
        }
    }

    private IEnumerator WatchField(PlayerTook playerTook, FieldInfo field, bool initialValue)
    {
        while (true)
        {
            // ��������� �������� ���� ������ ���� �������
            yield return new WaitForSeconds(0.1f);

            bool currentValue = (bool)field.GetValue(playerTook);
            if (currentValue != initialValue)
            {
                // ���� �������� ����������, ��������� ��� � ���� PlayerTook
                UpdateFieldForAll(field, currentValue);

                // ��������� ��������� ��������
                initialValue = currentValue;
            }
        }
    }

    private void UpdateFieldForAll(FieldInfo field, bool newValue)
    {
        field.SetValue(Pt0, newValue);
        field.SetValue(Pt1, newValue);
        field.SetValue(Pt2, newValue);
    }
}