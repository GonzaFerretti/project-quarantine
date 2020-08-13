using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionFirecracker : BaseAction
{
    SoundManager _soundManager;

    public override void Do(Model m)
    {
        ModelPlayable mp = m as ModelPlayable;
        if (mp.firecracker == null) mp.firecracker = MonoBehaviour.Instantiate(mp.baseFirecracker);

        mp.firecracker.gameObject.SetActive(true);
        mp.firecracker.transform.position = mp.transform.position;
        mp.firecracker.StartCoroutine(mp.firecracker.TurnOff());
        mp.StartCoroutine(CancelLoss());
        if (!_soundManager) _soundManager = MonoBehaviour.FindObjectOfType<SoundManager>();
        if (clip)
           _soundManager.Play(clip);
    }   

    IEnumerator CancelLoss()
    {
        yield return new WaitForEndOfFrame();
        EventManager.TriggerEvent("LossCancel");
    }
}