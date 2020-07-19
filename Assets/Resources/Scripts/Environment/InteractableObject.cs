using UnityEngine;

public class InteractableObject : Model
{
    public ActionWrapper requiredAction;
    public InteractableFeedbackWrapper feedback;
    public Animator animator;
    public InteractablePreviewWrapper preview;

    //Tentative
    public GameObject objModel;
    public RuntimeAnimatorController anims;

    //Tentative
    public virtual void initModel(ref Animator animator, GameObject characterModel, RuntimeAnimatorController animations)
    {
        GameObject myPlayerCharacter = Instantiate(characterModel, transform);
        myPlayerCharacter.transform.localPosition = new Vector3(0, 0, 0);
        if (animations)
        {
            myPlayerCharacter.GetComponent<Animator>().runtimeAnimatorController = animations;
            animator = myPlayerCharacter.GetComponent<Animator>();
        }
        myPlayerCharacter.name = "objModel";
    }
    
    protected virtual void Start()
    {
        if (objModel && anims)
        {
            initModel(ref animator, objModel, anims);
        }
        if(preview)
        {
            preview.SetAction();
        }
        
        /*
        _mat = Resources.Load<Material>("Art/Visual/Placeholder/Blue");
        transform.GetComponent<MeshRenderer>().material = _mat;*/
    }

    public void Interact(ModelPlayable c)
    {
        if (feedback)
        { 
        if (feedback.action == null) feedback.SetAction();
        feedback.action.Do(this);
        }
    }
}
