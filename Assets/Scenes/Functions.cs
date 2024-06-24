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
    public bool CanGround; public GameObject LightedRoom; public GameObject UnlightedRoom;
    public GameObject FlashlightPref;
    public Transform Flashpoint;
    public float playerSpeed; public bool isPainted;
    private GameObject Player; bool Pulled;
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
    GameObject GravObj; public GameObject CanTube;
    public GameObject CantTube;
    GameObject tableObj; GameObject flashObj;
    GameObject exitObj;
    GameObject gunObj; GameObject keyObj;
    GameObject luckObj;
    GameObject paintObj;GameObject chairObj;
    GameObject ladderObj; GameObject bucketObj;
    GameObject tubeObj;
    public GameObject Painted;
    bool WantDestroy; Transform LadderPoint;
    PlayerTook PP;
    public GameObject PaintedR;
    public GameObject UnPaintedR;
    GameObject PlusObject;
    SpriteRenderer Menu;

    public void ActivateRA() { if (anim != null) { anim.SetBool("isRunning", true); } }

    public void DisactivateRA() { if (anim != null) { anim.SetBool("isRunning", false); } }

    public void ToList()
    {
        Menu = GameObject.FindGameObjectWithTag("ListM").GetComponent<SpriteRenderer>();
        if (!isMoving) { StartCoroutine(GoToList()); }
    }

    public void ToChair()
    {
        Menu = GameObject.FindGameObjectWithTag("ElektroEnd").GetComponent<SpriteRenderer>();
        if (!isMoving) { StartCoroutine(GoToChair()); }
    }

    public void ToBucket()
    {
        Menu = GameObject.FindGameObjectWithTag("BucketM").GetComponent<SpriteRenderer>();
        if (!isMoving) { StartCoroutine(GoToBucket()); }
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

    public void ToHatch()
    {
        Menu = GameObject.FindGameObjectWithTag("LuckM").GetComponent<SpriteRenderer>();
        if (!isMoving) { StartCoroutine(GoToLuck());}
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

    public void ToKey()
    {
        Menu = GameObject.FindGameObjectWithTag("KeyM").GetComponent<SpriteRenderer>();
        if (!isMoving) { StartCoroutine(GoToKey());}
    }

    public void ToFlashlight()
    {
        Menu = GameObject.FindGameObjectWithTag("FlashlightM").GetComponent<SpriteRenderer>();
        if (!isMoving) { StartCoroutine(GoToFlashlight());}
    }

    public void ToTable()
    {
        PlusObject = GameObject.FindGameObjectWithTag("Marker");
        WantDestroy = true;
        Menu = GameObject.FindGameObjectWithTag("TableM").GetComponent<SpriteRenderer>();
        if (!isMoving) { StartCoroutine(GoToTable());}
    }

    public void ToPainting()
    {
        PlusObject = GameObject.FindGameObjectWithTag("Paint");
        WantDestroy = true;
        Menu = GameObject.FindGameObjectWithTag("PaintM").GetComponent<SpriteRenderer>();
        if (!isMoving) { StartCoroutine(GoToPaint());}
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
            if (!isMoving && currentElevator != null) { StartCoroutine(GoToElevator()); }
        }
        else
        {
            CheckForElevator();
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
                ToElevator();
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
            else
            {
                currentElevator = null;
            }
        }
    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player"); LadderPoint = GameObject.FindGameObjectWithTag("PointL").GetComponent<Transform>();  
        if (Player != null) { anim = Player.GetComponent<Animator>(); } gunObj = GameObject.FindGameObjectWithTag("Gun"); paintObj = GameObject.FindGameObjectWithTag("Paint");
        list = GameObject.FindGameObjectWithTag("List"); exitObj = GameObject.FindGameObjectWithTag("Exit");  GravObj = GameObject.FindGameObjectWithTag("GravityCh");  chairObj = GameObject.FindGameObjectWithTag("Chair"); 
        ElevatorObj = GameObject.FindGameObjectWithTag("Elevator"); ladderObj = GameObject.FindGameObjectWithTag("Ladder"); keyObj = GameObject.FindGameObjectWithTag("Key"); 
        trashObj = GameObject.FindGameObjectWithTag("Trash"); tubeObj = GameObject.FindGameObjectWithTag("Tube"); tableObj = GameObject.FindGameObjectWithTag("Table");
        PP = Player.GetComponent<PlayerTook>(); GameObject UnGrounded = GameObject.FindGameObjectWithTag("UnGrounded"); luckObj = GameObject.FindGameObjectWithTag("Luck");
        if(CurrentRoom.CompareTag("CanChange")){Menu = GameObject.FindGameObjectWithTag("Grounded").GetComponent<SpriteRenderer>(); UnGrounded.SetActive(false); ActivateMenu(Menu);}


        pressedDoors = GameObject.FindGameObjectsWithTag("Door");
        pressedElevators = GameObject.FindGameObjectsWithTag("ButtonElev");
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

    public IEnumerator GoToChair()
    {
        isMoving = true; canDelete = false;
        playerSpeed = 5f;
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        DisActMenu();
        Vector3 targetPosition = chairObj.transform.position;
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
        PP.Ending = 3;
    }

    public IEnumerator GoToBucket()
    {
        isMoving = true; canDelete = false;
        playerSpeed = 5f;
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        DisActMenu();
        Vector3 targetPosition = bucketObj.transform.position;
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
        playerSpeed = 0f; WantDestroy = true; PlusObject = bucketObj; PP.hasBucket = true;
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
        playerSpeed = 0f; Menu = GameObject.FindGameObjectWithTag("NiteM").GetComponent<SpriteRenderer>();
        if (anim != null) { anim.SetBool("isRunning", false); anim.SetBool("isWatching", true); }

        if(!Pulled && !PP.hasFlashlight){Menu = GameObject.FindGameObjectWithTag("NiteM").GetComponent<SpriteRenderer>(); Pulled = true; ActivateMenu(Menu);}else
        if(!Pulled && PP.hasFlashlight){Menu = GameObject.FindGameObjectWithTag("NiteM").GetComponent<SpriteRenderer>(); Pulled = true; ActivateMenu(Menu);}else
        if(Pulled && PP.hasFlashlight){PP.turnedLight = true; Instantiate(FlashlightPref, Flashpoint.position, Quaternion.identity); PP.hasFlashlight = false; Menu = GameObject.FindGameObjectWithTag("NitePoint").GetComponent<SpriteRenderer>(); ActivateMenu(Menu); Menu = GameObject.FindGameObjectWithTag("NiteMFlashlight").GetComponent<SpriteRenderer>(); WantDestroy = true; ActivateMenu(Menu); LightedRoom.SetActive(true); UnlightedRoom.SetActive(false);}
        
        
        isMoving = false;
        canDelete = true;
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
        if (anim != null) {if(!PP.hasLadder){ anim.SetBool("isRunning", false); anim.SetBool("isWatching", true); }else{ anim.SetBool("isRunning", false); anim.SetBool("isWatching", false); anim.SetBool("isClimbing", true); Menu = GameObject.FindGameObjectWithTag("GunMTook").GetComponent<SpriteRenderer>(); WantDestroy = true; PlusObject = gunObj; }}
        isMoving = false; LadderPlus = null;
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

    public IEnumerator GoToKey()
    {
        isMoving = true; canDelete = false;
        playerSpeed = 5f;
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        DisActMenu();
        Vector3 targetPosition = keyObj.transform.position;
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
        isMoving = false; PlusObject = GameObject.FindGameObjectWithTag("Key"); WantDestroy = true;
        ActivateMenu(Menu);
        PP.hasKey = true;
        canDelete = true;
    }

    public IEnumerator GoToFlashlight()
    {
        isMoving = true; canDelete = false;
        playerSpeed = 5f;
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        DisActMenu();
        Vector3 targetPosition = flashObj.transform.position;
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
        isMoving = false; PlusObject = GameObject.FindGameObjectWithTag("Flashlight"); WantDestroy = true;
        ActivateMenu(Menu);
        PP.hasFlashlight = true;
        canDelete = true;
    }

    public IEnumerator GoToPaint()
    {
        isMoving = true; canDelete = false;
        playerSpeed = 5f;
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        DisActMenu();
        Vector3 targetPosition = paintObj.transform.position;
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
        if(PP.hasMarker){Menu = Painted.GetComponent<SpriteRenderer>(); PaintedR.SetActive(true); UnPaintedR.SetActive(false); PP.hasMarker = false;}
        ActivateMenu(Menu);
        if(!PP.hasMarker){PP.hasMarker = true; WantDestroy = false;}else{WantDestroy = true; canDelete = true;}
        isPainted = true;
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
        isMoving = false; ActivateMenu(Menu);
        canDelete = true;
        if(PP.hasPassword){Menu = GameObject.FindGameObjectWithTag("BearEnd").GetComponent<SpriteRenderer>();}
        ActivateMenu(Menu); PP.Ending = 1;
        
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
        if (anim != null) { anim.SetBool("isRunning", false); anim.SetBool("isWatching", true);}
        isMoving = false;
        if(PP.hasBucket){Menu = GameObject.FindGameObjectWithTag("FallEnd").GetComponent<SpriteRenderer>();PP.Ending = 6;}
        ActivateMenu(Menu);
        
        if(!PP.hasBucket){canDelete = true;}
    }


    public IEnumerator GoToLuck()
    {
        isMoving = true; canDelete = false;
        playerSpeed = 5f;
        if (anim != null) { anim.SetBool("isWatching", false); anim.SetBool("isRunning", true); }
        DisActMenu();
        Vector3 targetPosition = luckObj.transform.position;
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
        if(PP.hasKey){Menu = GameObject.FindGameObjectWithTag("VentEnd").GetComponent<SpriteRenderer>(); PP.Ending = 2;}
        ActivateMenu(Menu);
        if(!PP.hasKey){canDelete = true;} 
        
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
        playerSpeed = 0f;  LadderPlus = null;
        if (anim != null) {if(!PP.hasLadder){ anim.SetBool("isRunning", false); anim.SetBool("isWatching", true); }else{ anim.SetBool("isRunning", false); anim.SetBool("isWatching", false); anim.SetBool("isClimbing", true);}}
        isMoving = false;
        if(PP.hasLadder){LadderPlus = Instantiate(PlayerLadder, LadderPoint.position, Quaternion.identity); anim.SetBool("isClimbing", true); Menu = GameObject.FindGameObjectWithTag("TubeNothingM").GetComponent<SpriteRenderer>(); WantDestroy = true; CanTube.SetActive(false); CantTube.SetActive(true);}
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
                currentElevator = null;
            }
        }
        else
        {
            if(animGlobal != null)
            {
                animGlobal.SetTrigger("Start");
                yield return new WaitForSeconds(1);
                GoBottom();
                currentElevator = null;
            }
        }
        currentElevator = null;
        isMoving = false;
    }

    void Update()
    {

        if(luckObj == null){luckObj = GameObject.FindGameObjectWithTag("Luck");} if(keyObj == null){keyObj = GameObject.FindGameObjectWithTag("Key"); }
        if(paintObj == null){paintObj = GameObject.FindGameObjectWithTag("Paint");} if(chairObj == null){chairObj = GameObject.FindGameObjectWithTag("Chair");}

        if(pressedElevators == null)
        {
            pressedElevators = GameObject.FindGameObjectsWithTag("ButtonElev");
        }

        if(GameObject.FindGameObjectWithTag("LightedRoom3"))
        {
            PP.hasPassword = true;
        }



        if(currentElevator == null)
        {
            CheckForElevator();
        }

        if(ElevatorObj == null)
        {
            ElevatorObj = GameObject.FindGameObjectWithTag("Elevator");
        }

        if(animGlobal == null)
        {
            if (CurrentEtazh != null) { animGlobal = CurrentEtazh.GetComponent<Animator>(); }
        }

        if(animGlobalUp == null)
        {if (TopEtazh != null) { animGlobalUp = TopEtazh.GetComponent<Animator>(); }}


        if(animGlobalDown == null)
        {if (BottomEtazh != null) { animGlobalDown = BottomEtazh.GetComponent<Animator>(); }}

        if(anim == null)
        {
            anim = Player.GetComponent<Animator>();
        }

        if(isPainted)
        {
            PP.Ending = 4;
        }

        if(PP == null)
        {
            PP = Player.GetComponent<PlayerTook>();
        }

        if(DoorObj == null)
        {
            pressedDoors = GameObject.FindGameObjectsWithTag("Door");
        }

        if(flashObj == null)
        {
            flashObj = GameObject.FindGameObjectWithTag("Flashlight");
        }

        if(bucketObj == null)
        {
            bucketObj = GameObject.FindGameObjectWithTag("Bucket");
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