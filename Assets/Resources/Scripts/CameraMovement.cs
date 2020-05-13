using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public ModelPlayable player;
    public float camDistance;

    [Header("Debug")]
    public float camDistanceStep;
    public float camRotationStep;
    private float startingDistance;
    private Vector3 defaultCamRotation;

    public Vector2 smooth;

    //Tentative
    private void Start()
    {
        if (!player) player = FindObjectOfType<ModelPlayable>();
        startingDistance = camDistance;
        defaultCamRotation = transform.localRotation.eulerAngles;
    }

    private void LateUpdate()
    {
        if (player)
        {
            FollowTarget();
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
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            camDistance = startingDistance;
            Debug.Log(defaultCamRotation);
            transform.localRotation = Quaternion.Euler(defaultCamRotation);
            ActionMovement.resetDirections();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            camDistance += camDistanceStep * -Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.C))
        {
            Vector3 angle = transform.localRotation.eulerAngles;
            angle += Vector3.down * Time.deltaTime * camRotationStep;
            transform.localRotation = Quaternion.Euler(angle);
            updateMovementDirection();
        }
        else if(Input.GetKey(KeyCode.Z))
        {
            Vector3 angle = transform.localRotation.eulerAngles;
            angle += Vector3.up * Time.deltaTime * camRotationStep;
            transform.localRotation = Quaternion.Euler(angle);
            updateMovementDirection();
        }
    }

    private void updateMovementDirection()
    {
        Vector3 right = transform.right * Mathf.Sqrt(2) / 2;
        Vector3 left = -right;
        Vector3 up = new Vector3(transform.forward.x,0,transform.forward.z);
        Vector3 down = -up;
        /*Vector3 up = transform.forward * Mathf.Sqrt(2) / 2;
        Vector3 down = -up;*/
        ActionMovement.directionVectors[movementKeysDirection.up] = up;
        ActionMovement.directionVectors[movementKeysDirection.down] = down;
        ActionMovement.directionVectors[movementKeysDirection.left] = left;
        ActionMovement.directionVectors[movementKeysDirection.right] = right;
    }
}
