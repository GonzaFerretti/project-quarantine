using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public ModelPlayable player;
    public float camDistance;

    public bool isMainCamera;

    public float camDistanceStep;
    public float camRotationStep;
    private float startingDistance;
    private Vector3 defaultCamRotation;
    public bool shouldMove = true;

    public Vector2 smooth;

    public bool activateSmooth;

    //Tentative
    private void Start()
    {
        if (!GetComponent<SoundManager>()) gameObject.AddComponent<SoundManager>();
        StartCoroutine(FindPlayer());
        if (isMainCamera)
        {
            defaultCamRotation = transform.localRotation.eulerAngles;
            startingDistance = camDistance;
            updateMovementDirection();
            foreach (CameraMovement cam in FindObjectsOfType<CameraMovement>())
            {
                if (cam != this)
                {
                    cam.transform.localRotation = Quaternion.Euler(new Vector3(90, defaultCamRotation.y, defaultCamRotation.z));
                    break;
                }
            }
        }
    }

    IEnumerator FindPlayer()
    {
        yield return new WaitForEndOfFrame();
        if (!player) player = FindObjectOfType<ModelPlayable>();
    }
    /*
    public void setFixedCamera(bool _shouldMove,Vector3 position, Vector3 rotation)
    {
        shouldMove = _shouldMove;
        if (!shouldMove)
        {
            if (isMainCamera)
            {
                transform.position = position;
                transform.rotation = Quaternion.Euler(rotation);
            }
        }
    }*/

    private void LateUpdate()
    {
        if (player && shouldMove)
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
        else transform.position = new Vector3(player.transform.position.x - camDistance * (transform.forward.x), (/*player.transform.position.y + */player.standingBodyHeight)- camDistance * (transform.forward.y), player.transform.position.z - camDistance * (transform.forward.z));
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("DebugDelDebug");
            FindObjectOfType<ModelPlayable>();
        }*/
        if (shouldMove)
        {
            if (isMainCamera)
            {
                if (Input.GetAxis("Mouse ScrollWheel") != 0)
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
                if (Input.GetKey(KeyCode.E))
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
