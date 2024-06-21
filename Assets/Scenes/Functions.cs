using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Functions : MonoBehaviour
{
    private GameObject list;
    public GameObject PlayerLadder; GameObject LadderPlus;
    private GameObject DoorObj;
    private GameObject ElevatorObj; // Объект лифта
    private Animator anim;
    private Animator animGlobal;
    private Animator animGlobalUp;
    private Animator animGlobalDown;
    public bool CanGround;
    public GameObject FlashlightPref;
    public Transform Flashpoint;
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
    GameObject GravObj;
    GameObject tableObj;
    GameObject exitObj;
    GameObject gunObj;
    GameObject ladderObj;
    GameObject tubeObj;
    bool WantDestroy; Transform LadderPoint;
    PlayerTook PP;
    GameObject PlusObject;
    SpriteRenderer Menu;

    public void ActivateRA() { if (anim != null) { anim.SetBool("isRunning", true); } }

    public void DisactivateRA() { if (anim != null) { anim.SetBool("isRunning", false); } }

    public void ToList()
    {
        Menu = GameObject.FindGameObjectWithTag("ListM").GetComponent<SpriteRenderer>();
        if (!isMoving) { StartCoroutine(GoToList()); }
    }

    public void ToExit()
    {
        Menu = GameObject.FindGameObjectWithTag("ExitM").GetComponent<SpriteRenderer>();
        if (!isMoving) { StartCoroutine(GoToExit()); }
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
        if (!isMoving) { StartCoroutine(GoToTrash());}
    }

    public void ToNite()
    {
        Menu = null; GravObj = GameObject.FindGameObjectWithTag("GravityCh"); 
        if (!isMoving) { StartCoroutine(GoToGravity());}
    }

    public void ToTube()
    {
        Menu = GameObject.FindGameObjectWithTag("TubeM").GetComponent<SpriteRenderer>();
        if (!isMoving) { StartCoroutine(GoToTube());}
    }

    public void ToTable()
    {
        PlusObject = GameObject.FindGameObjectWithTag("Marker");
        WantDestroy = true;
        Menu = GameObject.FindGameObjectWithTag("TableM").GetComponent<SpriteRenderer>();
        if (!isMoving) { StartCoroutine(GoToTable());}
    }

    public void ToLadder()
    {
        WantDestroy = true;
        Menu = GameObject.FindGameObjectWithTag("LadderM").GetComponent<SpriteRenderer>();
        if (!isMoving) { StartCoroutine(GoToLadder());}
    }

    public void ToGun()
    {
        Menu = GameObject.FindGameObjectWithTag("GunM").GetComponent<SpriteRenderer>();
        if (!isMoving) { StartCoroutine(GoToGun());}
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
        Player = GameObject.FindGameObjectWithTag("Player"); LadderPoint = GameObject.FindGameObjectWithTag("PointL").GetComponent<Transform>();  
        if (Player != null) { anim = Player.GetComponent<Animator>(); } gunObj = GameObject.FindGameObjectWithTag("Gun");
        list = GameObject.FindGameObjectWithTag("List"); exitObj = GameObject.FindGameObjectWithTag("Exit");  GravObj = GameObject.FindGameObjectWithTag("GravityCh"); 
        ElevatorObj = GameObject.FindGameObjectWithTag("Elevator"); ladderObj = GameObject.FindGameObjectWithTag("Ladder");
        trashObj = GameObject.FindGameObjectWithTag("Trash"); tubeObj = GameObject.FindGameObjectWithTag("Tube"); tableObj = GameObject.FindGameObjectWithTag("Table");
        PP = Player.GetComponent<PlayerTook>(); GameObject UnGrounded = GameObject.FindGameObjectWithTag("UnGrounded");
        if(CurrentRoom.CompareTag("CanChange")){Menu = GameObject.FindGameObjectWithTag("Grounded").GetComponent<SpriteRenderer>(); UnGrounded.SetActive(false); ActivateMenu(Menu);}

        pressedDoors = GameObject.FindGameObjectsWithTag("Door");
        pressedElevators = GameObject.FindGameObjectsWithTag("ButtonElev"); // Находим все объекты с тегом "ButtonElev"
        if (CurrentEtazh != null) { animGlobal = CurrentEtazh.GetComponent<Animator>(); }
        if (TopEtazh != null) { animGlobalUp = TopEtazh.GetComponent<Animator>(); }
        if (BottomEtazh != null) { animGlobalDown = BottomEtazh.GetComponent<Animator>(); }
    }

    public IEnumerator GoToList()
    {
        isMoving = true; canDelete = false;
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

    public IEnumerator GoToGravity()
    {
        isMoving = true; canDelete = false;
        playerSpeed = 5f;
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        DisActMenu();
        Vector3 targetPosition = GravObj.transform.position;
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
        if(PP.hasFlashlight){Instantiate(FlashlightPref, Flashpoint.position, Quaternion.identity); }
        PP.turnedLight = true;
        isMoving = false;
        CanGround = true;
    }

    public IEnumerator GoToGun()
    {
        isMoving = true; canDelete = false;
        playerSpeed = 5f;
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        DisActMenu();
        Vector3 targetPosition = gunObj.transform.position;
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
        if (anim != null) {if(!PP.hasLadder){ anim.SetBool("isRunning", false); anim.SetBool("isWatching", true); }else{ anim.SetBool("isRunning", false); anim.SetBool("isWatching", false); anim.SetBool("isClimbing", true);}}
        isMoving = false;
        if(PP.hasLadder){LadderPlus = Instantiate(PlayerLadder, LadderPoint.position, Quaternion.identity); anim.SetBool("isClumbing", true); PP.hasGun = true; WantDestroy = true;}
        ActivateMenu(Menu);
        
        canDelete = true;
    }

    public IEnumerator GoToTable()
    {
        isMoving = true; canDelete = false;
        playerSpeed = 5f;
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        DisActMenu();
        Vector3 targetPosition = tableObj.transform.position;
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
        PP.hasMarker = true;
        canDelete = true;
    }

    public IEnumerator GoToLadder()
    {
        isMoving = true; canDelete = false;
        playerSpeed = 5f;
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        DisActMenu();
        Vector3 targetPosition = ladderObj.transform.position;
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
        isMoving = false; PlusObject = GameObject.FindGameObjectWithTag("Ladder");
        WantDestroy = true; canDelete = true; PP.hasLadder = true;
        ActivateMenu(Menu);

        
        canDelete = true; PP.hasLadder = true;
    }

    public IEnumerator GoToExit()
    {
        isMoving = true; canDelete = false;
        playerSpeed = 5f;
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        DisActMenu();
        Vector3 targetPosition = exitObj.transform.position;
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
        if(PP.hasPassword){Menu = GameObject.FindGameObjectWithTag("ExitPassM").GetComponent<SpriteRenderer>();}
        ActivateMenu(Menu);
        canDelete = true;
    }

    public IEnumerator GoToTrash()
    {
        isMoving = true; canDelete = false;
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
        if(PP.hasBucket){Menu = GameObject.FindGameObjectWithTag("EndingFall").GetComponent<SpriteRenderer>();}
        ActivateMenu(Menu);
        canDelete = true;
    }

    public IEnumerator GoToTube()
    {
        isMoving = true; canDelete = false;
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
        if (anim != null) {if(!PP.hasLadder){ anim.SetBool("isRunning", false); anim.SetBool("isWatching", true); }else{ anim.SetBool("isRunning", false); anim.SetBool("isWatching", false); anim.SetBool("isClimbing", true);}}
        isMoving = false;
        if(PP.hasLadder){anim.SetBool("isClimbing", true); Menu = GameObject.FindGameObjectWithTag("TubeNothingM").GetComponent<SpriteRenderer>(); WantDestroy = true;}
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
        isMoving = true; canDelete = false;
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
        isMoving = true; canDelete = false;
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
            if(animGlobal != null)
            {
                animGlobal.SetTrigger("Start");
                yield return new WaitForSeconds(1);
                GoTop();
            }
        }
        else
        {
            if(animGlobal != null)
            {
                animGlobal.SetTrigger("Start");
                yield return new WaitForSeconds(1);
                GoBottom();
            }
        }
        currentElevator = null;
        isMoving = false;
    }

    void Update()
    {   
        if(anim == null)
        {
            anim = Player.GetComponent<Animator>();
        }

        if(PP == null)
        {
            PP = Player.GetComponent<PlayerTook>();
        }

        if(DoorObj == null)
        {
            pressedDoors = GameObject.FindGameObjectsWithTag("Door");

        }

        if(GravObj == null)
        {
            GravObj = GameObject.FindGameObjectWithTag("GravityCh"); 
        }

        if(ladderObj == null)
        {
            ladderObj = GameObject.FindGameObjectWithTag("Ladder");
        }

        if (canDelete)
        {
            if (Input.anyKey)
            {
                if(!WantDestroy){DisActMenu();}
                else{Destroy(Menu.gameObject); Menu = null; WantDestroy = false; if(PlusObject != null){Destroy(PlusObject);}}
                canDelete = false; anim.SetBool("isClimbing", false); Destroy(LadderPlus);
            }
        }
    }

    public void GoTop()
    {
        TopEtazh.SetActive(true);
        animGlobalUp.SetTrigger("End");
        CurrentEtazh.SetActive(false);
    }

    public void GoBottom()
    {
        BottomEtazh.SetActive(true);
        animGlobalDown.SetTrigger("End");
        CurrentEtazh.SetActive(false);
    }
}