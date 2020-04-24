using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public ActionWrapper requiredAction;
    public InteractableFeedbackWrapper feedback;

    //Tentative
    Material _mat;
    protected virtual void Start()
    {
        _mat = Resources.Load<Material>("Art/Visual/Placeholder/Blue");
        transform.GetComponent<MeshRenderer>().material = _mat;
    }

    public void Interact(ModelPlayable c)
    {
        if (feedback.action == null) feedback.SetAction();
        feedback.action.Do(this);
    }
}
