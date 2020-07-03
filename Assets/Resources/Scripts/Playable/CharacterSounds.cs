using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSounds : MonoBehaviour
{
    SoundManager _manager;

    void Start()
    {
        FindSoundManager();
    }

    public void ReloadSM()
    {

    }

    void FindSoundManager()
    {
        if(!_manager)
        _manager = FindObjectOfType<SoundManager>();
    }

    public void PlayLeftFootStep(float f)
    {
        FindSoundManager();
        _manager.Play(Resources.Load<AudioClip>("Art/sfx/leftFoot"), false, f);
    }

    public void PlayRightFootStep(float f)
    {

        FindSoundManager();
        _manager.Play(Resources.Load<AudioClip>("Art/sfx/rightFoot"), false, f);
    }

    public void PlayLandAfterVault(float f)
    {
        FindSoundManager();
        _manager.Play(Resources.Load<AudioClip>("Art/sfx/landAfterVault"), false, f);
    }

    public void PlayBottleCrash(float f)
    {
        FindSoundManager();

    }

}
