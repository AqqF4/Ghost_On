using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCheckManager : MonoBehaviour
{
    public SoundCheck soundCheck; // Ссылка на скрипт SoundCheck
    public GameObject continueButton, listButton, List, shockButton, dislistButton; // Кнопка продолжения
    public GameObject pauseButton; // Кнопка паузы
    public int currentSoundIndex = 0; // Индекс текущего звука
    public bool isPaused = false; // Флаг состояния паузы
    public Electricity e;
    public bool isRespawning = false; // Флаг состояния респауна

    void Update()
    {
        if(shockButton.activeSelf)
        {
            if(!pauseButton.activeSelf)
            {
                continueButton.SetActive(true);
            }
            else
            {
                pauseButton.SetActive(true);
            }
            
        }
        else
        {
            continueButton.SetActive(false);
            pauseButton.SetActive(false);
        }

        // Если идет респаун, не выполняем обновления кнопок
        if (isRespawning)
            return;

        // Проверка завершения текущего звука
        if (!isPaused && !soundCheck.audioSource.isPlaying)
        {
            // Если HasCheckMark для текущего звука равно true, показать кнопку продолжения
            if (currentSoundIndex < (soundCheck.soundClips.Length + 1) && soundCheck.HasCheckMark[currentSoundIndex - 1])
            {

                if (soundCheck.HasCheckMark[currentSoundIndex - 1])
                {
                    if (!GameObject.FindGameObjectWithTag("ListButton"))
                    {
                        if(!e.isShoking)
                        {
                            listButton.SetActive(true);
                        }
                    }
                    pauseButton.SetActive(false);

                }
                else
                {
                    PlayNextSound();
                }
            }
            else
            {
                // Если HasCheckMark == false, сразу перейти к следующему звуку
                PlayNextSound();
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
        if (currentSoundIndex < (soundCheck.soundClips.Length + 1))
        {
            soundCheck.PlaySound(currentSoundIndex);

            // Если текущий звук требует отметки, активируем кнопку паузы
            if (soundCheck.HasCheckMark[currentSoundIndex])
            {
                pauseButton.SetActive(true);
            }
            
            currentSoundIndex++;
        }
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    // Метод для включения режима респауна
    public void StartRespawn()
    {
        isRespawning = true;
        dislistButton.SetActive(false);
        shockButton.SetActive(false);
    }

    // Метод для завершения режима респауна
    public void EndRespawn()
    {
        isRespawning = false;
        dislistButton.SetActive(false); // Включаем только если нужно
        shockButton.SetActive(true); // Включаем только если нужно
    }
}
