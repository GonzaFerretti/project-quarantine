
using UnityEngine;
using UnityEngine.UI;

public class AlertPhaseTimer : MonoBehaviour
{
    public float maxTimer;
    public float timer;
    public Text timerText;

    private void Start()
    {
        EventManager.SubscribeToEvent("Alert", ActivateAlert);
    }

    private void ActivateAlert()
    {
        timer = maxTimer;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.color = Color.red;
            timerText.text = "Alert!" + "\n"  + timer.ToString("F0");
        }
        else
        {
            timer = 0;
            DeactivateAlert();
        }
    }


    private void DeactivateAlert()
    {
        EventManager.TriggerEvent("AlertStop");
        timerText.color = Color.black;
        timerText.text = "Unseen";
    }
}
