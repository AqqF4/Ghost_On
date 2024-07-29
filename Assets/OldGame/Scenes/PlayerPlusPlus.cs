using System;
using System.Reflection;
using UnityEngine;

public class PlayerPlusPlus : MonoBehaviour
{
    public PlayerTook Pt0;
    public PlayerTook Pt1;
    public PlayerTook Pt2;

    private FieldInfo[] boolFields;
    private bool[] previousValuesPt0;
    private bool[] previousValuesPt1;
    private bool[] previousValuesPt2;

    void Start()
    {
        // Получаем все поля типа bool из класса PlayerTook
        boolFields = typeof(PlayerTook).GetFields(BindingFlags.Public | BindingFlags.Instance);

        // Инициализируем массивы для хранения предыдущих значений полей
        previousValuesPt0 = new bool[boolFields.Length];
        previousValuesPt1 = new bool[boolFields.Length];
        previousValuesPt2 = new bool[boolFields.Length];

        // Сохраняем начальные значения полей
        SavePreviousValues(Pt0, previousValuesPt0);
        SavePreviousValues(Pt1, previousValuesPt1);
        SavePreviousValues(Pt2, previousValuesPt2);
    }

    void Update()
    {
        for (int i = 0; i < boolFields.Length; i++)
        {
            // Пропускаем поля, которые не являются булевыми
            if (boolFields[i].FieldType != typeof(bool))
                continue;

            // Проверяем изменения для Pt0
            bool currentValuePt0 = (bool)boolFields[i].GetValue(Pt0);
            if (currentValuePt0 != previousValuesPt0[i])
            {
                UpdateFieldForAll(boolFields[i], currentValuePt0);
                SavePreviousValues(Pt0, previousValuesPt0);
                SavePreviousValues(Pt1, previousValuesPt1);
                SavePreviousValues(Pt2, previousValuesPt2);
                break; // Прерываем цикл, чтобы избежать одновременной обработки всех изменений
            }

            // Проверяем изменения для Pt1
            bool currentValuePt1 = (bool)boolFields[i].GetValue(Pt1);
            if (currentValuePt1 != previousValuesPt1[i])
            {
                UpdateFieldForAll(boolFields[i], currentValuePt1);
                SavePreviousValues(Pt0, previousValuesPt0);
                SavePreviousValues(Pt1, previousValuesPt1);
                SavePreviousValues(Pt2, previousValuesPt2);
                break;
            }

            // Проверяем изменения для Pt2
            bool currentValuePt2 = (bool)boolFields[i].GetValue(Pt2);
            if (currentValuePt2 != previousValuesPt2[i])
            {
                UpdateFieldForAll(boolFields[i], currentValuePt2);
                SavePreviousValues(Pt0, previousValuesPt0);
                SavePreviousValues(Pt1, previousValuesPt1);
                SavePreviousValues(Pt2, previousValuesPt2);
                break;
            }
        }
    }

    private void SavePreviousValues(PlayerTook playerTook, bool[] previousValues)
    {
        for (int i = 0; i < boolFields.Length; i++)
        {
            // Пропускаем поля, которые не являются булевыми
            if (boolFields[i].FieldType != typeof(bool))
                continue;

            previousValues[i] = (bool)boolFields[i].GetValue(playerTook);
        }
    }

    private void UpdateFieldForAll(FieldInfo field, bool newValue)
    {
        // Пропускаем поля, которые не являются булевыми
        if (field.FieldType != typeof(bool))
            return;

        field.SetValue(Pt0, newValue);
        field.SetValue(Pt1, newValue);
        field.SetValue(Pt2, newValue);
    }
}