using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Controller/Action/Interact Preview")]
public class ActionInteractPreviewWrapper : ActionWrapper
{
    public float rayDistance;
    public Text text;
    public float arc, density;

    public override void SetAction()
    {
        action = new ActionInteractPreview(rayDistance,text,arc,density);
    }
}
