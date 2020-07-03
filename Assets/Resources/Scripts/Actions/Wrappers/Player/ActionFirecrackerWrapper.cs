using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Firecracker")]
public class ActionFirecrackerWrapper : ActionWrapper
{
    public AudioClip sound;

    public override void SetAction()
    {
        action = new ActionFirecracker(sound);
    }
}