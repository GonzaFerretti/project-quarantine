using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public ModelPlayable player;
    public float camDistance;

    public Vector2 smooth;

    private void LateUpdate()
    {
        if (player)
        {
            FollowTarget();
            AdjustPlayerMovement();
        }
    }

    void AdjustPlayerMovement()
    {
        PlayerController controller = player.controller as PlayerController;

        ActionMovementWrapper controlUp = controller.actionLinks[0].action as ActionMovementWrapper;
        ActionMovementWrapper controlDown = controller.actionLinks[1].action as ActionMovementWrapper;
        ActionMovementWrapper controlLeft = controller.actionLinks[2].action as ActionMovementWrapper;
        ActionMovementWrapper controlRight = controller.actionLinks[3].action as ActionMovementWrapper;

        controlUp.direction = movementKeysDirection.up;
        controlDown.direction = movementKeysDirection.down;
        controlLeft.direction = movementKeysDirection.left;
        controlRight.direction = movementKeysDirection.right;

        for (int i = 0; i < 4; i++)
        {
            controller.actionLinks[i].action.SetAction();
        }
    }


    void FollowTarget()
    {
        Vector3 newPos;

        newPos.x = ((player.transform.position.x - camDistance * (transform.forward.x) - transform.position.x) / smooth.x) * Time.deltaTime;
        newPos.y = ((player.transform.position.y - camDistance * (transform.forward.y) - transform.position.y) / smooth.y) * Time.deltaTime;
        newPos.z = ((player.transform.position.z - camDistance * (transform.forward.z) - transform.position.z) / smooth.y) * Time.deltaTime;

        transform.position += newPos;
    }
}
