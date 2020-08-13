using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

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
        EventManager.SubscribeToEvent("Alert", ActivateAlert);
        EventManager.SubscribeToEvent("UnsubEnter", EnterBehavior);
        soundManager = FindObjectOfType<SoundManager>();
        StartCoroutine(StartSeenIndicator());
    }

    IEnumerator StartSeenIndicator()
    {
        yield return new WaitForSeconds(0.5f);
        if (!seenIndicator)
        {
            seenIndicator = GameObject.Find("IconitoOjoCambiable").GetComponent<Image>();
        }
        seenIndicator.sprite = eyeShut;
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
            seenIndicator.sprite = eyeOpen;
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