using UnityEngine;

public class Model : MonoBehaviour
{
    public SoundManager sm;
    public Renderer[] rens;
    public virtual void InitModel(ref Animator animator, GameObject characterModel, RuntimeAnimatorController animations)
    {
        GameObject myPlayerCharacter = Instantiate(characterModel, transform);
        myPlayerCharacter.transform.localPosition = new Vector3(0, 0, 0);
        if (animations)
        {
            if (myPlayerCharacter.GetComponent<Animator>())
            {
                myPlayerCharacter.GetComponent<Animator>().runtimeAnimatorController = animations;
                animator = myPlayerCharacter.GetComponent<Animator>();
            }
            else
            {
                myPlayerCharacter.GetComponentInChildren<Animator>().runtimeAnimatorController = animations;
                animator = myPlayerCharacter.GetComponentInChildren<Animator>();
            }
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
        rens = myPlayerCharacter.GetComponentsInChildren<Renderer>();
    }
}
