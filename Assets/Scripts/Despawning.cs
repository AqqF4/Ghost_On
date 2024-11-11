using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawning : MonoBehaviour
{
    public GameObject PlayButton, StopButton, ShockerButton,  JumpSound;
    public DeathTimer dt;
    public SoundCheck sc;
    public SoundCheckManager scm;
    public Electricity e;
    public float respawningTime;

    public IEnumerator Respawn()
    {
        // Устанавливаем анимацию на деспаун
        e.SetDespawnAnimation();

        // Отключаем все кнопки во время деспауна
        PlayButton.SetActive(false);
        StopButton.SetActive(false);
        scm.StartRespawn();
        ShockerButton.SetActive(false);

        // Ждём время респауна
        yield return new WaitForSeconds(respawningTime);

        // Устанавливаем анимацию на респаун
        e.SetRespawnAnimation();

        // Запускаем функцию сброса для нового запуска процесса тестирования
        respawningEverything();
    }



    public void respawningEverything() 
    {
        // Сбрасываем аудио на первый клип и индекс звука
        scm.currentSoundIndex = 0;
        sc.audioSource.clip = sc.soundClips[0];
        scm.isPaused = true;

        // Запускаем первый звук (сразу приостанавливаем)
        scm.PlayNextSound();
        sc.audioSource.Pause();
    
        // Обновляем видимость кнопок
        scm.pauseButton.SetActive(false);
        scm.continueButton.SetActive(true);
        scm.EndRespawn();

        // Сбрасываем флаги фаз и таймер
        dt.phase2Triggered = false;
        dt.phase3Triggered = false;
        dt.phase4Triggered = false;
        dt.jumpscareTriggered = false;
        dt.timer = 0f;

        // Устанавливаем анимацию на исходное состояние
        dt.animatronicAnim.SetTrigger("Reset");
    
        // Отключаем звук скримера
        JumpSound.SetActive(false);
    
        // Выводим сообщение о начале тестирования заново
        Debug.Log("Процесс тестирования начат заново после скримера.");
    }
}
