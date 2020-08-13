using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemContainer : MonoBehaviour
{
    public TextMeshProUGUI amount;
    public Image UiImage;
    public ActionKeyLinks flingKeyLinks;
    public GameObject amountDisplay;
    public GameObject itemKeyDisplay;
    public GameObject modText;
    public Image selectedBorder;

    public void UpdateText(string text, bool isSelected)
    {
        amount.text = text;
        selectedBorder.gameObject.SetActive(isSelected);
    }

    public void InitContainer(Item item, string amountText, bool isSelected)
    {
        UiImage.sprite = item.icon;
        amount.text = amountText;
        UiImage.SetNativeSize();
        selectedBorder.gameObject.SetActive(isSelected);
        if (!item.isStackable)
        {
            amountDisplay.SetActive(false);
        }
        string keyText = "";
        if (item.allowingActions.Count > 0)
        {
            if (item.allowingActions[0].actionKey != null)
            {
                keyText = "[" + item.allowingActions[0].actionKey.key + "]";
            }
        }
        else
        {
            if (item is FlingableItem)
            {
                keyText = "[" + flingKeyLinks.key + "]";
            }
            else
            {
                itemKeyDisplay.SetActive(false);
                return;
            }
        }
        itemKeyDisplay.GetComponentInChildren<TextMeshProUGUI>().text = keyText;
    }
}
