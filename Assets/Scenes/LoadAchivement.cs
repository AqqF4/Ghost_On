using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAchivement : MonoBehaviour
{
    Transform SpawnPoint;
    Functions FF;
    PlayerTook PP;
    int HasBeen;


    int Gun;
    public GameObject GunAchivement;

    int Dark;
    public GameObject DarkAchivement;

    int Gravity;
    public GameObject GravAchivement;

    int Back;
    public GameObject BackAchivement;

    int Door;
    public GameObject DoorAchivement;

    int Turtle;
    public GameObject TurtleAchivement;

    int Elektricity;
    public GameObject ElektroAchivement;

    int Pure;
    public GameObject PureAchivement;

    int Labs;
    public GameObject LabsAchivement;


    void Start()
    {
        SpawnPoint = GameObject.FindGameObjectWithTag("AchP").GetComponent<Transform>();
        FF = GetComponent<Functions>();
        PP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTook>();
    }

    
    void Update()
    {
        Gun = PlayerPrefs.GetInt("Gun", Gun);
        Dark = PlayerPrefs.GetInt("Dark", Dark);
        Gravity = PlayerPrefs.GetInt("Gravity", Gravity);
        HasBeen = PlayerPrefs.GetInt("HasBeen", HasBeen);

        //dlya gun
        if(PP.hasGun && Gun != 0)
        {
            Instantiate(GunAchivement, SpawnPoint.position, Quaternion.identity);
            Gun = 1;
            PlayerPrefs.SetInt("Gun", Gun);
            PlayerPrefs.Save();
        }

        //dlya dark
        if(FF.WasInEveryDR && Dark != 0)
        {
            Instantiate(DarkAchivement, SpawnPoint.position, Quaternion.identity);
            Dark = 1;
            PlayerPrefs.SetInt("Dark", Dark);
            PlayerPrefs.Save();
        }

        //dlya gravity
        if(FF.Gravitied && Gravity != 0)
        {
            Instantiate(GravAchivement, SpawnPoint.position, Quaternion.identity);
            Gravity = 1;
            PlayerPrefs.SetInt("Gravity", Gravity);
            PlayerPrefs.Save();
        }

        //dlya not endded yet
        if(HasBeen == 1 && Back != 0)
        {
            Instantiate(BackAchivement, SpawnPoint.position, Quaternion.identity);
            Back = 1;
            PlayerPrefs.SetInt("Back", Back);
            PlayerPrefs.Save();
        }

        //dlya gun
        if(PP.hasGun && Gun != 0)
        {
            Instantiate(GunAchivement, SpawnPoint.position, Quaternion.identity);
            Gun = 1;
            PlayerPrefs.SetInt("Gun", Gun);
            PlayerPrefs.Save();
        }
    }
}
