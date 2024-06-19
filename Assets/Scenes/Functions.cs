using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Functions : MonoBehaviour
{
    private GameObject list;
    private GameObject DoorObj;
    private GameObject ElevatorObj; // Объект лифта
    private Animator anim;
    private Animator animGlobal;
    public float playerSpeed;
    private GameObject Player;
    public float fixedYPosition = -0.5f;
    public GameObject LastRoom;
    public GameObject CurrentRoom;
    public GameObject NextRoom;
    GameObject[] pressedDoors;
    GameObject[] pressedElevators; // Массив объектов лифтов
    Door currentDoor;
    ButtonElevator currentElevator; // Текущий лифт
    public GameObject TopEtazh;
    public GameObject CurrentEtazh;
    public GameObject BottomEtazh;
    public bool isMoving;
    bool canDelete = false;
    GameObject trashObj;
    GameObject tubeObj;
    SpriteRenderer Menu;

    public void ActivateRA() { if (anim != null) { anim.SetBool("isRunning", true); } }

    public void DisactivateRA() { if (anim != null) { anim.SetBool("isRunning", false); } }

    public void ToList()
    {
        Menu = GameObject.FindGameObjectWithTag("ListM").GetComponent<SpriteRenderer>();
        if (!isMoving) { StartCoroutine(GoToList()); }
    }

    public void ToDoor()
    {
        CheckForDoor();
        if (DoorObj != null)
        {
            if (!isMoving) { StartCoroutine(GoToDoor()); }
        }
    }

    public void ToTrash()
    {
        Menu = GameObject.FindGameObjectWithTag("TrashM").GetComponent<SpriteRenderer>();
        StartCoroutine(GoToTrash());
    }

    public void ToTube()
    {
        Menu = GameObject.FindGameObjectWithTag("TubeM").GetComponent<SpriteRenderer>();
        StartCoroutine(GoToTube());
    }

    public void ToElevator()
    {
        CheckForElevator();
        if (ElevatorObj != null)
        {
            if (!isMoving) { StartCoroutine(GoToElevator()); }
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

    void CheckForElevator()
    {
        foreach (GameObject pressedElevator in pressedElevators)
        {
            currentElevator = pressedElevator.GetComponent<ButtonElevator>();
            if (currentElevator.Pressed)
            {
                ElevatorObj = ElevatorObj;
                break;
            }
        }
    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null) { anim = Player.GetComponent<Animator>(); }
        list = GameObject.FindGameObjectWithTag("List"); 
        ElevatorObj = GameObject.FindGameObjectWithTag("Elevator");
        trashObj = GameObject.FindGameObjectWithTag("Trash"); tubeObj = GameObject.FindGameObjectWithTag("Tube");
        GameObject globalObject = GameObject.FindGameObjectWithTag("Global");
        pressedDoors = GameObject.FindGameObjectsWithTag("Door");
        pressedElevators = GameObject.FindGameObjectsWithTag("ButtonElev"); // Находим все объекты с тегом "ButtonElev"
        if (globalObject != null) { animGlobal = globalObject.GetComponent<Animator>(); }
    }

    public IEnumerator GoToList()
    {
        isMoving = true;
        playerSpeed = 5f;
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        DisActMenu();
        Vector3 targetPosition = list.transform.position;
        while (Mathf.Abs(Player.transform.position.x - targetPosition.x) > 0.1f)
        {
            if (Player.transform.position.x < targetPosition.x)
            {
                Player.transform.localScale = new Vector3(Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z);
            }
            else if (Player.transform.position.x > targetPosition.x)
            {
                Player.transform.localScale = new Vector3(-Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z);
            }
            Vector3 newPosition = Vector2.MoveTowards(Player.transform.position, new Vector3(targetPosition.x, Player.transform.position.y, Player.transform.position.z), playerSpeed * Time.deltaTime);
            newPosition.y = fixedYPosition;
            Player.transform.position = newPosition;
            yield return null;
        }
        playerSpeed = 0f;
        if (anim != null) { anim.SetBool("isRunning", false); anim.SetBool("isWatching", true); }
        isMoving = false;
        ActivateMenu(Menu);
        canDelete = true;
    }

    public IEnumerator GoToTrash()
    {
        isMoving = true;
        playerSpeed = 5f;
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        DisActMenu();
        Vector3 targetPosition = trashObj.transform.position;
        while (Mathf.Abs(Player.transform.position.x - targetPosition.x) > 0.1f)
        {
            if (Player.transform.position.x < targetPosition.x)
            {
                Player.transform.localScale = new Vector3(Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z);
            }
            else if (Player.transform.position.x > targetPosition.x)
            {
                Player.transform.localScale = new Vector3(-Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z);
            }
            Vector3 newPosition = Vector2.MoveTowards(Player.transform.position, new Vector3(targetPosition.x, Player.transform.position.y, Player.transform.position.z), playerSpeed * Time.deltaTime);
            newPosition.y = fixedYPosition;
            Player.transform.position = newPosition;
            yield return null;
        }
        playerSpeed = 0f;
        if (anim != null) { anim.SetBool("isRunning", false); anim.SetBool("isWatching", true); }
        isMoving = false;
        ActivateMenu(Menu);
        canDelete = true;
    }

    public IEnumerator GoToTube()
    {
        isMoving = true;
        playerSpeed = 5f;
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        DisActMenu();
        Vector3 targetPosition = tubeObj.transform.position;
        while (Mathf.Abs(Player.transform.position.x - targetPosition.x) > 0.1f)
        {
            if (Player.transform.position.x < targetPosition.x)
            {
                Player.transform.localScale = new Vector3(Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z);
            }
            else if (Player.transform.position.x > targetPosition.x)
            {
                Player.transform.localScale = new Vector3(-Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z);
            }
            Vector3 newPosition = Vector2.MoveTowards(Player.transform.position, new Vector3(targetPosition.x, Player.transform.position.y, Player.transform.position.z), playerSpeed * Time.deltaTime);
            newPosition.y = fixedYPosition;
            Player.transform.position = newPosition;
            yield return null;
        }
        playerSpeed = 0f;
        if (anim != null) { anim.SetBool("isRunning", false); anim.SetBool("isWatching", true); }
        isMoving = false;
        ActivateMenu(Menu);
        canDelete = true;
    }

    void DisActMenu()
    {
        if (Menu != null)
        {
            Menu.color = new Color(0, 0, 0, 0);
            SetChildrenActive(Menu.gameObject, false);
        }
    }

    void ActivateMenu(SpriteRenderer menu)
    {
        if (menu != null)
        {
            menu.color = new Color(1, 1, 1, 1);
            SetChildrenActive(menu.gameObject, true);
        }
    }

    void SetChildrenActive(GameObject parent, bool isActive)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(isActive);
        }
    }

    public IEnumerator GoToDoor()
    {
        isMoving = true;
        playerSpeed = 5f;
        DisActMenu();
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        Vector3 targetPosition = DoorObj.transform.position;
        while (Mathf.Abs(Player.transform.position.x - targetPosition.x) > 0.1f)
        {
            if (Player.transform.position.x < targetPosition.x)
            {
                Player.transform.localScale = new Vector3(Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z);
            }
            else if (Player.transform.position.x > targetPosition.x)
            {
                Player.transform.localScale = new Vector3(-Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z);
            }
            Vector3 newPosition = Vector2.MoveTowards(Player.transform.position, new Vector3(targetPosition.x, Player.transform.position.y, Player.transform.position.z), playerSpeed * Time.deltaTime);
            newPosition.y = fixedYPosition;
            Player.transform.position = newPosition;
            yield return null;
        }
        playerSpeed = 0f;
        if (anim != null) { anim.SetBool("isRunning", false); }
        if (currentDoor.ComingBack)
        {
            LastRoom.SetActive(true);
            CurrentRoom.SetActive(false);
        }
        else
        {
            NextRoom.SetActive(true);
            CurrentRoom.SetActive(false);
        }
        currentDoor.Unnpress();
        currentDoor = null;
        DoorObj = null;
        isMoving = false;
    }

    public IEnumerator GoToElevator()
    {
        isMoving = true;
        playerSpeed = 5f;
        currentElevator.Pressed = false;
        DisActMenu(); 
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        Vector3 targetPosition = ElevatorObj.transform.position;
        while (Mathf.Abs(Player.transform.position.x - targetPosition.x) > 0.1f)
        {
            if (Player.transform.position.x < targetPosition.x)
            {
                Player.transform.localScale = new Vector3(Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z);
            }
            else if (Player.transform.position.x > targetPosition.x)
            {
                Player.transform.localScale = new Vector3(-Mathf.Abs(Player.transform.localScale.x), Player.transform.localScale.y, Player.transform.localScale.z);
            }
            Vector3 newPosition = Vector2.MoveTowards(Player.transform.position, new Vector3(targetPosition.x, Player.transform.position.y, Player.transform.position.z), playerSpeed * Time.deltaTime);
            newPosition.y = fixedYPosition;
            Player.transform.position = newPosition;
            yield return null;
        }
        playerSpeed = 0f;
        if (anim != null) { anim.SetBool("isRunning", false); }
        if (currentElevator.Up)
        {
            TopEtazh.SetActive(true);
            CurrentEtazh.SetActive(false);
        }
        else
        {
            BottomEtazh.SetActive(true);
            CurrentEtazh.SetActive(false);
        }
        currentElevator = null;
        isMoving = false;
    }

    void Update()
    {
        if (canDelete)
        {
            if (Input.anyKey)
            {
                DisActMenu();
            }
        }
    }
}