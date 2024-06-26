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
    public bool hasCola;


    public int Ending;
    // 1 - Bear
    // 2 - Vent
    // 3 - Chair
    // 4 - Whatever, but painted
    // 5 - Heart
    // 6 - Fall

    public void SaveEnding()
    {
        PlayerPrefs.SetInt("Ending", Ending);
        PlayerPrefs.Save();
    }
}
