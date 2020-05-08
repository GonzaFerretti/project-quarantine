using UnityEngine;

public class Model : MonoBehaviour
{
    public virtual void initModel(ref Animator animator, GameObject characterModel, RuntimeAnimatorController animations)
    {
        GameObject myPlayerCharacter = Instantiate(characterModel, transform);
        myPlayerCharacter.GetComponent<Animator>().runtimeAnimatorController = animations;
        myPlayerCharacter.name = "characterModel";
        animator = myPlayerCharacter.GetComponent<Animator>();
    }
}
