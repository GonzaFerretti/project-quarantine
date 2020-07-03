using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionFirecracker : IAction
{
    AudioClip _sound;
    SoundManager _soundManager;

    public ActionFirecracker(AudioClip sound)
    {
        _sound = sound;
    }

    public void Do(Model m)
    {
        ModelPlayable mp = m as ModelPlayable;
        if (mp.firecracker == null) mp.firecracker = MonoBehaviour.Instantiate(mp.baseFirecracker);

        mp.firecracker.gameObject.SetActive(true);
        mp.firecracker.transform.position = mp.transform.position;
        mp.firecracker.StartCoroutine(mp.firecracker.TurnOff());
        mp.StartCoroutine(CancelLoss());
        if (!_soundManager) _soundManager = MonoBehaviour.FindObjectOfType<SoundManager>();
        if (_sound)
           _soundManager.Play(_sound);
    }   

    IEnumerator CancelLoss()
    {
        yield return new WaitForEndOfFrame();
        EventManager.TriggerEvent("LossCancel");
    }
}