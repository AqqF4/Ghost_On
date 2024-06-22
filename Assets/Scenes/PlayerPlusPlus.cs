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
        // Получаем все поля типа bool из класса PlayerTook
        boolFields = typeof(PlayerTook).GetFields(BindingFlags.Public | BindingFlags.Instance);

        // Подписываемся на изменение всех булевых полей для каждого PlayerTook
        SubscribeToFieldChanges(Pt0);
        SubscribeToFieldChanges(Pt1);
        SubscribeToFieldChanges(Pt2);
    }

    private void SubscribeToFieldChanges(PlayerTook playerTook)
    {
        foreach (var field in boolFields)
        {
            // Получаем текущее значение поля
            bool currentValue = (bool)field.GetValue(playerTook);

            // Подписываемся на событие изменения поля
            StartCoroutine(WatchField(playerTook, field, currentValue));
        }
    }

    private IEnumerator WatchField(PlayerTook playerTook, FieldInfo field, bool initialValue)
    {
        while (true)
        {
            // Проверяем значение поля каждую долю секунды
            yield return new WaitForSeconds(0.1f);

            bool currentValue = (bool)field.GetValue(playerTook);
            if (currentValue != initialValue)
            {
                // Если значение изменилось, обновляем его у всех PlayerTook
                UpdateFieldForAll(field, currentValue);

                // Обновляем начальное значение
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