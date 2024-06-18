using System.Collections;
using UnityEngine;

public class Functions : MonoBehaviour
{
    private GameObject list;
    private GameObject DoorObj;
    private Animator anim; GameObject elevator;
    private Animator animGlobal;
    public float playerSpeed;
    private GameObject Player;
    public float fixedYPosition = -0.5f;
    public GameObject LastRoom;
    public GameObject CurrentRoom;
    public GameObject NextRoom;
    ButtonElevator Button;
    GameObject[] pressedButtons;
    public bool On1stFloor;
    public GameObject EtazhCurrent;
    public GameObject EtazhTop;
    public GameObject Clone;
    public GameObject EtazhBottom;
    GameObject[] pressedDoors;
    Door currentDoor;
    public bool isMoving;

    public void ActivateRA() { if (anim != null) { anim.SetBool("isRunning", true); } }

    public void DisactivateRA() { if (anim != null) { anim.SetBool("isRunning", false); } }

    public void ToList() { if (!isMoving) { StartCoroutine(GoToList()); } }

    public void ToDoor() { CheckForDoor(); if (DoorObj != null) { if (!isMoving) { StartCoroutine(GoToDoor()); } } }

    public void ToElevator() { CheckForElevator(); if(Button != null) { StartCoroutine(GoToElevator()); } }

    void CheckForDoor() { foreach (GameObject pressedDoor in pressedDoors) { currentDoor = pressedDoor.GetComponent<Door>(); if (currentDoor.Pressed) { DoorObj = pressedDoor; break; } } }

    void CheckForElevator() { foreach (GameObject pressedButton in pressedButtons) { Button = pressedButton.GetComponent<ButtonElevator>(); if (!Button.Pressed) { break; } } }

    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null) { anim = Player.GetComponent<Animator>(); }
        list = GameObject.FindGameObjectWithTag("List"); elevator = GameObject.FindGameObjectWithTag("Elevator");
        GameObject globalObject = GameObject.FindGameObjectWithTag("Global");
        pressedDoors = GameObject.FindGameObjectsWithTag("Door");
        pressedButtons = GameObject.FindGameObjectsWithTag("ButtonElev");
        if (globalObject != null) { animGlobal = globalObject.GetComponent<Animator>(); }
    }

    public IEnumerator GoToList() {
        isMoving = true; playerSpeed = 5f;
        if (anim != null) { anim.SetBool("isRunning", true); }
        Vector3 targetPosition = list.transform.position;
        while (Mathf.Abs(Player.transform.position.x - targetPosition.x) > 0.1f) {
            if (Player.transform.position.x < targetPosition.x) { Player.transform.localScale = new Vector3(Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z); }
            else if (Player.transform.position.x > targetPosition.x) { Player.transform.localScale = new Vector3(-Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z); }
            Vector3 newPosition = Vector2.MoveTowards(Player.transform.position, new Vector3(targetPosition.x, Player.transform.position.y, Player.transform.position.z), playerSpeed * Time.deltaTime);
            newPosition.y = fixedYPosition; Player.transform.position = newPosition; yield return null;
        }
        playerSpeed = 0f; if (anim != null) { anim.SetBool("isRunning", false); anim.SetBool("isWatching", true); } isMoving = false;
    }

    public IEnumerator GoToElevator() {
        isMoving = true; playerSpeed = 5f;
        if (anim != null) { anim.SetBool("isRunning", true); }
        Vector3 targetPosition = elevator.transform.position;
        while (Mathf.Abs(Player.transform.position.x - targetPosition.x) > 0.1f) {
            if (Player.transform.position.x < targetPosition.x) { Player.transform.localScale = new Vector3(Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z); }
            else if (Player.transform.position.x > targetPosition.x) { Player.transform.localScale = new Vector3(-Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z); }
            Vector3 newPosition = Vector2.MoveTowards(Player.transform.position, new Vector3(targetPosition.x, Player.transform.position.y, Player.transform.position.z), playerSpeed * Time.deltaTime);
            newPosition.y = fixedYPosition; Player.transform.position = newPosition; yield return null;
        }
        if(On1stFloor)
        {
            Clone.SetActive(true);
            Player.SetActive(false);
            Player = Clone;
        }
        playerSpeed = 0f; isMoving = false; if(!Button.Up){EtazhTop.SetActive(true); EtazhCurrent.SetActive(false);}else{EtazhBottom.SetActive(true); EtazhCurrent.SetActive(false);}
    }

    public IEnumerator GoToDoor() {
        isMoving = true; playerSpeed = 5f; currentDoor.Pressed = false;
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        Vector3 targetPosition = DoorObj.transform.position;
        while (Mathf.Abs(Player.transform.position.x - targetPosition.x) > 0.1f) {
            if (Player.transform.position.x < targetPosition.x) { Player.transform.localScale = new Vector3(Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z); }
            else if (Player.transform.position.x > targetPosition.x) { Player.transform.localScale = new Vector3(-Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z); }
            Vector3 newPosition = Vector2.MoveTowards(Player.transform.position, new Vector3(targetPosition.x, Player.transform.position.y, Player.transform.position.z), playerSpeed * Time.deltaTime);
            newPosition.y = fixedYPosition; Player.transform.position = newPosition; yield return null;
        }
        playerSpeed = 0f; if (anim != null) { anim.SetBool("isRunning", false); }
        if (currentDoor.ComingBack) { LastRoom.SetActive(true); CurrentRoom.SetActive(false); }
        else { NextRoom.SetActive(true); CurrentRoom.SetActive(false); }
        currentDoor = null; DoorObj = null; isMoving = false;
    }
}