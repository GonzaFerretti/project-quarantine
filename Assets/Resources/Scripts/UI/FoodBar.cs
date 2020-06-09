using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodBar : MonoBehaviour
{
    public Image foodFillBar;
    public Text foodBarText;
    public Color unfilledColor;
    public Color filledColor;

    public void UpdateFoodBar(float amount)
    {
        // tentative
        int totalRequired = 2;
        Debug.Log(amount / (totalRequired * 1f));
        foodFillBar.fillAmount = amount / (totalRequired*1f);
        foodBarText.text = amount + "/" + totalRequired + " FOOD";
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
