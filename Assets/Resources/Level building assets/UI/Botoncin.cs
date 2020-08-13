using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Botoncin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float origFont, howMuchBigger;
    TextMeshProUGUI textm;

    void Start()
    {
        textm = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        origFont = textm.fontSize;
    }

    public void BiggerFont()
    {
        textm.fontSize = origFont + howMuchBigger;
    }
    public void OrigFont()
    {
        textm.fontSize = origFont;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        BiggerFont();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OrigFont();
    }
}
