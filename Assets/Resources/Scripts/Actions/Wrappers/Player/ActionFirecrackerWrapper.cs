using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Firecracker")]
public class ActionFirecrackerWrapper : ActionWrapper
{
    public SoundClip sound;

    public override void SetAction()
    {
        action = new ActionFirecracker(sound);
    }
}