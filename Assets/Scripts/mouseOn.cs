using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite defaultSprite; // Спрайт по умолчанию
    public Sprite hoverSprite;   // Спрайт при наведении
    public GameObject InSound;
    public GameObject OutSound;

    private Image buttonImage;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = defaultSprite; // Устанавливаем начальный спрайт
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = hoverSprite; // Меняем спрайт при наведении
        Instantiate(InSound, transform.position, Quaternion.identity);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = defaultSprite; // Возвращаем начальный спрайт, когда мышь уходит
        Instantiate(OutSound, transform.position, Quaternion.identity);
    }
}
