using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public ModelPlayable player;
    public float camDistance;

    public bool isMainCamera;

    [Header("Test")]
    public bool testLerp;
    public float testWaitDuration;
    public float testTransitionDuration;
    public Transform testTarget;
    public Vector3 testDistanceVector;


    public float camDistanceStep;
    public float camRotationStep;
    private float startingDistance;
    private Vector3 defaultCamRotation;
    public bool shouldMove = true;
    public focusStatus status = focusStatus.isNotFocused;

    public Vector2 smooth;

    public bool activateSmooth;

    public enum focusStatus
    {
        isFocused,
        isFocusing,
        isNotFocused,
    }

    //Tentative
    private void Start()
    {
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

    public void StartFocusOnPoint(Vector3 targetPos, float waitDuration, float transitionDuration, Vector3 distanceVector)
    {
        StartCoroutine(FocusOnPoint(targetPos, waitDuration, transitionDuration, distanceVector));
    }

    IEnumerator FocusOnPoint(Vector3 targetPos, float waitDuration,float transitionDuration, Vector3 distanceVector)
    {
        float startTime = Time.time;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = targetPos + distanceVector;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.LookRotation(-distanceVector.normalized);
        StartCoroutine(LerpCamera(startTime, transitionDuration, startPosition, endPosition, startRotation, endRotation, focusStatus.isFocused));
        while (!(status == focusStatus.isFocused))
        {
            yield return null;
        }
        updateMovementDirection();
        yield return new WaitForSeconds(waitDuration);
        startTime = Time.time;
        endPosition = startPosition;
        startPosition = transform.position;
        endRotation = startRotation;
        startRotation = transform.rotation;
        StartCoroutine(LerpCamera(startTime, transitionDuration, startPosition, endPosition, startRotation, endRotation, focusStatus.isNotFocused));
        while (!(status == focusStatus.isNotFocused))
        {
            yield return null;
        }
        updateMovementDirection();
    }

    public IEnumerator LerpCamera(float startTime, float duration, Vector3 startPosition, Vector3 endPosition, Quaternion startRotation, Quaternion endRotation, focusStatus finalStatus)
    {
        status = focusStatus.isFocusing;
        while (Time.time - startTime < duration)
        {
            float index = Mathf.InverseLerp(0, duration, Time.time - startTime);
            transform.position = Vector3.Lerp(startPosition, endPosition, index);
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, index);
            yield return null;
        }
        status = finalStatus;
    }

    IEnumerator FindPlayer()
    {
        yield return new WaitForEndOfFrame();
        if (!player) player = FindObjectOfType<ModelPlayable>();
    }

    private void LateUpdate()
    {
        if (player && shouldMove)
        {
            FollowTarget();
        }
    }

    void FollowTarget()
    {
        if (status == focusStatus.isNotFocused)
        {
            transform.position = new Vector3(player.transform.position.x, player.standingBodyHeight, player.transform.position.z) - camDistance * transform.forward;
        }
        if (testLerp)
        {
            testLerp = !testLerp;
            StartFocusOnPoint(testTarget.position, testWaitDuration, testTransitionDuration, testDistanceVector);
        }
    }

    private void Update()
    {
        if (shouldMove)
        {
            if (isMainCamera)
            {
                if (Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    camDistance += camDistanceStep * -Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;
                }
            }
        }
    }

    public void Rotate(float directionMult, float step)
    {
        Vector3 angle = transform.localRotation.eulerAngles;
        angle += Vector3.up * Time.deltaTime * step * directionMult;
        transform.localRotation = Quaternion.Euler(angle);
        if (isMainCamera) updateMovementDirection();
    }

    public void updateMovementDirection()
    {
        Vector3 right = transform.right * Mathf.Sqrt(2) / 2;
        Vector3 left = -right;
        Vector3 up = new Vector3(transform.forward.x, 0, transform.forward.z);
        Vector3 down = -up;
        ActionMovement.modifyDirections(up, left, down, right);
    }
}
