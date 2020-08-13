using UnityEngine;

public class DoorIconChanger : MonoBehaviour
{
    public Sprite allowedSprite;
    public Sprite forbiddenSprite;
    public Color allowedColor;
    public Color forbiddenColor;
    public SpriteRenderer spr;
    public Light myLight;

    void Start()
    {
        EventManager.SubscribeToEvent("Enter", Unsubscriber);
        EventManager.SubscribeToEvent("Alert", AlertChange);
        EventManager.SubscribeToEvent("AlertStop", AlertEndChange);
    }

    void AlertChange()
    {
        spr.sprite = forbiddenSprite;
        myLight.color = forbiddenColor;
    }

    void AlertEndChange()
    {
        if(spr)
        spr.sprite = allowedSprite;   
        myLight.color = allowedColor;
    }

    void Unsubscriber()
    {
        EventManager.UnsubscribeToEvent("Enter", Unsubscriber);
        EventManager.UnsubscribeToEvent("Alert", AlertChange);
        EventManager.UnsubscribeToEvent("AlertEnd", AlertEndChange);
    }
}