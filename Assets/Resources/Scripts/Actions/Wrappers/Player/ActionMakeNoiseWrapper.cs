using UnityEngine;

[CreateAssetMenu(menuName ="Controller/Action/Make Noise")]
public class ActionMakeNoiseWrapper : ActionWrapper
{
    public override void SetAction()
    {
        action = new ActionMakeNoise();
    }
}
