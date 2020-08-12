using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("Item container settings")]
    public GameObject sampleContainer;
    public float distance;
    public GameObject initialPivot;
    private Vector3 initialPos;
    [Header("+/- amount text settings")]
    public GameObject modTextPrefab;
    public float modTextDistance;
    public float modTextTime;
    public float modTextStartHeight;
    public float usableItemFlyingTextDelta;


    public Dictionary<string,ItemContainer> itemContainers = new Dictionary<string, ItemContainer>();
    public Dictionary<GameObject, FlyingUItext> modTexts = new Dictionary<GameObject, FlyingUItext>();

    public void Start()
    {
        initialPos = initialPivot.GetComponent<RectTransform>().anchoredPosition;
    }

    public void RemoveFromInterface(Item item)
    {
        if (itemContainers.ContainsKey(item.name))
        {
            GameObject go = itemContainers[item.name].gameObject;
            itemContainers.Remove(item.name);
            Destroy(go);
        }
    }

    public KeyValuePair<GameObject, FlyingUItext> GetAvailableModText(string itemName)
    {
        KeyValuePair<GameObject, FlyingUItext> modTextElement;
        bool foundAvailable = false;
        foreach (KeyValuePair<GameObject, FlyingUItext> modText in modTexts)
        {
            if (!modText.Key.activeSelf)
            {
                modTextElement = modText;
                foundAvailable = true;
                modText.Key.gameObject.SetActive(true);
                break;
            }
        }
        if (!foundAvailable)
        {
            //Debug.Log("not available");
            GameObject go = Instantiate(modTextPrefab, transform);
            FlyingUItext ui = go.GetComponent<FlyingUItext>();
            modTextElement = new KeyValuePair<GameObject, FlyingUItext>(go, ui);
            modTexts.Add(go,ui);
        }
        return modTextElement;
    }

    public void UpdateItem(Item item, int amount, bool isAdding, bool isReloading, bool isSelected, bool isSelecting)
    {
        //Debug.Log(item.displayName);
        if (!itemContainers.ContainsKey(item.name) || (isReloading))
        {
            GameObject newContainer = Instantiate(sampleContainer, transform);
            newContainer.name = item.displayName;
            itemContainers[item.name] = newContainer.GetComponent<ItemContainer>();
            newContainer.GetComponent<RectTransform>().anchoredPosition = new Vector3(initialPos.x + distance * itemContainers.Count - 1, initialPos.y, initialPos.z);
            itemContainers[item.name].InitContainer(item, amount.ToString(), isSelected);
            //Debug.Log("cantidad de item containers " + itemContainers.Count);
        }
        else
        {
            //Debug.Log("Actualizando data");
            ItemContainer itemCont = itemContainers[item.name];
            itemCont.UpdateText(amount.ToString(), isSelected);
            if (item.isStackable && !isReloading && !isSelecting)
            {
                KeyValuePair<GameObject, FlyingUItext> flyingText = GetAvailableModText(item.name);
                Vector3 contPos = itemCont.GetComponent<RectTransform>().anchoredPosition;
                float startingHeight = (itemCont.itemKeyDisplay.activeSelf) ? modTextStartHeight + usableItemFlyingTextDelta : modTextStartHeight;
                flyingText.Key.GetComponent<RectTransform>().anchoredPosition = new Vector3(contPos.x , startingHeight, contPos.z);
                string value = (isAdding) ? "+1" : "-1";
                flyingText.Value.StartMoving(modTextDistance,modTextTime, value);
            }
        }
    }
}
