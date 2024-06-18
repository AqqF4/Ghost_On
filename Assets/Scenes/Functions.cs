using System.Collections;
using UnityEngine;

public class Functions : MonoBehaviour
{
    private GameObject list;
    private GameObject Rubbish;
    private GameObject Exit;
    private GameObject DoorObj;
    private GameObject LadderObj;
    private Ladder currentLadder;
    private Animator anim;
    private Animator Room2Anim;
    private GameObject[] pressedDoors;
    private Animator animGlobal;
    public float playerSpeed;
    private GameObject Player;
    private float fixedYPosition = 0f;
    public GameObject[] Rooms;
    public GameObject LastRoom;
    public GameObject CurrentRoom;
    public GameObject NextRoom;
    private GameObject UnderLadder;
    public int Etazh = 1;
    public bool isMoving;
    private bool isDown;
    private GameObject[] Ladders;
    private Door currentDoor;

    public void ActivateRA()
    {
        if (anim != null) { anim.SetBool("isRunning", true); }
    }

    public void DisactivateRA()
    {
        if (anim != null) { anim.SetBool("isRunning", false); }
    }

    public void ToList()
    {
        if (!isMoving) { StartCoroutine(GoToList()); }
    }

    public void Ladder()
    {
        CheckForLadder();
        if (LadderObj != null)
        {
            if (!isMoving)
            {
                if (currentLadder.isHigh) { StartCoroutine(UpToLadder()); }
                else { StartCoroutine(DownToLadder()); }
            }
        }
    }

    public void ToDoor()
    {
        CheckForDoor();
        if (DoorObj != null)
        {
            if (!isMoving) { StartCoroutine(GoToDoor()); }
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

    void CheckForLadder()
    {
        foreach (GameObject pressedLadder in Ladders)
        {
            currentLadder = pressedLadder.GetComponent<Ladder>();
            if (currentLadder.Pressed)
            {
                LadderObj = pressedLadder;
                break;
            }
        }
    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        UnderLadder = GameObject.FindGameObjectWithTag("LadderR");
        if (Player != null) { anim = Player.GetComponent<Animator>(); }
        list = GameObject.FindGameObjectWithTag("List");
        Room2Anim = GameObject.FindGameObjectWithTag("Room2").GetComponent<Animator>();
        GameObject globalObject = GameObject.FindGameObjectWithTag("Global");
        pressedDoors = GameObject.FindGameObjectsWithTag("Door");
        Ladders = GameObject.FindGameObjectsWithTag("LadderR");
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
            newPosition.y = fixedYPosition;
            Player.transform.position = newPosition;
            yield return null;
        }
        playerSpeed = 0f;
        if (anim != null) { anim.SetBool("isRunning", false); anim.SetBool("isWatching", true); }
        isMoving = false;
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
            newPosition.y = fixedYPosition;
            Player.transform.position = newPosition;
            yield return null;
        }
        playerSpeed = 0f;
        if (anim != null) { anim.SetBool("isRunning", false); }
        if (currentDoor.ComingBack) { LastRoom.SetActive(true); CurrentRoom.SetActive(false); }
        else { NextRoom.SetActive(true); CurrentRoom.SetActive(false); }
        currentDoor.Pressed = false;
        currentDoor = null;
        DoorObj = null;
        isMoving = false;
    }

    public IEnumerator UpToLadder()
    {
        isMoving = true;
        playerSpeed = 5f;
        currentLadder.Pressed = false;
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        Vector3 targetPosition = UnderLadder.transform.position;
        while (Mathf.Abs(Player.transform.position.x - targetPosition.x) > 0.1f)
        {
            if (Player.transform.position.x < targetPosition.x) { Player.transform.localScale = new Vector3(Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z); }
            else if (Player.transform.position.x > targetPosition.x) { Player.transform.localScale = new Vector3(-Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z); }
            Vector3 newPosition = Vector2.MoveTowards(Player.transform.position, new Vector3(targetPosition.x, Player.transform.position.y, Player.transform.position.z), playerSpeed * Time.deltaTime);
            newPosition.y = fixedYPosition;
            Player.transform.position = newPosition;
            yield return null;
        }
        playerSpeed = 0f;
        if (anim != null) { anim.SetBool("isRunning", false); }
        Etazh += 1;
        Room2Anim.SetTrigger("Higher");
        isMoving = false;
    }

    public IEnumerator DownToLadder()
    {
        isMoving = true;
        playerSpeed = 5f;
        currentLadder.Pressed = false;
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        Vector3 targetPosition = UnderLadder.transform.position;
        while (Mathf.Abs(Player.transform.position.x - targetPosition.x) > 0.1f)
        {
            if (Player.transform.position.x < targetPosition.x) { Player.transform.localScale = new Vector3(Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z); }
            else if (Player.transform.position.x > targetPosition.x) { Player.transform.localScale = new Vector3(-Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z); }
            Vector3 newPosition = Vector2.MoveTowards(Player.transform.position, new Vector3(targetPosition.x, Player.transform.position.y, Player.transform.position.z), playerSpeed * Time.deltaTime);
            newPosition.y = fixedYPosition;
            Player.transform.position = newPosition;
            yield return null;
        }
        playerSpeed = 0f;
        if (anim != null) { anim.SetBool("isRunning", false); }
        Etazh -= 1;
        Room2Anim.SetTrigger("Lower");
        isMoving = false;
    }

    public void NotMove()
    {
        isMoving = false;
    }
}