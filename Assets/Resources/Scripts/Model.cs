using UnityEngine;

public class Model : MonoBehaviour
{
    public SoundManager sm;
    public virtual void InitModel(ref Animator animator, GameObject characterModel, RuntimeAnimatorController animations)
    {
        GameObject myPlayerCharacter = Instantiate(characterModel, transform);
        myPlayerCharacter.transform.localPosition = new Vector3(0, 0, 0);
        if (animations)
        { 
        myPlayerCharacter.GetComponent<Animator>().runtimeAnimatorController = animations;
        animator = myPlayerCharacter.GetComponent<Animator>();
        }
        SoundManager globalSm = FindObjectOfType<SoundManager>();
        sm = globalSm;
        if (GetComponentInChildren<CharacterSounds>())
        {
            CharacterSounds charSounds = myPlayerCharacter.GetComponentInChildren<CharacterSounds>();
            charSounds.FindSoundManager();
        }
        else
        {
            globalSm.GetComponent<CharacterSounds>().Clone(myPlayerCharacter);
        }
        myPlayerCharacter.name = "characterModel";
    }
}
