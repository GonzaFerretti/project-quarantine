using UnityEngine;
using UnityEngine.UI;

public class AlertPhaseTimer : MonoBehaviour
{
    public float maxTimer;
    public float timer;
    public Text timerText;
    public Image seenIndicator;
    public Sprite eyeShut, eyeOpen;
    bool _onAlert;
    public SoundManager soundManager;
    public SoundClip alertSound;

    private void Start()
    {
        if (!seenIndicator)
        { 
        seenIndicator = GameObject.Find("IconitoOjoCambiable").GetComponent<Image>();
        }
        seenIndicator.sprite = eyeShut;
        EventManager.SubscribeToEvent("Alert", ActivateAlert);
        EventManager.SubscribeToEvent("UnsubEnter", EnterBehavior);
        /*
        if (!timerText)
        {
            try
            {
                timerText = GameObject.Find("DiscoveryTimer").GetComponent<Text>();
            }
            catch
            {
                Debug.LogError("Couldn't find Timer Object! Please make sure the Canvas was added to the scene!");
            }
        }*/
    }

    private void ActivateAlert()
    {
        if (timer == 0)
        {
            if (soundManager == null)
                soundManager = Camera.main.GetComponent<SoundManager>();

            if (alertSound)
                soundManager.Play(alertSound);
        }
        timer = maxTimer;
        _onAlert = true;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            /*
            timerText.color = Color.red;*/
            seenIndicator.sprite = eyeOpen;
            //timerText.text = "Alert!" + "\n" + timer.ToString("F0");
        }
        else
        {
            if (_onAlert == true)
            {
                timer = 0;
                DeactivateAlert();
                _onAlert = false;
            }
        }
    }

    void EnterBehavior()
    {
        EventManager.UnsubscribeToEvent("Alert", ActivateAlert);
        EventManager.UnsubscribeToEvent("UnsubEnter", EnterBehavior);
    }

    private void DeactivateAlert()
    {
        EventManager.TriggerEvent("AlertStop");
        /*timerText.color = Color.black;
        timerText.text = "Unseen";*/
        seenIndicator.sprite = eyeShut;
    }
}
