using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ChangeSprite : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject DarkImage;
    public GameObject NormalText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse entered: " + gameObject.name);
        DarkImage.SetActive(true);
        NormalText.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exited: " + gameObject.name);
        DarkImage.SetActive(false);
        NormalText.SetActive(true);
    }
}
