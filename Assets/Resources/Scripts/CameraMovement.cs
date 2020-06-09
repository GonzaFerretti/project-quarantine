using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public ModelPlayable player;
    public float camDistance;

    public bool isMainCamera;

    public float camDistanceStep;
    public float camRotationStep;
    private float startingDistance;
    private Vector3 defaultCamRotation;
    public CameraMovement otherCamera;

    public Vector2 smooth;

    public bool activateSmooth;

    //Tentative
    private void Start()
    {
        if (!player) player = FindObjectOfType<ModelPlayable>();
        defaultCamRotation = transform.localRotation.eulerAngles;
        if (isMainCamera)
        {
            startingDistance = camDistance;
            updateMovementDirection();
        }
        else
        {
            foreach (CameraMovement cam in FindObjectsOfType<CameraMovement>())
            {
                if (cam != this)
                {
                    otherCamera = cam;
                    transform.localRotation = Quaternion.Euler(new Vector3(90,otherCamera.defaultCamRotation.y,otherCamera.defaultCamRotation.z));
                    defaultCamRotation = transform.localRotation.eulerAngles;
                    break;
                }
            }
        }
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
        if (activateSmooth)
        {

            Vector3 newPos;
            newPos.x = ((player.transform.position.x - camDistance * (transform.forward.x) - transform.position.x) / smooth.x) * Time.deltaTime;
            newPos.y = ((player.transform.position.y - camDistance * (transform.forward.y) - transform.position.y) / smooth.y) * Time.deltaTime;
            newPos.z = ((player.transform.position.z - camDistance * (transform.forward.z) - transform.position.z) / smooth.y) * Time.deltaTime;

            transform.position += newPos;
        }
        else transform.position = new Vector3(player.transform.position.x - camDistance * (transform.forward.x), player.transform.position.y - camDistance * (transform.forward.y), player.transform.position.z - camDistance * (transform.forward.z));
    }

    private void Update()
    {
        if (isMainCamera)
        {
            if (Input.GetKey(KeyCode.R))
            {
                camDistance = startingDistance;
                transform.localRotation = Quaternion.Euler(defaultCamRotation);
                updateMovementDirection();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                camDistance += camDistanceStep * -Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                Vector3 angle = transform.localRotation.eulerAngles;
                angle += Vector3.down * Time.deltaTime * camRotationStep;
                transform.localRotation = Quaternion.Euler(angle);
                updateMovementDirection();
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                Vector3 angle = transform.localRotation.eulerAngles;
                angle += Vector3.up * Time.deltaTime * camRotationStep;
                transform.localRotation = Quaternion.Euler(angle);
                updateMovementDirection();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.R))
            {
                transform.localRotation = Quaternion.Euler(defaultCamRotation);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                Vector3 angle = transform.localRotation.eulerAngles;
                angle += Vector3.down * Time.deltaTime * camRotationStep;
                transform.localRotation = Quaternion.Euler(angle);
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                Vector3 angle = transform.localRotation.eulerAngles;
                angle += Vector3.up * Time.deltaTime * camRotationStep;
                transform.localRotation = Quaternion.Euler(angle);
            }
        }
    }

    private void updateMovementDirection()
    {
        Vector3 right = transform.right * Mathf.Sqrt(2) / 2;
        Vector3 left = -right;
        Vector3 up = new Vector3(transform.forward.x, 0, transform.forward.z);
        Vector3 down = -up;
        ActionMovement.modifyDirections(up, left, down, right);
    }
}
