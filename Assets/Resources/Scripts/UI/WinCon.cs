using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinCon : MonoBehaviour
{
    public List<string> reqKeys;
    public List<string> confirmed;
    public GameObject canvas;
    public GameObject winWindow;
    public ModelNPC grandmaAnim;
    public ModelNPC motherAnim;

    public TextMeshProUGUI prompt;
    public TextMeshProUGUI message;
    public TextMeshProUGUI confirmation;

    Animator _grandmaAnimator;
    Animator _momAnimator;

    public bool no;

    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForFixedUpdate();
        reqKeys = new List<string>(ResourceManager.requiredResources.Keys);

        confirmed = new List<string>();
        for (int i = 0; i < reqKeys.Count; i++)
        {
            if (ResourceManager.currentResources[reqKeys[i]] >= ResourceManager.requiredResources[reqKeys[i]])
            {
                if (!confirmed.Contains(reqKeys[i]))
                    confirmed.Add(reqKeys[i]);
            }
        }
        _grandmaAnimator = grandmaAnim.GetComponentInChildren<Animator>();
        _momAnimator = motherAnim.GetComponentInChildren<Animator>();
        if (confirmed.Count == reqKeys.Count)
        {
            EndGame("You win!", "Your family managed to live on for a long time!", "Yay!", true);
        }
        else
        {
            if(!no) 
            EndGame("You lost...", "You did not bring enough resources for your family to live...", "End.", false);
        }
    }

    void EndGame(string s, string ss, string sss, bool b)
    {
        //Time.timeScale = 0;
        //winWindow.SetActive(true);  
        ModelPlayable _player = FindObjectOfType<ModelPlayable>();
        TentativeMapInfoKeeper _keeper = FindObjectOfType<TentativeMapInfoKeeper>();
        _player.controller = _player.lossController;
        Camera.main.GetComponent<CameraMovement>().enabled = false;
        prompt.text = s;
        message.text = ss;
        confirmation.text = sss;

        SceneManager.activeSceneChanged -= _player.ChangedActiveScene;
        SceneManager.activeSceneChanged -= _keeper.SpawnItems;
        SceneManager.activeSceneChanged -= _keeper.SetCameraAttributes;
        EventManager.UnsubscribeToLocationEvent("EnterLocation", _player.LocRepositioning);
        EventManager.UnsubscribeToEvent("Loss", _player.LossBehavior);


        ScentCreator scentCreator = _player.GetComponentInChildren<ScentCreator>();
        for (int i = 0; i < scentCreator.scentObjects.Count; i++)
        {
            Destroy(scentCreator.scentObjects[i].gameObject);
        }


        StartCoroutine(Zooming());
        if (b)
        {
            _grandmaAnimator.SetBool("cheering", true);
            _momAnimator.SetBool("cheering", true);
        }
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    IEnumerator Zooming()
    {
        yield return new WaitForFixedUpdate();
        if (Camera.main.fieldOfView >= 12)
        {
            Camera.main.fieldOfView -= 1 * 15 * Time.deltaTime;
            Camera.main.transform.position += new Vector3(0.5f, 0, -0.2f) * Time.deltaTime;
            StartCoroutine(Zooming());
        }
        else
        {
            winWindow.SetActive(true);
        }
    }
}