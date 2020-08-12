using UnityEngine;

public class BushModel : Model
{
    public SoundClip sound;

    private void Start()
    {
        sm = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ModelPlayable>())
        {
        sm.Play(sound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ModelPlayable>())
        {
            sm.Play(sound);
        }
    }
}