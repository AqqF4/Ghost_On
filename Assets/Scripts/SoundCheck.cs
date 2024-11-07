using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCheck : MonoBehaviour
{
    public AudioClip[] soundClips; // Массив звуковых клипов для воспроизведения
    public GameObject[] checkmarks; // Массив объектов галочек для каждого звука
    public bool firstSoundOnStart = true; // Определяет, проигрывать ли первый звук при старте
    public bool[] HasCheckMark; // Булевые значения для каждого звука, true по умолчанию
    public AudioSource audioSource; // Ссылка на компонент AudioSource
    public SoundCheckManager scm;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }


        if (firstSoundOnStart)
        {
            scm.PlayNextSound();
        }
    }

    public void PlaySound(int index)
    {
        if (index < soundClips.Length)
        {
            audioSource.clip = soundClips[index];
            audioSource.Play();
        }
    }

    public void PauseSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            scm.isPaused = true;
        }
    }

    public void ResumeSound()
    {
        if (!audioSource.isPlaying && audioSource.clip != null)
        {
            audioSource.UnPause();
            scm.isPaused = false;
        }
        else if(scm.isPaused)
        {
            scm.PlayNextSound();
            scm.isPaused = false;
        }
    }

    public bool IsCheckmarkPlaced(int index)
    {
        if (index < checkmarks.Length)
        {
            return checkmarks[index].activeSelf;
        }
        return false;
    }
}
