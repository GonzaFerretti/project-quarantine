using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LossMenuManager : MonoBehaviour
{
    public GameObject lossBox;

    private void Start()
    {
        EventManager.SubscribeToEvent("Loss",LossBehavior);
    }

    void LossBehavior()
    {
        Time.timeScale = 0;
        lossBox.SetActive(true);
    }

    public void LossConfirm()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
