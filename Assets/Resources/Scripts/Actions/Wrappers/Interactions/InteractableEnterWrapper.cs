using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Environment/Objects/Enter")]
public class InteractableEnterWrapper : InteractableFeedbackWrapper
{
    public string[] scenes;

    public override void SetAction()
    {
        action = new InteractableEnter(this);
    }
}
