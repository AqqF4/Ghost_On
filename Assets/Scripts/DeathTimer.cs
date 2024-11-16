using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    public SoundCheckManager soundCheckManager; // Ссылка на SoundCheckManager
    public float timeLimit = 5f; // Лимит времени для проверки отметки
    public float timer; // Таймер
    public Animator animatronicAnim; // Аниматор для аниматроника
    public bool phase2Triggered = false;
    public bool phase3Triggered = false;
    public bool phase4Triggered = false;
    public bool jumpscareTriggered = false;
    public SoundCheck soundCheck;
    public List L;

    void Update()
    {

        if(soundCheckManager.soundCheck.IsCheckmarkPlaced(soundCheckManager.currentSoundIndex - 2))
        {
            AfterCheckMark();
        }

        if(soundCheckManager.soundCheck.IsCheckmarkPlaced(soundCheckManager.currentSoundIndex - 1))
        {
            L.ListDown();
            animatronicAnim.SetTrigger("Jumpscare"); // Запускаем jumpscare
            jumpscareTriggered = true;
            Debug.Log("Activated Jumpscare, because of incorrect checkmark");
        }

        if(soundCheckManager.soundCheck.IsCheckmarkPlaced(soundCheckManager.currentSoundIndex))
        {
            L.ListDown();
            animatronicAnim.SetTrigger("Jumpscare"); // Запускаем jumpscare
            jumpscareTriggered = true;
            Debug.Log("Activated Jumpscare, because of incorrect checkmark");
        }

        if(soundCheckManager.soundCheck.IsCheckmarkPlaced(soundCheckManager.currentSoundIndex + 1))
        {
            L.ListDown();
            animatronicAnim.SetTrigger("Jumpscare"); // Запускаем jumpscare
            jumpscareTriggered = true;
            Debug.Log("Activated Jumpscare, because of incorrect checkmark");
        }

        



        if (soundCheckManager.List.activeSelf && !soundCheckManager.soundCheck.IsCheckmarkPlaced(soundCheckManager.currentSoundIndex - 2)) // Таймер работает только если список активен
        {
            timer += Time.deltaTime; // Увеличиваем таймер

            // Проверка на 30% от лимита времени
            if (!phase2Triggered && timer >= timeLimit * 0.3f)
            {
                animatronicAnim.SetTrigger("s2"); // Запускаем анимацию фазы 2
                phase2Triggered = true;
            }

            // Проверка на 60% от лимита времени
            if (!phase3Triggered && timer >= timeLimit * 0.6f)
            {
                animatronicAnim.SetTrigger("s3"); // Запускаем анимацию фазы 3
                phase3Triggered = true;
            }

            // Проверка на 90% от лимита времени
            if (!phase4Triggered && timer >= timeLimit * 0.9f)
            {
                animatronicAnim.SetTrigger("s4"); // Запускаем анимацию фазы 4
                phase4Triggered = true;
            }

            // Проверка на 100% от лимита времени
            if (!jumpscareTriggered && timer >= timeLimit)
            {
                L.ListDown();
                animatronicAnim.SetTrigger("Jumpscare"); // Запускаем jumpscare
                jumpscareTriggered = true;
                Debug.Log("Activated Jumpscare, because of time limit");
            }
        }
    }

    public void AfterCheckMark()
    {
        
        if(!soundCheckManager.List.activeSelf)
        {

            if(soundCheckManager.currentSoundIndex == 5 && soundCheck.HasCheckMark[soundCheckManager.currentSoundIndex - 1])
            {
                AfterChecks();
                soundCheckManager.StartRespawn();
            }
            soundCheckManager.listButton.SetActive(false);
            soundCheckManager.pauseButton.SetActive(true);
            soundCheckManager.PlayNextSound();
            soundCheck.audioSource.Pause();
            soundCheckManager.isPaused = true;
            soundCheckManager.pauseButton.SetActive(false);
            soundCheckManager.continueButton.SetActive(true);
        }

        
    }

    public void AfterChecks()
    {
        Debug.Log("Все звуки были проиграны.");
        soundCheckManager.e.SuccesAnimation();
    }

}
