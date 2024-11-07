using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCheckManager : MonoBehaviour
{
    public SoundCheck soundCheck; // Ссылка на скрипт SoundCheck
    public GameObject continueButton; // Кнопка продолжения
    public GameObject pauseButton; // Кнопка паузы
    public int currentSoundIndex = 0; // Индекс текущего звука
    public bool isPaused = false; // Флаг состояния паузы

    void Update()
    {
        // Проверка, закончился ли звук
        if (!isPaused && !soundCheck.audioSource.isPlaying)
        {
            // Если отметка не нужна, сразу переход к следующему звуку
            if (!soundCheck.HasCheckMark[currentSoundIndex - 1])
            {
                PlayNextSound();
            }
            else
            {
                // Показать кнопку для установки отметки
                continueButton.SetActive(true);
                pauseButton.SetActive(false);
            }
        }
    }

    public void OnContinueButtonClicked()
    {
        if (isPaused)
        {
            soundCheck.ResumeSound();
            isPaused = false;
            continueButton.SetActive(false);
            pauseButton.SetActive(true);
        }
    }

    public void OnPauseButtonClicked()
    {
        if (!isPaused)
        {
            soundCheck.PauseSound();
            isPaused = true;
            continueButton.SetActive(true);
            pauseButton.SetActive(false);
        }
    }

    public void PlayNextSound()
    {
        if (currentSoundIndex < soundCheck.soundClips.Length)
        {
            soundCheck.PlaySound(currentSoundIndex);
            
            // Проверяем, должна ли быть установлена отметка для текущего звука
            if (soundCheck.HasCheckMark[currentSoundIndex])
            {
                pauseButton.SetActive(true); // Показать кнопку паузы для установки отметки
            }
            else
            {
                // Если отметка не требуется, сразу переходим к следующему звуку
                currentSoundIndex++;
                PlayNextSound();
                return;
            }

            currentSoundIndex++;
        }
        else
        {
            Debug.Log("Все звуки были проиграны.");
        }
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}