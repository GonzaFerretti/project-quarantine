using UnityEngine;

[CreateAssetMenu(menuName ="Controller/Action/Movement")]
public class ActionMovementWrapper : ActionWrapper
{
    public movementKeysDirection direction;

    public override void SetAction()
    {
        action = new ActionMovement(direction);
    }
}
