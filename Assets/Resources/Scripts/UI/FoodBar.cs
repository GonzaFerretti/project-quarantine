using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodBar : ResourceUI
{
    public Image foodFillBar;
    public TextMeshProUGUI foodBarText;
    public Color unfilledColor;
    public Color filledColor;

    public override void UpdateUI(int current, int required)
    {
        // tentative
        foodFillBar.fillAmount = current / (required*1f);
        foodBarText.text = current + "/" + required;
        if (foodFillBar.fillAmount >= 1)
        {
            foodFillBar.color = filledColor;
        }
        else
        {
            foodFillBar.color = unfilledColor;
        }
    }
}
