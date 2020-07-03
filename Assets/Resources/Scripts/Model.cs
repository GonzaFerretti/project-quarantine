using UnityEngine;

public class Model : MonoBehaviour
{
    public virtual void InitModel(ref Animator animator, GameObject characterModel, RuntimeAnimatorController animations)
    {
        GameObject myPlayerCharacter = Instantiate(characterModel, transform);
        myPlayerCharacter.transform.localPosition = new Vector3(0, 0, 0);
        if (animations)
        { 
        myPlayerCharacter.GetComponent<Animator>().runtimeAnimatorController = animations;
        animator = myPlayerCharacter.GetComponent<Animator>();
        }
        myPlayerCharacter.AddComponent<CharacterSounds>();
        myPlayerCharacter.name = "characterModel";
    }
}
