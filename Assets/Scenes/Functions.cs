using System.Collections;
using UnityEngine;

public class Functions : MonoBehaviour
{
    private GameObject list;
    private GameObject Rubbish;
    private GameObject Exit;
    private GameObject DoorObj;
    private Animator anim;
    GameObject[] pressedDoors;
    private Animator animGlobal;
    public float playerSpeed;
    private GameObject Player;
    private float fixedYPosition = 0f;
    public GameObject[] Rooms;
    public GameObject LastRoom;
    public GameObject CurrentRoom;
    public GameObject NextRoom;
    public bool isMoving;
    Door currentDoor;

    public void ActivateRA()
    {
        if (anim != null)
        {
            anim.SetBool("isRunning", true);
        }
    }

    public void DisactivateRA()
    {
        if (anim != null)
        {
            anim.SetBool("isRunning", false);
        }
    }

    public void ToList()
    {
        if(!isMoving){StartCoroutine(GoToList());}
    }

    public void ToDoor()
    {
        CheckForDoor();
        if (DoorObj != null)
        {
            if(!isMoving){StartCoroutine(GoToDoor());}
        }
    }

    void CheckForDoor()
    {
        foreach (GameObject pressedDoor in pressedDoors)
        {
            currentDoor = pressedDoor.GetComponent<Door>();
            if (currentDoor.Pressed)
            {
                DoorObj = pressedDoor;
                break;
            }
        }
    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null) { anim = Player.GetComponent<Animator>(); }
        list = GameObject.FindGameObjectWithTag("List");
        GameObject globalObject = GameObject.FindGameObjectWithTag("Global");
        pressedDoors = GameObject.FindGameObjectsWithTag("Door");
        if (globalObject != null) { animGlobal = globalObject.GetComponent<Animator>(); }
    }

    public IEnumerator GoToList()
    {
        isMoving = true;
        playerSpeed = 5f;
        if (anim != null) { anim.SetBool("isRunning", true); }
        Vector3 targetPosition = list.transform.position;
        while (Mathf.Abs(Player.transform.position.x - targetPosition.x) > 0.1f)
        {
            if (Player.transform.position.x < targetPosition.x) { Player.transform.localScale = new Vector3(Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z); }
            else if (Player.transform.position.x > targetPosition.x) { Player.transform.localScale = new Vector3(-Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z); }
            Vector3 newPosition = Vector2.MoveTowards(Player.transform.position, new Vector3(targetPosition.x, Player.transform.position.y, Player.transform.position.z), playerSpeed * Time.deltaTime);
            newPosition.y = fixedYPosition; Player.transform.position = newPosition; yield return null;
        }
        playerSpeed = 0f; if (anim != null) { anim.SetBool("isRunning", false); anim.SetBool("isWatching", true); }isMoving = false;
    }

    public IEnumerator GoToDoor()
    {
        isMoving = true;
        playerSpeed = 5f;
        currentDoor.Pressed = false;
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        Vector3 targetPosition = DoorObj.transform.position;
        while (Mathf.Abs(Player.transform.position.x - targetPosition.x) > 0.1f)
        {
            if (Player.transform.position.x < targetPosition.x) { Player.transform.localScale = new Vector3(Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z); }
            else if (Player.transform.position.x > targetPosition.x) { Player.transform.localScale = new Vector3(-Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z); }
            Vector3 newPosition = Vector2.MoveTowards(Player.transform.position, new Vector3(targetPosition.x, Player.transform.position.y, Player.transform.position.z), playerSpeed * Time.deltaTime);
            newPosition.y = fixedYPosition; Player.transform.position = newPosition; yield return null;
        }
        playerSpeed = 0f; if (anim != null) { anim.SetBool("isRunning", false); }
        if (currentDoor.ComingBack) { LastRoom.SetActive(true); CurrentRoom.SetActive(false); }
        else { NextRoom.SetActive(true); CurrentRoom.SetActive(false); }
        currentDoor.Pressed = false; currentDoor = null; DoorObj = null; isMoving = false;
    }
}