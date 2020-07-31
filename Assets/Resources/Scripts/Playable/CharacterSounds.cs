using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSounds : MonoBehaviour
{
    public SoundManager _manager;
    public SoundClip leftFoot;
    public SoundClip rightFoot;
    public SoundClip landAfterVault;

    void Start()
    {
        FindSoundManager();
    }

    public void Clone(GameObject go)
    {
        CharacterSounds charSounds = go.AddComponent<CharacterSounds>();
        charSounds.leftFoot = leftFoot;
        charSounds.rightFoot = rightFoot;
        charSounds.landAfterVault = landAfterVault;
        charSounds._manager = _manager;
    }

    public void FindSoundManager()
    {
        if (!_manager)
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
