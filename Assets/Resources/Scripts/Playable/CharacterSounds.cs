using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSounds : MonoBehaviour
{
    SoundManager _manager;
    public SoundClip leftFoot;
    public SoundClip rightFoot;
    public SoundClip landAfterVault;

    void Start()
    {
        FindSoundManager();
    }

    public void FindSoundManager()
    {
        if(!_manager)
        _manager = FindObjectOfType<SoundManager>();
    }

    public void PlayLeftFootStep(float f)
    {
        FindSoundManager();
        _manager.Play(leftFoot);
    }

    public void PlayRightFootStep(float f)
    {

        FindSoundManager();
        _manager.Play(rightFoot);
    }

    public void PlayLandAfterVault(float f)
    {
        FindSoundManager();
        _manager.Play(landAfterVault);
    }

    public void PlayBottleCrash(float f)
    {
        FindSoundManager();
    }

}
